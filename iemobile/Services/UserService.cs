using System;
using System.Diagnostics;
using System.Threading.Tasks;
using iemobile.Interfaces;
using iemobile.Models;

namespace iemobile.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService() : base("User")
        {
        }

        public async Task<bool> Create(NewUserRequest newUser)
        {
            try
            {
                var response = await PostToWebApi("create", newUser);

                if(response != null)
                {
                    return response.IsSuccessStatusCode;
                }
                  
                return false;
            }
            catch
            {
                Debug.WriteLine("Failed to create");
                return false;
            }
        }

        public async Task<string> FetchMqttBrokerUrl()
        {
            var result =  await GetFromWebApi<MqttBrokerEndpoint>("FetchMqttServerAddress");
            if(result != null)
            {
                return result.MqttBrokerAddress;
            }

            return "127.0.0.1";

        }

        public async Task<UserRequest> Login(Login login)
        {
            try
            {
                var response = await PostToWebApi<UserRequest>("login", login);

                SetToken(response.Token);
                
                return response;
            }
            catch
            {
                Debug.WriteLine("Failed to login");
                return null;
            }
        }


    }
}
