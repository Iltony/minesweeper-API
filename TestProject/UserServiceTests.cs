using AutoFixture;
using AutoMapper;
using FakeItEasy;
using MWEntities;
using MWPersistence;
using MWServices;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

namespace TestProject
{
    [TestFixture]
    public class UserServiceTests
    {
        private IUserService _service;
        private IUserRepository _repository;
        private ResourceManager _resourceManager;
        private IServicesResourceManager _servicesResourceManager;
        private IMapper _mapper;
        private Fixture _fixture;

        [SetUp]
        public void SetUp() {
            _fixture = Utils.GetFixture();

            _resourceManager = A.Fake<ResourceManager>();
            _repository = A.Fake<IUserRepository>();
            _mapper = A.Fake<IMapper>();

            A.CallTo(() => _resourceManager.GetString(A<string>._)).Returns("The resource value");
            _servicesResourceManager = new ServicesResourceManager(_resourceManager);

            _service = new UserService(_repository, _servicesResourceManager, _mapper);
        }

        [Test]
        public async Task ExistsUserAsync_WhenUserExist_MustReturnTrue()
        {
            var username = _fixture.Create<string>();

            // set true for any query in the repository
            A.CallTo(
                () => _repository.ExistsUserAsync(A<string>._)
            ).Returns(true);

            _service = new UserService(_repository, _servicesResourceManager, _mapper);


            // calls the method
            var result = await _service.ExistsUserAsync(username);


            Assert.IsTrue(result);
        }

        [Test]
        public async Task ExistUserAsync_WhenUserDoesNotExist_MustReturnFalse()
        {
            var username = _fixture.Create<string>();

            A.CallTo(
                () => _repository.ExistsUserAsync(A<string>._)
            ).Returns(false);


            _service = new UserService(_repository, _servicesResourceManager, _mapper);
            var result = await _service.ExistsUserAsync(username);

            Assert.IsFalse(result);
        }

        
        [Test]
        public async Task RegisterUserAsync_WhenUserIsRegistered_MustReturnTheUser()
        {
            var username = _fixture.Create<string>();
            var firstName = _fixture.Create<string>();
            var lastName = _fixture.Create<string>();
            var birthdate = DateTime.Now.AddYears(-12);

            var userCreated =
                _fixture.Build<User>()
                    .With(u => u.Username, username)
                    .With(u => u.FirstName, firstName)
                    .With(u => u.LastName, lastName)
                    .With(u => u.Birthdate, birthdate)
                    .Create();

            A.CallTo(
                () => _repository.RegisterUserAsync(A<User>._)
            ).Returns(userCreated);

            _service = new UserService(_repository, _servicesResourceManager, _mapper);

            var result = await _service.RegisterUserAsync(username, firstName, lastName, birthdate);

            Assert.AreEqual(userCreated, result);
        }

        [Test]
        public void RegisterUserAsync_WhenUsernameIsEmpty_MustThrownInvalidUserNameException()
        {
            var username = string.Empty;
            
            A.CallTo(() => _repository.ExistsUserAsync(A<string>._)).Returns(true);

            _service = new UserService(_repository, _servicesResourceManager, _mapper);

            // calls the method and validate if the exception is raised
            Assert.ThrowsAsync<InvalidUsernameException>(async () => await _service.RegisterUserAsync(
                                                                                            username,
                                                                                           _fixture.Create<string>(),
                                                                                           _fixture.Create<string>(),
                                                                                           DateTime.Now.AddYears(-12)));
        }

        [Test]
        public void RegisterUserAsync_WhenUsernameIstooShort_MustThrownInvalidUserNameException()
        {
            //The username must be greater than 1 character
            var username = string.Empty;

            A.CallTo(() => _repository.ExistsUserAsync(A<string>._)).Returns(true);

            _service = new UserService(_repository, _servicesResourceManager, _mapper);

            // calls the method and validate if the exception is raised
            Assert.ThrowsAsync<InvalidUsernameException>(async () => await _service.RegisterUserAsync(
                                                                                            username,
                                                                                           _fixture.Create<string>(),
                                                                                           _fixture.Create<string>(),
                                                                                           DateTime.Now.AddYears(-12)));
        }




        [Test]
        public void BuildUser_CreateTheUser_WithTheCorrectUsername()
        {
            var username = _fixture.Create<string>();

            var result = UserService.BuildUser(
                    username,
                   _fixture.Create<string>(),
                   _fixture.Create<string>(),
                   DateTime.Now.AddYears(-12));

            Assert.AreEqual(username, result.Username);
        }

        [Test]
        public void BuildUser_CreateTheUser_WithTheCorrectFirstname()
        {
            var firstName = _fixture.Create<string>();


            var result = UserService.BuildUser(
                    _fixture.Create<string>(),
                    firstName,
                    _fixture.Create<string>(), 
                    DateTime.Now.AddYears(-12));

            Assert.AreEqual(firstName, result.FirstName);
        }

        [Test]
        public void BuildUser_CreateTheUser_WithTheCorrectLastName()
        {
            var lastName = _fixture.Create<string>();

            var result = UserService.BuildUser(
                    _fixture.Create<string>(),
                    _fixture.Create<string>(),
                    lastName,
                    DateTime.Now.AddYears(-12)) ; 

            Assert.AreEqual(lastName, result.LastName);
        }

        [Test]
        public void BuildUser_CreateTheUser_WithTheCorrectBirthDate()
        {
            var birthdate = DateTime.Now.AddYears(-12);

            var result = UserService.BuildUser(
                _fixture.Create<string>(),
                _fixture.Create<string>(),
                _fixture.Create<string>(), 
                birthdate);

            Assert.AreEqual(birthdate, result.Birthdate);
        }

        [Test]
        public void GetUserBoardsAsync_WhenUserIsInvalid_MustThrownInvalidUserNameException()
        {
            //The username must be greater than 1 character
            var username = string.Empty;

            _service = new UserService(_repository, _servicesResourceManager, _mapper);

            // calls the method and validate if the exception is raised
            Assert.ThrowsAsync<InvalidUsernameException>(async () => await _service.GetUserBoardsAsync(username));
        }

        [Test]
        public async Task GetUserBoardsAsync_WhenUserExists_DoesNotRaiseException()
        {
            var username = _fixture.Create<string>();
            
            A.CallTo(() => _repository.ExistsUserAsync(A<string>._)).Returns(false);

            // calls the method and validate if the exception is raised
            Assert.ThrowsAsync<InvalidUsernameException>(async () => await _service.GetUserBoardsAsync(username));
        }

        [Test]
        public async Task GetUserBoardsAsync_WhenHaveValue_ReturnsTheBoards()
        {
            var username = _fixture.Create<string>();
            var persistibleBoards = new List<PersistibleBoard> {
                _fixture.Create<PersistibleBoard>(),
                _fixture.Create<PersistibleBoard>(),
                _fixture.Create<PersistibleBoard>()
            };
                
            var boards = _fixture.CreateMany<Board>(persistibleBoards.Count).ToList();

            A.CallTo(() => _repository.ExistsUserAsync(A<string>._)).Returns(true);
            A.CallTo(() => _repository.GetUserBoardsAsync(A<string>._)).Returns(persistibleBoards);
            A.CallTo(
                () => _mapper.Map<IList<PersistibleBoard>, IList<Board>>(A<IList<PersistibleBoard>>._)
            ).Returns((IList<Board>)boards);

            _service = new UserService(_repository, _servicesResourceManager, _mapper);

            var result = await _service.GetUserBoardsAsync(username);

            Assert.AreEqual(boards.Count, result.Count);
         
        }
    }
}
