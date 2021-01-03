using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HQ_Backend.ApplicationCore.DataStructures.User;

namespace HQ_Backend.src.ApplicationCore.DataStructures.ControllerResponses
{
    public class Authentication
    {
        public Authentication()
        {

        }

        public Authentication(UserAuthentication user, string message)
        {
            User = user;
            Message = message;
        }

        public UserAuthentication User { get; set; }
        public string Message { get; set; }
    }
}
