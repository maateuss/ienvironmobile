using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace iemobile.ViewModels
{
    public class SuccessSignUpViewModel : BaseViewModel
    {
        public ICommand VoltarCommand { get; }

        public SuccessSignUpViewModel()
        {
            VoltarCommand = new Command(async () => await CoreMethods.PushPageModelWithNewNavigation<LoginViewModel>(null));
        }
    }
}
