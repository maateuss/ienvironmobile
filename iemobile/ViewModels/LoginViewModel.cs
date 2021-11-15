using System;
using System.Threading.Tasks;
using System.Windows.Input;
using iemobile.Interfaces;
using iemobile.Models;
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
        private readonly IUserService userService;
        
        public LoginViewModel(IUserService userService)
        {
            this.userService = userService;
            LoginCommand = new Command(async () => await LoginAsync());
            CriarContaCommand = new Command(async () => await CriarContaAsync());
            EsqueceuSenhaCommand = new Command(async () => await EsqueceuSenhaAsync());
            Login = preferences.User;
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
                if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Senha))
                {
                    await DisplayAlert("Preencha o login e a senha!");
                    return;
                }

                var login = new Login { login = Login, password = Senha };

                var result = await userService.Login(login);

                if(result == null)
                {
                    await DisplayAlert("Usuário ou senha inválidos!");
                    return;
                }

                preferences.Token = result.Token;
                preferences.RefreshKey = result.RefreshToken;
                preferences.Username = result.UserData.Name;
                preferences.User = result.UserData.Login;

                var mqttServer = await userService.FetchMqttBrokerUrl();

                Settings.MqttEndpoint = mqttServer;

                if (result != null)
                {
                    await CoreMethods.PushPageModelWithNewNavigation<MainViewModel>(Login);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Usuário ou senha inválidos!");
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
