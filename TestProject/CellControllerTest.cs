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
    public class CellControllerTest
    {
        private ICellController _controller;
        private ICellService _service;
        private ResourceManager _resourceManager;
        private IServicesResourceManager _servicesResourceManager;
        private Fixture _fixture;

        private const string MSG_INVALIDCELL = "InvalidCell";
        private const string MSG_GAMENOTSTARTED = "GameNotStarted";
        private const string MSG_DEFAULTERRORMESSAGE = "DefaultErrorMessage";
        
        [SetUp]
        public void SetUp()
        {
            _fixture = Utils.GetFixture();
            _service = A.Fake<ICellService>();

            _resourceManager = A.Fake<ResourceManager>();

            A.CallTo(() => _resourceManager.GetString(MSG_INVALIDCELL)).Returns(MSG_INVALIDCELL);
            A.CallTo(() => _resourceManager.GetString(MSG_GAMENOTSTARTED)).Returns(MSG_GAMENOTSTARTED);
            A.CallTo(() => _resourceManager.GetString(MSG_DEFAULTERRORMESSAGE)).Returns(MSG_DEFAULTERRORMESSAGE);

            _servicesResourceManager = new ServicesResourceManager(_resourceManager);
            _controller = new CellController(_service, _servicesResourceManager);
        }

        [Test]
        public async Task CheckAsync_WhenInvalidCellExceptionOcurrs_MustReturnError()
        {
            var request = new CellRequest
            {
                Board = _fixture.Create<Board>(),
                Cell = _fixture.Create<CellCoordinates>()
            };

            A.CallTo(() => _service.CheckAsync(A<Board>._, A<int>._, A<int>._))
                    .Throws(new InvalidCellException(_resourceManager));

            var result = await _controller.CheckAsync(request) as ErrorResponse;

            Assert.AreEqual("error", result.Status);
            Assert.AreEqual(MSG_INVALIDCELL, result.Message);
        }

        [Test]
        public async Task CheckAsync_GameNotStartedExceptionOcurrs_MustReturnError()
        {
            var request = new CellRequest
            {
                Board = _fixture.Create<Board>(),
                Cell = _fixture.Create<CellCoordinates>()
            };

            A.CallTo(() => _service.CheckAsync(A<Board>._, A<int>._, A<int>._))
                    .Throws(new GameNotStartedException(_resourceManager));

            var result = await _controller.CheckAsync(request) as ErrorResponse;

            Assert.AreEqual("error", result.Status);
            Assert.AreEqual(MSG_GAMENOTSTARTED, result.Message);
        }

        [Test]
        public async Task CheckAsync_WhenGenericExceptionOcurrs_MustReturnGenericError()
        {
            var request = new CellRequest
            {
                Board = _fixture.Create<Board>(),
                Cell = _fixture.Create<CellCoordinates>()
            };


            A.CallTo(() => _service.CheckAsync(A<Board>._, A<int>._, A<int>._))
                    .Throws(new Exception());

            var result = await _controller.CheckAsync(request) as ErrorResponse;

            Assert.AreEqual("error", result.Status);
            Assert.AreEqual(MSG_DEFAULTERRORMESSAGE, result.Message);
        }

        [Test]
        public async Task CheckAsync_WhenIsOK_MustReturnSuccess()
        {
            var board = _fixture.Create<Board>();
            var request = new CellRequest
            {
                Board = board,
                Cell = _fixture.Create<CellCoordinates>()
            };

            var col = _fixture.Create<int>();
            var row = _fixture.Create<int>();

            A.CallTo(() => _service.CheckAsync(A<Board>._, A<int>._, A<int>._)).Returns(board);

            var result = await _controller.CheckAsync(request) as SuccessResponse<Board>;

            Assert.AreEqual("success", result.Status);
            Assert.AreEqual(board, result.Data);
        }

        [Test]
        public async Task FlagAsync_WhenInvalidCellExceptionOcurrs_MustReturnError()
        {
            var request = new CellRequest{
                Board = _fixture.Create<Board>(),
                Cell = _fixture.Create<CellCoordinates>()
            };

            A.CallTo(() => _service.FlagAsync(A<Board>._, A<int>._, A<int>._))
                    .Throws(new InvalidCellException(_resourceManager));

            var result = await _controller.FlagAsync(request) as ErrorResponse;

            Assert.AreEqual("error", result.Status);
            Assert.AreEqual(MSG_INVALIDCELL, result.Message);
        }

        [Test]
        public async Task FlagAsync_WhenGameNotStartedExceptionOcurrs_MustReturnError()
        {
            var request = new CellRequest
            {
                Board = _fixture.Create<Board>(),
                Cell = _fixture.Create<CellCoordinates>()
            };

            A.CallTo(() => _service.FlagAsync(A<Board>._, A<int>._, A<int>._))
                    .Throws(new GameNotStartedException(_resourceManager));

            var result = await _controller.FlagAsync(request) as ErrorResponse;

            Assert.AreEqual("error", result.Status);
            Assert.AreEqual(MSG_GAMENOTSTARTED, result.Message);
        }
       
        [Test]
        public async Task FlagAsync_WhenGenericExceptionOcurrs_MustReturnGenericError()
        {
            var request = new CellRequest
            {
                Board = _fixture.Create<Board>(),
                Cell = _fixture.Create<CellCoordinates>()
            };

            A.CallTo(() => _service.FlagAsync(A<Board>._, A<int>._, A<int>._))
                    .Throws(new Exception());

            var result = await _controller.FlagAsync(request) as ErrorResponse;

            Assert.AreEqual("error", result.Status);
            Assert.AreEqual(MSG_DEFAULTERRORMESSAGE, result.Message);
        }

        [Test]
        public async Task FlagAsync_WhenIsOK_MustReturnSuccess()
        {
            var board = _fixture.Create<Board>();
            var request = new CellRequest
            {
                Board = board,
                Cell = _fixture.Create<CellCoordinates>()
            };

            A.CallTo(() => _service.FlagAsync(A<Board>._, A<int>._, A<int>._)).Returns(board);

            var result = await _controller.FlagAsync(request) as SuccessResponse<Board>;

            Assert.AreEqual("success", result.Status);
            Assert.AreEqual(board, result.Data);
        }
    }
}
