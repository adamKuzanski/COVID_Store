using System.Threading.Tasks;
using HQ_Backend.ApplicationCore;
using HQ_Backend.ApplicationCore.DataStructures.User;
using HQ_Backend.src.ApplicationCore.DataStructures.ControllerResponses;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HQ_Backend.RESTApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegistrationController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserRegistrationController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        // POST api/UserRegistrationController/SignUp
        [HttpPost("SignUp")]
        public async Task<IActionResult> PostSignUp([FromBody] UserRegisterModel userRegister)
        {
            var isUserRegistrationComplete = this._userRepository.RegisterNewUser(userRegister);

            if (await isUserRegistrationComplete)
            {
                var token = this.GetToken();
                var newUser = new UserAuthentication(userRegister, token);
                var response = new Authentication(newUser, "SUCCESSFULLY SIGNED UP THE NEW USER");
                return new OkObjectResult(response);
            }
            else
            {
                var response = new Authentication(null, "FAILED TO ADD A NEW USER");
                return new BadRequestObjectResult(response);
            }
        }


        // POST api/UserRegistrationController/SignIn
        [HttpPost("SignIn")]
        public async Task<IActionResult> PostSignIn([FromBody] UserLoginModel userLogin)
        {
            var userSignIn = await this._userRepository.SignInUser(userLogin);
            if (userSignIn != null)
            {
                var token = this.GetToken();
                var newUser = new UserAuthentication(userSignIn, token);
                var response = new Authentication(newUser, "SUCCESSFULLY SIGNED IN THE USER");
                return new OkObjectResult(response);
            }
            else
            {
                var response = new Authentication(null, "FAILED TO ADD THE USER");
                return new BadRequestObjectResult(response);
            }
        }

        // POST api/UserRegistrationController/AllUsers
        [HttpGet("AllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var isUserRegistrationComplete = this._userRepository.GetAllUsers();

            if (await isUserRegistrationComplete != null)
            {
                return new OkObjectResult(isUserRegistrationComplete.Result);
            }

            return new BadRequestObjectResult(isUserRegistrationComplete);
        }


        private string GetToken()
        {
            return "fake-jwt-token";
        }
    }
}
