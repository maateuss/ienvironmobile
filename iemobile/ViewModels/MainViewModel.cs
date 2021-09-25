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
    public class MainViewModel : BaseViewModel
    {


        public ICommand SelectedAmbienteCommand { get; }
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


        }

        public ObservableCollection<Ambiente> AmbientesList { get; set; }

        private readonly IAmbienteService ambienteService;





        public MainViewModel(IAmbienteService ambienteService)
        {
            this.ambienteService = ambienteService;
            SelectedAmbienteCommand = new Command<Ambiente>(async (ambiente) => await SelectedAmbienteAsync(ambiente));
            EditUserCommand = new Command(async () => await EditUserAsync());
        }
        private async Task EditUserAsync()
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;
            try
            {
                await CoreMethods.PushPageModel<EditUserViewModel>();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
            }

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
                await CoreMethods.PushPageModel<SensorViewModel>(ambiente);
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
