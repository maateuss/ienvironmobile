using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using iemobile.Interfaces;
using iemobile.Models;
using iemobile.Services;
using Xamarin.Forms;

namespace iemobile.ViewModels
{
    public class SensorViewModel : BaseViewModel
    {
        public ICommand GoBackCommand { get; }
        public ICommand SelectedDataCommand { get; }
        public ICommand EditUserCommand { get; }

        private MqttService mqttService;
        private object initData;

        public override async void Init(object initData)
        {
            base.Init(initData);

            this.initData = initData;

            Debug.WriteLine(initData);
            mqttService = new MqttService();
            await FetchData(initData);
            MessagingCenter.Subscribe<MqttService, string>(this, "message", (sender, message) =>
            {
                var splits = message.Split('|');
                Device.BeginInvokeOnMainThread(async () =>
                    await HandleMessage(splits[0],splits[1])
                );
            });
        }

        private async Task HandleMessage(string topic, string message)
        {
            if(SensorList.Any(x=>x.Topic == topic))
            {
                var sensor = SensorList.FirstOrDefault(x => x.Topic == topic);
                if(sensor != null)
                {
                    var index = SensorList.IndexOf(sensor);
                    SensorList.RemoveAt(index);
                    sensor.Valor = message;
                    SensorList.Insert(index, sensor);
                }
            } else if (AtuadorList.Any(x=>x.Topic == topic)) {
                var atuador = AtuadorList.FirstOrDefault(x => x.Topic == topic);
                if(atuador != null)
                {
                    var index = AtuadorList.IndexOf(atuador);
                    AtuadorList.RemoveAt(index);
                    atuador.Valor = message;
                    AtuadorList.Insert(index, atuador);
                }

            } else if (topic.StartsWith("alert"))
            {
                await DisplayAlert("alerta", message, "ok");
            }
        }

        private async Task FetchData(object initData)
        {
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
                    if (((Evento)data).IsManual)
                    {
                        var activate = await Prompt("Acionar Evento", $"Deseja acionar o evento {((Evento)data).Nome}? ", "Sim", "Não");
                        if (activate)
                        {
                            ShowLoading();
                            var result = await eventoService.AcionarEventoManual((Evento)data);
                            await DisplayAlert(result ? "Evento acionado com sucesso!" : "Verifique a conexão, não foi possível acionar o evento");
                            if (result) { await FetchData(initData); }

                        }
                    }
                    else
                    {
                        await DisplayAlert(((Evento)data).Description);
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
