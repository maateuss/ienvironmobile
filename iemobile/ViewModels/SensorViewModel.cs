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


        }

        public ObservableCollection<Ambiente> AmbientesList { get; set; }
        public ObservableCollection<Sensor> SensorList { get; set; }

        private readonly IAmbienteService ambienteService;
        private readonly ISensorService sensorService;

        public SensorViewModel(IAmbienteService ambienteService, ISensorService sensorService)
        {
            this.ambienteService = ambienteService;
            this.sensorService = sensorService;
            SelectedAmbienteCommand = new Command<Ambiente>(async (ambiente) => await SelectedAmbienteAsync(ambiente));
            GoBackCommand = new Command(async () => await CoreMethods.PopPageModel());
        }


        private async Task SelectedAmbienteAsync(Ambiente ambiente)
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;
            ShowLoading();
            try
            {
                Debug.WriteLine(ambiente);
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
        public ICommand GoBackCommand { get; }
        public ICommand SelectedAmbienteCommand { get; }

    }
}
