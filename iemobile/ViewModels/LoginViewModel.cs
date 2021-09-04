using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace iemobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand LoginCommand { get; }
        public string Senha { get; set; }
        public string Login { get; set; }
        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await LoginAsync());
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

                await CoreMethods.PushPageModelWithNewNavigation<MainViewModel>((object)Login);
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
