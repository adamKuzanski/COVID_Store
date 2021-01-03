namespace HQ_Backend.ApplicationCore.DataStructures.User
{
    public class UserLoginModel
    {
        public UserLoginModel()
        {

        }

        public UserLoginModel(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }


        public string Email { get; set; }
        public string Password { get; set; }
    }
}
