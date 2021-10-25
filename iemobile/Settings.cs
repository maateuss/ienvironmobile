using System;
using Xamarin.Essentials;

namespace iemobile
{
    public static class Settings
    {
        public static string BackEndBaseUrl = "https://ienvironment.azurewebsites.net";
        public static string MqttEndpoint = "localhost";
        public static int MqttPort = 1883;
    }


    public class PreferencesAccessLayer
    {
        public string User
        {
            get
            {
                return Preferences.Get("user", string.Empty);
            }

            set
            {
                Preferences.Set("user", value);
            }
        }

        public string Username
        {
            get
            {
                return Preferences.Get("username", string.Empty);
            }

            set
            {
                Preferences.Set("username", value);
            }
        }


        public string RefreshKey
        {
            get
            {
                return Preferences.Get("refreshkey", string.Empty);
            }
            set
            {
                Preferences.Set("refreshkey", value);
            }
        }

        public string Token
        {
            get
            {
                return Preferences.Get("token", string.Empty);
            }
            set
            {
                Preferences.Set("token", value);
            }
        }

        public string MqttEndpoint
        {
            get
            {
                return Preferences.Get("MqttEndpoint", string.Empty);
            }
            set
            {
                Preferences.Set("MqttEndpoint", value);
            }
        }


        public int MqttPort
        {
            get
            {
                return Preferences.Get("MqttPort", 0);
            }
            set
            {
                Preferences.Set("MqttPort", value);
            }
        }
    }
}
