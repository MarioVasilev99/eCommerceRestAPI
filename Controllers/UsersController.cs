namespace eCommerceRestAPI.Controllers
{
    using eCommerceRestAPI.Dtos.Input.Users;
    using eCommerceRestAPI.Helpers;
    using eCommerceRestAPI.Models;
    using eCommerceRestAPI.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [ApiController]
    [Route(RoutesHelper.UserController)]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IUsersService usersService;

        public UsersController(ApplicationDbContext dbContext, IUsersService usersService)
        {
            this.dbContext = dbContext;
            this.usersService = usersService;
        }

        [HttpPost(RoutesHelper.UserLogin)]
        [AllowAnonymous]
        public IActionResult Login([FromBody]UserLoginDto login)
        {
            IActionResult response = Unauthorized();

            User user = this.usersService.AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = this.usersService.GenerateJWTToken(user);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = user,
                });
            }

            return response;
        }

        [HttpPost(RoutesHelper.UserRegister)]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]UserRegisterDto register)
        {
            //TODO: Create Username and password validation
            bool isCurrencyCodeValid = await this.usersService.ValidateCurrencyCodeAsync(register.CurrencyCode);

            //TODO: Simplify if else
            if (!isCurrencyCodeValid)
            {
                return this.BadRequest(ExceptionsHelper.CurrencyCodeNotValid);
            }
            else if (string.IsNullOrEmpty(register.Username) ||
                     string.IsNullOrEmpty(register.Password))
            {
                return this.BadRequest(ExceptionsHelper.UsernamePasswordIncorrect);
            }
            else
            {
                await this.usersService.RegisterUserAsync(register);
                return this.Ok();
            }
        }
    }
}
