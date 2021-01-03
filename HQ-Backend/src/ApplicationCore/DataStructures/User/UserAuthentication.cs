using HQ_Backend.src.ApplicationCore.DataStructures;

namespace HQ_Backend.ApplicationCore.DataStructures.User
{
    public class UserAuthentication : UserRegisterModel
    {
        public UserAuthentication()
        {
            
        }

        public UserAuthentication(UserRegisterModel userRegister, string token): base(userRegister)
        {
            this.Token = token;
        }

        public UserAuthentication(string email, string password, string firstName, string lastName, int idUserType, string token): base(email, password, firstName, lastName, idUserType)
        {
            this.Token = token;
            this.UserId = -1; // database ads new user if id < 0. If id > 0 --> update user info
        }

        public string Token { get; set; }
        public int UserId { get; set; }
    }
}
