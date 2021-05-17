using AutoFixture;
using FakeItEasy;
using minesweeper_API;
using minesweeper_API.Controllers;
using MWEntities;
using MWServices;
using NUnit.Framework;
using System;
using System.Resources;
using System.Threading.Tasks;

namespace TestProject
{
    [TestFixture]
    public class UserControllerTest
    {
        private IUserController _userController;

        private IUserService _service;
        private ResourceManager _resourceManager;
        private IServicesResourceManager _servicesResourceManager;
        private Fixture _fixture;


        private const string MSG_USERREGISTERED = "UserRegistered";
        private const string MSG_INVALIDUSERNAME = "InvalidUsername";
        private const string MSG_DUPLICATEDUSERNAME = "DuplicatedUsername";
        private const string MSG_DEFAULTERRORMESSAGE = "DefaultErrorMessage";

        [SetUp]
        public void SetUp()
        {
            _fixture = Utils.GetFixture();
            _service = A.Fake<IUserService>();

            _resourceManager = A.Fake<ResourceManager>();

            A.CallTo(() => _resourceManager.GetString(MSG_USERREGISTERED)).Returns(MSG_USERREGISTERED);
            A.CallTo(() => _resourceManager.GetString(MSG_INVALIDUSERNAME)).Returns(MSG_INVALIDUSERNAME);
            A.CallTo(() => _resourceManager.GetString(MSG_DUPLICATEDUSERNAME)).Returns(MSG_DUPLICATEDUSERNAME);
            A.CallTo(() => _resourceManager.GetString(MSG_DEFAULTERRORMESSAGE)).Returns(MSG_DEFAULTERRORMESSAGE);
            
            _servicesResourceManager = new ServicesResourceManager(_resourceManager);
            _userController = new UserController(_service, _servicesResourceManager);
        }

        [Test]
        public async Task ExistsUserAsync_WhenUserExist_MustReturnError()
        {
            var username = _fixture.Create<string>();
            var firstName = _fixture.Create<string>();
            var lastName = _fixture.Create<string>();
            var birthdate = _fixture.Create<DateTime>();

            var user = UserService.BuildUser(username, firstName, lastName, birthdate);

            A.CallTo(() => _service.RegisterUserAsync(username, firstName, lastName, birthdate))
                    .Throws(new DuplicatedUsernameException(_resourceManager));

            var result = await _userController.RegisterAsync(username, firstName, lastName, birthdate) as ErrorResponse;

            Assert.AreEqual("error", result.Status); 
            Assert.AreEqual(MSG_DUPLICATEDUSERNAME, result.Message);
        }

        [Test]
        public async Task ExistsUserAsync_WhenUserIsNotValid_MustReturnError()
        {
            var username = _fixture.Create<string>();
            var firstName = _fixture.Create<string>();
            var lastName = _fixture.Create<string>();
            var birthdate = _fixture.Create<DateTime>();

            var user = UserService.BuildUser(username, firstName, lastName, birthdate);

            A.CallTo(() => _service.RegisterUserAsync(username, firstName, lastName, birthdate))
                    .Throws(new InvalidUsernameException(_resourceManager));

            var result = await _userController.RegisterAsync(username, firstName, lastName, birthdate) as ErrorResponse;

            Assert.AreEqual("error", result.Status);
            Assert.AreEqual(MSG_INVALIDUSERNAME, result.Message);
        }

        [Test]
        public async Task ExistsUserAsync_WhenAnErrorOcurrs_MustReturnGenericError()
        {
            var username = _fixture.Create<string>();
            var firstName = _fixture.Create<string>();
            var lastName = _fixture.Create<string>();
            var birthdate = _fixture.Create<DateTime>();

            var user = UserService.BuildUser(username, firstName, lastName, birthdate);

            A.CallTo(() => _service.RegisterUserAsync(username, firstName, lastName, birthdate))
                    .Throws<Exception>();

            var result = await _userController.RegisterAsync(username, firstName, lastName, birthdate) as ErrorResponse;

            Assert.AreEqual("error", result.Status);
            Assert.AreEqual(MSG_DEFAULTERRORMESSAGE, result.Message);
        }




        [Test]
        public async Task ExistsUserAsync_WhenUserIsRegistered_MustReturnSuccess()
        {
            var username = _fixture.Create<string>();
            var firstName = _fixture.Create<string>();
            var lastName = _fixture.Create<string>();
            var birthdate = _fixture.Create<DateTime>();

            var user = UserService.BuildUser(username, firstName, lastName, birthdate);

            A.CallTo(() => _service.RegisterUserAsync(username, firstName, lastName, birthdate)).Returns(user);

            var result = await _userController.RegisterAsync(username, firstName, lastName, birthdate) as SuccessResponse<User>;

            Assert.AreEqual("success", result.Status);
            Assert.AreEqual(MSG_USERREGISTERED, result.Message);
            Assert.AreEqual(user, result.Data);
        }







    }
}
