using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace iemobile.ViewModels
{
    public class EditUserViewModel : BaseViewModel
    {
        public ICommand GoBackCommand { get; }
        public ICommand EditUserCommand { get; }

        public EditUserViewModel()
        {
            GoBackCommand = new Command(async () => await CoreMethods.PopPageModel());
            EditUserCommand = new Command(async () => await CoreMethods.PopPageModel());
        }


    }
}
