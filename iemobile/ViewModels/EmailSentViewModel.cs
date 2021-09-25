using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace iemobile.ViewModels
{
    public class EmailSentViewModel : BaseViewModel
    {
        public ICommand VoltarCommand { get; }

        public EmailSentViewModel()
        {
            VoltarCommand = new Command(async () => await CoreMethods.PushPageModelWithNewNavigation<LoginViewModel>(null));
        }
    }
}
