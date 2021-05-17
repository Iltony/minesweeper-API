using MWEntities;
using MWPersistence;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("TestProject")]

namespace MWServices
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IServicesResourceManager _serviceResourceManager;

        public UserService(
            IUserRepository userRepository,
            IServicesResourceManager serviceResourceManager)
        {
            _userRepository = userRepository;
            _serviceResourceManager = serviceResourceManager;
        }

        public Task<bool> ExistsUserAsync(string username)
        {
            return _userRepository.ExistsUserAsync(username);
        }

        public async Task<User> RegisterUserAsync(string username, string firstName, string lastName, DateTime birthdate)
        {
            if (string.IsNullOrWhiteSpace(username) || (username?.Length <= 1))
            {
                throw new InvalidUsernameException(_serviceResourceManager.ResourceManager);
            }

            var isUsernameRegistered = await _userRepository.ExistsUserAsync(username);
            if (isUsernameRegistered)
            {
                throw new DuplicatedUsernameException(_serviceResourceManager.ResourceManager);
            }

            var userToRegister = BuildUser(username, firstName, lastName, birthdate);

            return await _userRepository.RegisterUserAsync(userToRegister);
        }

        internal static User BuildUser(string username, string firstName, string lastName, DateTime birthdate)
        {
            return new User
            {
                Username = username,
                FirstName = firstName,
                LastName = lastName,
                Birthdate = birthdate
            };
        }
    }
}
