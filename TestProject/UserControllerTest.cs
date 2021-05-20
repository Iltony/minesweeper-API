using AutoFixture;
using FakeItEasy;
using minesweeper_API;
using minesweeper_API.Controllers;
using MWEntities;
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
        private const string MSG_INVALIDBIRTHDATE = "InvalidBirthDate";

        [SetUp]
        public void SetUp()
        {
            _fixture = Utils.GetFixture();
            _service = A.Fake<IUserService>();

            _resourceManager = A.Fake<ResourceManager>();

            A.CallTo(() => _resourceManager.GetString(MSG_USERREGISTERED)).Returns(MSG_USERREGISTERED);
            A.CallTo(() => _resourceManager.GetString(MSG_INVALIDUSERNAME)).Returns(MSG_INVALIDUSERNAME);
            A.CallTo(() => _resourceManager.GetString(MSG_DUPLICATEDUSERNAME)).Returns(MSG_DUPLICATEDUSERNAME);
            A.CallTo(() => _resourceManager.GetString(MSG_DEFAULTERRORMESSAGE)).Returns(MSG_DEFAULTERRORMESSAGE); ;
            A.CallTo(() => _resourceManager.GetString(MSG_INVALIDBIRTHDATE)).Returns(MSG_INVALIDBIRTHDATE);

            _servicesResourceManager = new ServicesResourceManager(_resourceManager);
            _userController = new UserController(_service, _servicesResourceManager);
        }

        [Test]
        public async Task ExistsUserAsync_WhenUserExist_MustReturnError()
        {
            var username = _fixture.Create<string>();
            var firstName = _fixture.Create<string>();
            var lastName = _fixture.Create<string>();
            var birthdate = DateTime.Now.AddYears(-12);

            var request = new RegisterRequest
            {
                Username = username,
                FirstName = firstName,
                LastName = lastName,
                Birthdate = birthdate
            };

            var user = UserService.BuildUser(username, firstName, lastName, birthdate);

            A.CallTo(() => _service.RegisterUserAsync(username, firstName, lastName, birthdate))
                    .Throws(new DuplicatedUsernameException(_resourceManager));

            var result = await _userController.RegisterAsync(request) as ErrorResponse;

            Assert.AreEqual("error", result.Status);
            Assert.AreEqual(MSG_DUPLICATEDUSERNAME, result.Message);
        }

        [Test]
        public async Task ExistsUserAsync_WhenBirthDateIsInvalid_MustReturnError()
        {
            var username = _fixture.Create<string>();
            var firstName = _fixture.Create<string>();
            var lastName = _fixture.Create<string>();
            var birthdate = DateTime.Now.AddYears(-9);

            var request = new RegisterRequest
            {
                Username = username,
                FirstName = firstName,
                LastName = lastName,
                Birthdate = birthdate
            };

            var user = UserService.BuildUser(username, firstName, lastName, birthdate);

            A.CallTo(() => _service.RegisterUserAsync(username, firstName, lastName, birthdate))
                    .Throws(new InvalidBirthDateException(_resourceManager));

            var result = await _userController.RegisterAsync(request) as ErrorResponse;

            Assert.AreEqual("error", result.Status);
            Assert.AreEqual(MSG_INVALIDBIRTHDATE, result.Message);
        }

        [Test]
        public async Task ExistsUserAsync_WhenUserIsNotValid_MustReturnError()
        {
            var username = _fixture.Create<string>();
            var firstName = _fixture.Create<string>();
            var lastName = _fixture.Create<string>();
            var birthdate = DateTime.Now.AddYears(-12);

            var request = new RegisterRequest
            {
                Username = username,
                FirstName = firstName,
                LastName = lastName,
                Birthdate = birthdate
            };

            var user = UserService.BuildUser(username, firstName, lastName, birthdate);

            A.CallTo(() => _service.RegisterUserAsync(username, firstName, lastName, birthdate))
                    .Throws(new InvalidUsernameException(_resourceManager));

            var result = await _userController.RegisterAsync(request) as ErrorResponse;

            Assert.AreEqual("error", result.Status);
            Assert.AreEqual(MSG_INVALIDUSERNAME, result.Message);
        }

        [Test]
        public async Task ExistsUserAsync_WhenAnErrorOcurrs_MustReturnGenericError()
        {
            var username = _fixture.Create<string>();
            var firstName = _fixture.Create<string>();
            var lastName = _fixture.Create<string>();
            var birthdate = DateTime.Now.AddYears(-12);

            var request = new RegisterRequest
            {
                Username = username,
                FirstName = firstName,
                LastName = lastName,
                Birthdate = birthdate
            };


            var user = UserService.BuildUser(username, firstName, lastName, birthdate);

            A.CallTo(() => _service.RegisterUserAsync(username, firstName, lastName, birthdate))
                    .Throws<Exception>();

            var result = await _userController.RegisterAsync(request) as ErrorResponse;

            Assert.AreEqual("error", result.Status);
            Assert.AreEqual(MSG_DEFAULTERRORMESSAGE, result.Message);
        }

        [Test]
        public async Task ExistsUserAsync_WhenUserIsRegistered_MustReturnSuccess()
        {
            var username = _fixture.Create<string>();
            var firstName = _fixture.Create<string>();
            var lastName = _fixture.Create<string>();
            var birthdate = DateTime.Now.AddYears(-12);

            var request = new RegisterRequest
            {
                Username = username,
                FirstName = firstName,
                LastName = lastName,
                Birthdate = birthdate
            };

            var user = UserService.BuildUser(username, firstName, lastName, birthdate);

            A.CallTo(() => _service.RegisterUserAsync(username, firstName, lastName, birthdate)).Returns(user);

            var result = await _userController.RegisterAsync(request) as SuccessResponse<User>;

            Assert.AreEqual("success", result.Status);
            Assert.AreEqual(MSG_USERREGISTERED, result.Message);
            Assert.AreEqual(user, result.Data);
        }



        [Test]
        public async Task GetBoardsAsync_WhenUserHasBoards_MustReturnSuccess()
        {
            var username = _fixture.Create<string>();
            var boards = _fixture.CreateMany<Board>().ToList();

            A.CallTo(() => _service.GetUserBoardsAsync(username)).Returns(boards);

            SuccessResponse<IList<Board>> result = (SuccessResponse<IList<Board>>) await _userController.GetUserBoardsAsync(username);

            Assert.AreEqual("success", result.Status);
            Assert.IsNull(result.Message);
            Assert.AreEqual(boards, result.Data);
        }


        [Test]
        public async Task GetBoardsAsync_WhenUserDoesntHaveBoards_MustReturnSuccess()
        {
            var username = _fixture.Create<string>();
            var boards = _fixture.CreateMany<Board>(0).ToList();

            A.CallTo(() => _service.GetUserBoardsAsync(username)).Returns(boards);

            SuccessResponse<IList<Board>> result = (SuccessResponse<IList<Board>>)await _userController.GetUserBoardsAsync(username);

            Assert.AreEqual("success", result.Status);
            Assert.IsNull(result.Message);
            Assert.AreEqual(boards, result.Data);
        }


        [Test]
        public async Task GetBoardsAsync_WhenUserIsInvalid_MustReturnError()
        {
            var username = _fixture.Create<string>();
            var boards = _fixture.CreateMany<Board>(0).ToList();

            A.CallTo(() => _service.GetUserBoardsAsync(username))
                .Throws(new InvalidUsernameException(_resourceManager));

            var result = await _userController.GetUserBoardsAsync(username) as ErrorResponse;

            Assert.AreEqual("error", result.Status);
            Assert.AreEqual(MSG_INVALIDUSERNAME, result.Message);
        }






        [Test]
        public async Task GetUserAsync_WhenUserExists_MustReturnSuccess()
        {
            var username = _fixture.Create<string>();
            var user = _fixture.Create<User>();

            A.CallTo(() => _service.GetUserAsync(username)).Returns(user);

            SuccessResponse<User> result = (SuccessResponse<User>)await _userController.GetUserAsync(username);

            Assert.AreEqual("success", result.Status);
            Assert.IsNull(result.Message);
            Assert.AreEqual(user, result.Data);
        }


        [Test]
        public async Task GetUserAsync_WhenUserDoesNotExists_MustReturnSuccess()
        {
            var username = _fixture.Create<string>();
            
            A.CallTo(() => _service.GetUserAsync(username)).Returns((User)null);

            SuccessResponse<User> result = (SuccessResponse<User>)await _userController.GetUserAsync(username);

            Assert.AreEqual("success", result.Status);
            Assert.IsNull(result.Data);
            Assert.IsNull(result.Message);
        }


        [Test]
        public async Task GetUserAsync_WhenErrorOcurrs_MustReturnDefaultError()
        {
            var username = _fixture.Create<string>();
            var errorMessge = _fixture.Create<string>();

            A.CallTo(() => _service.GetUserAsync(username))
                .Throws(new Exception(errorMessge));

            var result = await _userController.GetUserAsync(username) as ErrorResponse;

            Assert.AreEqual("error", result.Status);
            Assert.AreEqual(MSG_DEFAULTERRORMESSAGE, result.Message);
        }
    }
}
