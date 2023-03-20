using liquorApi.Exceptions;
using liquorApi.Context.Entities;
using liquorApi.Context;
using liquorApi.Models;

namespace liquorApi.Services;

    public interface IAuthenticationService
    {
        Dictionary<string, string> Authenticate(UserLoginDtoIn userLogin);

        Task<Dictionary<string, string>> Register(RegisterDtoIn registerDtoIn);       

        Dictionary<string, string> ResetPassword(string email);
    }

public class AuthenticationService : IAuthenticationService
{

    private readonly LicoresDbContext _context;

    private readonly JwtTokenProvider _jwtTokenProvider;


    public AuthenticationService(
        LicoresDbContext context,
        JwtTokenProvider jwtTokenProvider
    )
    {
        this._context = context;
        this._jwtTokenProvider = jwtTokenProvider;
    }


    public Dictionary<string, string> Authenticate(UserLoginDtoIn userLogin)
    {
        var user = this._context.Users.SingleOrDefault(u => u.Email == userLogin.Email);

        if (user is null || !BCrypt.Net.BCrypt.Verify(userLogin.Password, user.Password))
            throw new ApiExceptions("Email or password is incorrect");

        var response = new Dictionary<string, string>();

        
        string token = this._jwtTokenProvider.GenerateToken(user);

        response.Add("message", "Login successful");
        response.Add("BearerToken", "Bearer " + token);
        return response;
    }

    public async Task<Dictionary<string, string>> Register(RegisterDtoIn registerDtoIn)
    {
        if (this._context.Users.Any(u => u.Email == registerDtoIn.Email))
            throw new ApiExceptions("Email already exists");

        User user = new User();

        user.Email = registerDtoIn.Email;
        user.Password = BCrypt.Net.BCrypt.HashPassword(registerDtoIn.Password);

        this._context.Users.Add(user);
        await this._context.SaveChangesAsync();
        var response = new Dictionary<string, string>();

        response.Add("message", "registered");
        return response;
    }

    public Dictionary<string, string> ResetPassword(string email)
    {
        throw new NotImplementedException();
    }


}
