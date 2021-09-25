using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace iemobile.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        public ICommand CadastrarCommand { get; }
        public ICommand VoltarCommand { get; }
        public string Senha { get; set; }
        public string Login { get; set; }
        public string Nome { get; set; }
        public SignUpViewModel()
        {
            CadastrarCommand = new Command(async () => await CadastrarAsync());
            VoltarCommand = new Command(async () => await VoltarAsync());
        }


        protected async Task VoltarAsync()
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;
            try
            {
                await CoreMethods.PopPageModel();
                //await CoreMethods.PushPageModelWithNewNavigation<MainViewModel>((object)Login);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
            }
        }

        protected async Task CadastrarAsync()
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;
            try
            {
                await CoreMethods.PushPageModel<SuccessSignUpViewModel>();
                //await CoreMethods.PushPageModelWithNewNavigation<MainViewModel>((object)Login);
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
