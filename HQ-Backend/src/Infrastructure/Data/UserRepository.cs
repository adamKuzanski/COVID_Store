using HQ_Backend.src.ApplicationCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using HQ_Backend.ApplicationCore;
using HQ_Backend.ApplicationCore.DataStructures.User;
using HQ_Backend.Secrets;
using HQ_Backend.src.ApplicationCore.DataStructures;

namespace HQ_Backend.src.Infrastructure.Data
{
    public class UserRepository: IUserRepository
    {
        private const string ConnectionString = Connections.DbConnection;
        private const int RegisterNewUserConst = 0;

        public UserRepository()
        {

        }

        public async Task<bool> RegisterNewUser(UserRegisterModel userRegister)
        {
            await using var sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            var sqlCommand = new SqlCommand("RegisterNewUser", sqlConnection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            sqlCommand.Parameters.AddWithValue("@ID_User", Convert.ToInt32(RegisterNewUserConst));
            sqlCommand.Parameters.AddWithValue("@First_Name", userRegister.FirstName.Trim());
            sqlCommand.Parameters.AddWithValue("@Last_Name", userRegister.LastName.Trim());
            sqlCommand.Parameters.AddWithValue("@Email", userRegister.Email.Trim());
            sqlCommand.Parameters.AddWithValue("@Password", userRegister.Password.Trim());
            sqlCommand.Parameters.AddWithValue("@ID_User_Type", Convert.ToInt32(userRegister.IdUserType));
            var rowsAffected = sqlCommand.ExecuteNonQuery();

            return rowsAffected > 0;
        }

        public async Task<UserAuthentication> SignInUser(UserLoginModel userLogin)
        {
            await using var sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            var sqlAdapter = new SqlDataAdapter("SignInUser", sqlConnection);
            sqlAdapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

            sqlAdapter.SelectCommand.Parameters.AddWithValue("@Email", userLogin.Email.Trim());
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@Password", userLogin.Password.Trim());

            var dataTable = new DataTable();

            try
            {
                sqlAdapter.Fill(dataTable);
            }
            catch (Exception)
            {
                return null;
            }


            var finalUser = new UserAuthentication();
            finalUser.Email = userLogin.Email;
            finalUser.Password = userLogin.Password;
            finalUser.UserId = int.Parse(dataTable.Rows[0][0].ToString());
            finalUser.FirstName = dataTable.Rows[0][1].ToString();
            finalUser.LastName = dataTable.Rows[0][2].ToString();
            finalUser.IdUserType = int.Parse(dataTable.Rows[0][5].ToString());

            return finalUser;
        }

        public async Task<object> GetAllUsers()
        {
            await using var sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            var sqlAdapter = new SqlDataAdapter("GetAllUsers", sqlConnection);
            sqlAdapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            
            var dataTable = new DataTable();


            try
            {
                sqlAdapter.Fill(dataTable);
            }
            catch (Exception)
            {
                return null;
            }

            var users = dataTable.AsEnumerable().Select(n => n.ItemArray).ToList();

            return users;
        }
    }
}
