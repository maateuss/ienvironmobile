using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using iemobile.Interfaces;
using iemobile.Models;
using Xamarin.Forms;

namespace iemobile.ViewModels
{
    public class SensorViewModel : BaseViewModel
    {
        public ICommand GoBackCommand { get; }
        public ICommand SelectedDataCommand { get; }
        public ICommand EditUserCommand { get; }


        public override async void Init(object initData)
        {
            base.Init(initData);

            Debug.WriteLine(initData);

            AmbientesList = new ObservableCollection<Ambiente>();

            var ambientes = await ambienteService.BuscarAmbientes();

            foreach (var item in ambientes)
            {
                AmbientesList.Add(item);
            }

            SensorList = new ObservableCollection<Sensor>();

            var sensores = await sensorService.BuscarSensores(initData as Ambiente);

            foreach (var item in sensores)
            {
                SensorList.Add(item);
            }

            AtuadorList = new ObservableCollection<Atuador>();

            var atuadores = await atuadorService.BuscarAtuadores(initData as Ambiente);

            foreach (var item in atuadores)
            {
                AtuadorList.Add(item);
            }

            EventosList = new ObservableCollection<Evento>();

            var eventos = await eventoService.BuscarEventos(initData as Ambiente);

            foreach (var item in eventos)
            {
                EventosList.Add(item);
            }


        }

        public ObservableCollection<Ambiente> AmbientesList { get; set; }
        public ObservableCollection<Sensor> SensorList { get; set; }
        public ObservableCollection<Atuador> AtuadorList { get; set; }
        public ObservableCollection<Evento> EventosList { get; set; }

        private readonly IAmbienteService ambienteService;
        private readonly ISensorService sensorService;
        private readonly IEventoService eventoService;
        private readonly IAtuadorService atuadorService;
        

        public SensorViewModel(IAmbienteService ambienteService, ISensorService sensorService, IAtuadorService atuadorService, IEventoService eventoService)
        {
            this.ambienteService = ambienteService;
            this.sensorService = sensorService;
            this.atuadorService = atuadorService;
            this.eventoService = eventoService;

            SelectedDataCommand = new Command<object>(async (data) => await SelectedDataAsync(data));
            GoBackCommand = new Command(async () => await CoreMethods.PopPageModel());
            EditUserCommand = new Command(async () => await CoreMethods.PushPageModel<EditUserViewModel>());
        }


        private async Task SelectedDataAsync(object data)
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;
            try
            {
                if(data is Atuador)
                {
                    Debug.WriteLine("Atuador Selecionado");
                }
                else if (data is Sensor)
                {
                    Debug.WriteLine("Sensor Selecionado");
                }
                else if (data is Evento)
                {
                    var activate = await Prompt("Acionar Evento", $"Deseja acionar o evento {((Evento)data).Nome}? ", "Sim", "Não");
                    if (activate)
                    {
                         ShowLoading();
                         await Task.Delay(200);
                        await DisplayAlert("Evento acionado com sucesso!");
                    }
                }
                else
                {
                    Debug.WriteLine("Tipo invalido");
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
                HideLoading();
            }


        }


    }
}
