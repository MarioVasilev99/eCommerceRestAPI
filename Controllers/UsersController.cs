﻿namespace eCommerceRestAPI.Controllers
{
    using eCommerceRestAPI.Dtos.Input.Users;
    using eCommerceRestAPI.Models;
    using eCommerceRestAPI.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IUsersService usersService;

        public UsersController(ApplicationDbContext dbContext, IUsersService usersService)
        {
            this.dbContext = dbContext;
            this.usersService = usersService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody]UserLoginDto login)
        {
            IActionResult response = Unauthorized();

            User user = this.usersService.AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = this.usersService.GenerateJWTToken(login);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = user,
                });
            }

            return response;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]UserRegisterDto register)
        {
            //TODO: Create Username and password validation
            bool isCurrencyCodeValid = await this.usersService.ValidateCurrencyCodeAsync(register.CurrencyCode);

            //TODO: Simplify if else
            if (!isCurrencyCodeValid)
            {
                //TODO: Remove magic string
                return this.BadRequest("Currency code not valid.");
            }
            else if (string.IsNullOrEmpty(register.Username) ||
                     string.IsNullOrEmpty(register.Password))
            {
                //TODO: Remove magic string
                return this.BadRequest("The username or password is incorrect.");
            }
            else
            {
                await this.usersService.RegisterUserAsync(register);
                return this.Ok();
            }
        }
    }
}
