using System;
using System.Threading.Tasks;
using iemobile.Models;

namespace iemobile.Interfaces
{
    public interface IUserService
    {
        Task<UserRequest> Login(Login login);
        Task<bool> Create(NewUserRequest newUser);
    }
}
