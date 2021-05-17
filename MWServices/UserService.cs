using AutoMapper;
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
        private IMapper _iMapper;

        public UserService(
            IUserRepository userRepository,
            IServicesResourceManager serviceResourceManager,
            IMapper iMapper)
        {
            _userRepository = userRepository;
            _serviceResourceManager = serviceResourceManager;
            _iMapper = iMapper;
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

            if (DateTime.Now.AddDays(-10) <= birthdate){
                throw new InvalidBirthDateException(_serviceResourceManager.ResourceManager);
            }

            var userToRegister = BuildUser(username, firstName, lastName, birthdate);

            return await _userRepository.RegisterUserAsync(userToRegister);
        }

        public async Task<IList<Board>> GetUserBoardsAsync(string username)
        {
            var userExists = await ExistsUserAsync(username);
            if (!userExists)
            {
                throw new InvalidUsernameException(_serviceResourceManager.ResourceManager);
            }

            var persistibleResult = await _userRepository.GetUserBoardsAsync(username);
            var result = _iMapper.Map<IList<PersistibleBoard>, IList<Board>>(persistibleResult);

            return result;
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
