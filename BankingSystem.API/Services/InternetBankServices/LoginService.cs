using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositories.DTOs;
using Repositories.InternetBankRepositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Services.InternetBankServices
{
    public interface ILoginService
    {
        Task<SignInResult> LogIn(LoginDto login);
        Task<string> JWTToken(LoginDto login);
    }

    public class LoginService : ILoginService
    {
        private readonly IConfiguration _configuration;
        private readonly ILoginRepository _loginRepository;

        public LoginService(
            IConfiguration configuration, ILoginRepository loginRepository)
        {
            _configuration = configuration;
            _loginRepository = loginRepository;
        }

        public async Task<SignInResult> LogIn(LoginDto login)
        {
            try
            {
                var checkUsernameResult = await _loginRepository.CheckUsernameInDb(login.Email);

                if (checkUsernameResult == null)
                {
                    return new SignInResult();
                }

                var signInUser = await _loginRepository.SignIn(checkUsernameResult, login.Password);

                if (signInUser == null)
                {
                    return new SignInResult();
                }

                return signInUser;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<string> JWTToken(LoginDto login)
        {
            var getIdentityUser = await _loginRepository.CheckUsernameInDb(login.Email);

            var userRoles = await _loginRepository.GetUserRoles(getIdentityUser);

            var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, getIdentityUser.Id),
                    new Claim(ClaimTypes.Name, login.Email),
                    new Claim(ClaimTypes.Role, userRoles.FirstOrDefault()),
                };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            var result = new JwtSecurityTokenHandler().WriteToken(token);

            return result;
        }
    }
}
