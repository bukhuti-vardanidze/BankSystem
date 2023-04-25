using DB.Entities;
using Microsoft.AspNetCore.Identity;
using Repositories.DTOs;
using Repositories.InternetBankRepositories;

namespace Services.InternetBankServices
{
    public interface IRegisterUserService
    {
        Task<(IdentityResult register, int userId)> RegisterUser(RegisterUserDto userRegistration);
    }
    public class RegisterUserService : IRegisterUserService
    {
        private readonly IRegisterUserRepository _registerUserRepository;

        public RegisterUserService( IRegisterUserRepository registerUserRepository)
        {   
            _registerUserRepository = registerUserRepository;
        }
        
        public async Task<(IdentityResult register, int userId)> RegisterUser(RegisterUserDto userRegistration)
        {
            try
            {
                var identityUserCheckResult = await _registerUserRepository.CheckIdentityUserInDb(userRegistration);

                if (identityUserCheckResult != null)
                {
                    return (new IdentityResult(),0);
                }

                var checkIdNumberResult = await _registerUserRepository.CheckIdNumberInDb(userRegistration);

                if (checkIdNumberResult)
                {
                    return (new IdentityResult(), 0);
                }

                var createIdentityUser = new IdentityUser()
                {
                    UserName = userRegistration.EmailAddress,
                    NormalizedUserName = userRegistration.EmailAddress.ToUpper(),
                    Email = userRegistration.EmailAddress,
                    NormalizedEmail = userRegistration.EmailAddress.ToUpper(),
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(
                        new IdentityUser { UserName = userRegistration.EmailAddress }, userRegistration.Password)
                };

                var createUserEntity = new UserEntity()
                {
                    Id = createIdentityUser.Id,
                    FirstName = userRegistration.FirstName,
                    LastName = userRegistration.LastName,
                    IDNumber = userRegistration.IDNumber,
                    BirthDate = userRegistration.BirthDate,
                    RegistrationDate = DateTime.Now
                };

                var connectUserToRole = new IdentityUserRole<string>()
                {
                    RoleId = "2",
                    UserId = createIdentityUser.Id
                };

                var identityResult = await _registerUserRepository.RegisterIdentityUser(createIdentityUser);
                var entityResult = await _registerUserRepository.RegisterUserEntity(createUserEntity);
                var identityRole = await _registerUserRepository.AddUserRole(connectUserToRole);

                return (identityResult, createUserEntity.UserId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }           
        }
    }
}
