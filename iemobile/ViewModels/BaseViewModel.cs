using System;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Acr.UserDialogs;
using FreshMvvm;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace iemobile.ViewModels
{
    public class BaseViewModel : FreshBasePageModel
    {
        public bool IsBusy { get; set; }
        public PreferencesAccessLayer preferences;
        protected IUserDialogs PageDialog = UserDialogs.Instance;

        public BaseViewModel()
        {
            preferences = new PreferencesAccessLayer();
        }

        protected Task DisplayAlert(string title, string message, string cancel)
        {
            return CoreMethods.DisplayAlert(title, message, cancel);
        }

        protected Task DisplayAlert(string message)
        {
            return CoreMethods.DisplayAlert("", message, "OK");
        }

        protected Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
        {
            return CoreMethods.DisplayActionSheet(title, cancel, destruction, buttons);
            //Acr.UserDialogs.UserDialogs.Instance.
        }

        protected Task<bool> Prompt(string title, string message, string accept, string cancel)
        {
            return App.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

        protected void ToastShort(string message)
        {
            PageDialog.Toast(message, TimeSpan.FromSeconds(3));
        }

        protected void ToastError(string message)
        {
            var cfg = new ToastConfig(message)
            {
                BackgroundColor = System.Drawing.Color.Red,
                Duration = TimeSpan.FromSeconds(3)
            };

            PageDialog.Toast(cfg);
        }

        protected void ToastLong(string message)
        {
            PageDialog.Toast(message, TimeSpan.FromSeconds(5));
        }

        protected void ToastSuccess(string message, int seconds)
        {
            var cfg = new ToastConfig(message)
            {
                BackgroundColor = System.Drawing.Color.Green,
                Duration = TimeSpan.FromSeconds(seconds)
            };

            PageDialog.Toast(cfg);
        }

        protected void ShowLoading(string message = null)
        {
            PageDialog.ShowLoading(message);
        }

        protected void HideLoading()
        {
            PageDialog.HideLoading();
        }

        protected async Task RunSafe(Task task, bool ShowLoading = true, string loadingMessage = null)
        {

                if (IsBusy)
                {
                    return;
                }
                else
                {
                    IsBusy = true;
                }

            try
            {
                if (ShowLoading)
                {
                    PageDialog.ShowLoading(loadingMessage ?? "Carregando");
                }

                await task;
            }
            catch (Exception ex)
            {
                IsBusy = false;
                HideLoading();
                Debug.WriteLine(ex.ToString());
                ToastError("Erro ao executar operação");
            }
            finally
            {
                IsBusy = false;
                if (ShowLoading)
                {
                    HideLoading();
                }
            }
        }

        protected async Task<T> DeserializeJson<T>(HttpResponseMessage apiResponse) where T : class
        {
            var responseStream = await apiResponse.Content.ReadAsStreamAsync().ConfigureAwait(false);
            var json = JsonConvert.DeserializeObject<T>(await new StreamReader(responseStream).ReadToEndAsync().ConfigureAwait(false));
            return json;
        }


    }
}
