using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace iemobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand LoginCommand { get; }
        public ICommand EsqueceuSenhaCommand { get; }
        public ICommand CriarContaCommand { get; }
        public string Senha { get; set; }
        public string Login { get; set; }
        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await LoginAsync());
            CriarContaCommand = new Command(async () => await CriarContaAsync());
            EsqueceuSenhaCommand = new Command(async () => await EsqueceuSenhaAsync());
        }


        protected async Task LoginAsync()
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;
            ShowLoading();
            try
            {
                if (string.IsNullOrEmpty(Login))
                {
                    await DisplayAlert("Preencha o login!");
                }

                await CoreMethods.PushPageModelWithNewNavigation<MainViewModel>(Login);
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


        protected async Task CriarContaAsync()
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;
            try
            {
                await CoreMethods.PushPageModel<SignUpViewModel>();
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected async Task EsqueceuSenhaAsync()
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;
            try
            {
                await CoreMethods.PushPageModel<ForgotPasswordViewModel>();
            }
            finally
            {
                IsBusy = false;
            }
        }




    }
}
