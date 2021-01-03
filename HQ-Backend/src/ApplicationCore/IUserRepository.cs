using System.Collections.Generic;
using System.Threading.Tasks;
using HQ_Backend.ApplicationCore.DataStructures.User;

namespace HQ_Backend.ApplicationCore
{
    public interface IUserRepository
    {
        public Task<bool> RegisterNewUser(UserRegisterModel userRegister);
        public Task<UserAuthentication> SignInUser(UserLoginModel userLogin);
        public Task<object> GetAllUsers();
    }
}
