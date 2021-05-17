using AutoFixture;
using FakeItEasy;
using MWEntities;
using MWPersistence;
using MWServices;
using NUnit.Framework;
using System;
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
        private Fixture _fixture;

        [SetUp]
        public void SetUp() {
            _fixture = Utils.GetFixture();

            _resourceManager = A.Fake<ResourceManager>();
            _repository = A.Fake<IUserRepository>();

            A.CallTo(() => _resourceManager.GetString(A<string>._)).Returns("The resource value");
            _servicesResourceManager = new ServicesResourceManager(_resourceManager);

            _service = new UserService(_repository, _servicesResourceManager);
        }

        [Test]
        public async Task ExistsUserAsync_WhenUserExist_MustReturnTrue()
        {
            var username = _fixture.Create<string>();

            // set true for any query in the repository
            A.CallTo(
                () => _repository.ExistsUserAsync(A<string>._)
            ).Returns(true);

            _service = new UserService(_repository, _servicesResourceManager);


            // calls the method
            var result = await _service.ExistsUserAsync(username);


            Assert.IsTrue(result);
        }

        [Test]
        public async Task ExistUserAsync_WhenUserDoesNotExist_MustReturFalse()
        {
            var username = _fixture.Create<string>();

            A.CallTo(
                () => _repository.ExistsUserAsync(A<string>._)
            ).Returns(false);


            _service = new UserService(_repository, _servicesResourceManager);
            var result = await _service.ExistsUserAsync(username);

            Assert.IsFalse(result);
        }




        [Test]
        public async Task RegisterUserAsync_WhenUserIsRegistered_MustReturnTheUser()
        {
            var username = _fixture.Create<string>();
            var firstName = _fixture.Create<string>();
            var lastName = _fixture.Create<string>();
            var birthdate = _fixture.Create<DateTime>();

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

            _service = new UserService(_repository, _servicesResourceManager);

            var result = await _service.RegisterUserAsync(username, firstName, lastName, birthdate);

            Assert.AreEqual(userCreated, result);
        }

        [Test]
        public void RegisterUserAsync_WhenUsernameIsEmpty_MustThrownInvalidUserNameException()
        {
            var username = string.Empty;
            
            A.CallTo(() => _repository.ExistsUserAsync(A<string>._)).Returns(true);

            _service = new UserService(_repository, _servicesResourceManager);

            // calls the method and validate if the exception is raised
            Assert.ThrowsAsync<InvalidUsernameException>(async () => await _service.RegisterUserAsync(
                                                                                            username,
                                                                                           _fixture.Create<string>(),
                                                                                           _fixture.Create<string>(),
                                                                                           _fixture.Create<DateTime>()));
        }

        [Test]
        public void RegisterUserAsync_WhenUsernameIstooShort_MustThrownInvalidUserNameException()
        {
            //The username must be greater than 1 character
            var username = string.Empty;

            A.CallTo(() => _repository.ExistsUserAsync(A<string>._)).Returns(true);

            _service = new UserService(_repository, _servicesResourceManager);

            // calls the method and validate if the exception is raised
            Assert.ThrowsAsync<InvalidUsernameException>(async () => await _service.RegisterUserAsync(
                                                                                            username,
                                                                                           _fixture.Create<string>(),
                                                                                           _fixture.Create<string>(),
                                                                                           _fixture.Create<DateTime>()));
        }




        [Test]
        public void BuildUser_CreateTheUser_WithTheCorrectUsername()
        {
            var username = _fixture.Create<string>();

            var result = UserService.BuildUser(
                    username,
                   _fixture.Create<string>(),
                   _fixture.Create<string>(),
                   _fixture.Create<DateTime>());

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
                    _fixture.Create<DateTime>());

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
                    _fixture.Create<DateTime>()) ; 

            Assert.AreEqual(lastName, result.LastName);
        }

        [Test]
        public void BuildUser_CreateTheUser_WithTheCorrectBirthDate()
        {
            var birthdate = _fixture.Create<DateTime>();

            var result = UserService.BuildUser(
                _fixture.Create<string>(),
                _fixture.Create<string>(),
                _fixture.Create<string>(), 
                birthdate);

            Assert.AreEqual(birthdate, result.Birthdate);
        }






    }
}
