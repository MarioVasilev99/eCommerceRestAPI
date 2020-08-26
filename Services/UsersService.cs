namespace eCommerceRestAPI.Services
{
    using eCommerceRestAPI.Dtos.Input.Users;
    using eCommerceRestAPI.Models;
    using eCommerceRestAPI.Services.Contracts;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Net.Http;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public class UsersService : IUsersService
    {
        private string ValidateCurrencyCodeUrl = "https://api.exchangeratesapi.io/latest?base=";

        private readonly ApplicationDbContext dbContext;
        private readonly IConfiguration configuration;

        public UsersService(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
        }

        //This method tries to find a user in the db. If not found returns null.
        public User AuthenticateUser(UserLoginDto loginCredentials)
        {
            return dbContext
                .Users
                .FirstOrDefault(u =>
                    u.Username == loginCredentials.Username &&
                    u.Password == loginCredentials.Password);
        }

        // This method generates a JWT Token string.
        public string GenerateJWTToken(UserLoginDto userInfo)
        {
            var jwtSection = this.configuration.GetSection("JWTSettings");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection.GetSection("SecretKey").Value));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

            };

            var token = new JwtSecurityToken(
            issuer: jwtSection.GetSection("Issuer").Value,
            audience: jwtSection.GetSection("Audience").Value,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> ValidateCurrencyCodeAsync(string currencyCode)
        {
            string url = this.ValidateCurrencyCodeUrl + currencyCode;
            bool isValid = false;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        isValid = true;
                    }
                }
            }

            return isValid;
        }

        public async Task RegisterUserAsync(UserRegisterDto userInfo)
        {
            User newUser = new User()
            {
                Username = userInfo.Username,
                Password = userInfo.Password,
                CurrencyCode = userInfo.CurrencyCode,
            };

            await this.dbContext.Users.AddAsync(newUser);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
