namespace HQ_Backend.ApplicationCore.DataStructures.User
{
    public class UserRegisterModel : UserLoginModel
    {
        public UserRegisterModel() : base()
        {

        }


        public UserRegisterModel(UserRegisterModel userRegister): base(userRegister.Email, userRegister.Password)
        {
            this.FirstName = userRegister.FirstName;
            this.LastName = userRegister.LastName;
            this.IdUserType = userRegister.IdUserType;
        }


        public UserRegisterModel(string email, string password, string firstName, string lastName, int idUserType) : base (email, password)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.IdUserType = idUserType;
        }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int IdUserType { get; set; }
    }
}
