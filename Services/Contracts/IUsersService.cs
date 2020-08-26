namespace eCommerceRestAPI.Services.Contracts
{
    using eCommerceRestAPI.Dtos.Input.Users;
    using eCommerceRestAPI.Models;
    using System.Threading.Tasks;

    public interface IUsersService
    {
        User AuthenticateUser(UserLoginDto loginCredentials);

        string GenerateJWTToken(User user);

        Task<bool> ValidateCurrencyCodeAsync(string currencyCode);

        Task RegisterUserAsync(UserRegisterDto userInfo);
    }
}
