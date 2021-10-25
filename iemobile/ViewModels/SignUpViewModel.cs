using System;
using System.Threading.Tasks;
using System.Windows.Input;
using iemobile.Interfaces;
using iemobile.Models;
using Xamarin.Forms;

namespace iemobile.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        public ICommand CadastrarCommand { get; }
        public ICommand VoltarCommand { get; }
        public string Senha { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        private readonly IUserService userService;
        public SignUpViewModel(IUserService service)
        {
            userService = service;
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
                var requestObject = new NewUserRequest
                {
                    Email = Email,
                    Login = Login,
                    Password = Senha,
                    Name = Name
                };

                var result = await userService.Create(requestObject);
                if(result)
                await CoreMethods.PushPageModel<SuccessSignUpViewModel>();
                else
                {
                    await DisplayAlert("Erro", "Não foi possível criar usuário, verifique as propriedades e a rede e tente novamente", "ok");
                }

                //await CoreMethods.PushPageModelWithNewNavigation<MainViewModel>((object)Login);
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
