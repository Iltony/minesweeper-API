using AutoFixture;
using AutoMapper;
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
    public class BoardControllerTest
    {
        private IBoardController _controller;
        private IBoardService _service;
        private ResourceManager _resourceManager;
        private IServicesResourceManager _servicesResourceManager;
        private Fixture _fixture;
        private IMapper _mapper;

        private const string MSG_BOARDSAVED= "BoardSaved";
        private const string MSG_BOARDRESUMED = "BoardResumed";
        private const string MSG_BOARDINITIALIZED = "BoardInitialized";
        private const string MSG_INVALIDBOARD = "InvalidBoard";
        private const string MSG_INVALIDBOARDFORCURRENTUSER = "InvalidBoardForCurrentUser";
        private const string MSG_INVALIDUSERNAME = "InvalidUsername";
        private const string MSG_INVALIDCELL= "InvalidCell";
        private const string MSG_DEFAULTERRORMESSAGE = "DefaultErrorMessage";
        
        [SetUp]
        public void SetUp()
        {
            _fixture = Utils.GetFixture();
            _service = A.Fake<IBoardService>();
            _mapper = A.Fake<IMapper>();

            _resourceManager = A.Fake<ResourceManager>();

            A.CallTo(() => _resourceManager.GetString(MSG_BOARDSAVED)).Returns(MSG_BOARDSAVED);
            A.CallTo(() => _resourceManager.GetString(MSG_BOARDRESUMED)).Returns(MSG_BOARDRESUMED);
            A.CallTo(() => _resourceManager.GetString(MSG_BOARDINITIALIZED)).Returns(MSG_BOARDINITIALIZED);
            A.CallTo(() => _resourceManager.GetString(MSG_INVALIDBOARD)).Returns(MSG_INVALIDBOARD);
            A.CallTo(() => _resourceManager.GetString(MSG_INVALIDBOARDFORCURRENTUSER)).Returns(MSG_INVALIDBOARDFORCURRENTUSER);
            A.CallTo(() => _resourceManager.GetString(MSG_INVALIDUSERNAME)).Returns(MSG_INVALIDUSERNAME);
            A.CallTo(() => _resourceManager.GetString(MSG_DEFAULTERRORMESSAGE)).Returns(MSG_DEFAULTERRORMESSAGE);
            A.CallTo(() => _resourceManager.GetString(MSG_INVALIDCELL)).Returns(MSG_INVALIDCELL);

            _servicesResourceManager = new ServicesResourceManager(_resourceManager);
            _controller = new BoardController(_service, _servicesResourceManager, _mapper);
        }


        [Test]
        public async Task SaveBoardsAsync_WhenInvalidBoardExceptionOcurrs_MustReturnError()
        {
            var board = _fixture.Create<Board>();

            A.CallTo(() => _service.SaveBoardAsync(A<Board>._))
                    .Throws(new InvalidBoardException(_resourceManager));

            var result = await _controller.SaveBoardAsync(board) as ErrorResponse;

            Assert.AreEqual("error", result.Status);
            Assert.AreEqual(MSG_INVALIDBOARD, result.Message);
        }

        [Test]
        public async Task SaveBoardsAsync_WhenAnErrorOcurrs_MustReturnGenericError()
        {
            var board = _fixture.Create<Board>();

            A.CallTo(() => _service.SaveBoardAsync(A<Board>._))
                    .Throws(new Exception());

            var result = await _controller.SaveBoardAsync(board) as ErrorResponse;

            Assert.AreEqual("error", result.Status);
            Assert.AreEqual(MSG_DEFAULTERRORMESSAGE, result.Message);
        }

        [Test]
        public async Task SaveBoardsAsync_WhenIsSaved_MustReturnSuccess()
        {
            var board = _fixture.Create<Board>();

            A.CallTo(() => _service.SaveBoardAsync(board)).Returns(board);

            var result = await _controller.SaveBoardAsync(board) as SuccessResponse<Board>;

            Assert.AreEqual("success", result.Status);
            Assert.AreEqual(MSG_BOARDSAVED, result.Message);
            Assert.AreEqual(board, result.Data);
        }


        [Test]
        public async Task ResumeAsync_WhenInvalidBoardExceptionOcurrs_MustReturnError()
        {
            var request = new ResumeRequest
            {
                BoardId = Guid.NewGuid(),
                CurrentUserName = _fixture.Create<string>()
            };


            A.CallTo(() => _service.ResumeAsync(A<Guid>._, A<string>._))
                    .Throws(new InvalidBoardException(_resourceManager));

            var result = await _controller.ResumeAsync(request) as ErrorResponse;

            Assert.AreEqual("error", result.Status);
            Assert.AreEqual(MSG_INVALIDBOARD, result.Message);
        }

        [Test]
        public async Task ResumeAsync_WhenInvalidBoardForCurrentUserExceptionOcurrs_MustReturnError()
        {
            var request = new ResumeRequest
            {
                BoardId = Guid.NewGuid(),
                CurrentUserName = _fixture.Create<string>()
            };

            A.CallTo(() => _service.ResumeAsync(A<Guid>._, A<string>._))
                    .Throws(new InvalidBoardForCurrentUserException(_resourceManager));

            var result = await _controller.ResumeAsync(request) as ErrorResponse;

            Assert.AreEqual("error", result.Status);
            Assert.AreEqual(MSG_INVALIDBOARDFORCURRENTUSER, result.Message);
        }

        [Test]
        public async Task ResumeAsync_WhenAnErrorOcurrs_MustReturnGenericError()
        {
            var request = new ResumeRequest
            {
                BoardId = Guid.NewGuid(),
                CurrentUserName = _fixture.Create<string>()
            };


            A.CallTo(() => _service.ResumeAsync(A<Guid>._, A<string>._))
                    .Throws(new Exception());

            var result = await _controller.ResumeAsync(request) as ErrorResponse;

            Assert.AreEqual("error", result.Status);
            Assert.AreEqual(MSG_DEFAULTERRORMESSAGE, result.Message);
        }

        [Test]
        public async Task ResumeAsync_WhenIsResumed_MustReturnSuccess()
        {
            var board = _fixture.Create<Board>();

            var request = new ResumeRequest
            {
                BoardId = Guid.NewGuid(),
                CurrentUserName = _fixture.Create<string>()
            };

            A.CallTo(() => _service.ResumeAsync(A<Guid>._, A<string>._)).Returns(board);

            var result = await _controller.ResumeAsync(request) as SuccessResponse<Board>;

            Assert.AreEqual("success", result.Status);
            Assert.AreEqual(MSG_BOARDRESUMED, result.Message);
            Assert.AreEqual(board, result.Data);
        }



        [Test]
        public async Task InitializeAsync_WhenInvalidUsernameExceptionOcurrs_MustReturnError()
        {
            var request = new InitializeRequest
            {
                InitialClickCell =  _fixture.Create<CellCoordinates>(),
                Username = _fixture.Create<string>(),
                Columns = _fixture.Create<int>(),
                Rows = _fixture.Create<int>(),
                Mines = _fixture.Create<int>()
            };

            A.CallTo(() => _service.InitializeAsync(A<Cell>._, A<string>._, A<int>._, A<int>._, A<int>._))
                    .Throws(new InvalidUsernameException(_resourceManager));

            var result = await _controller.InitializeAsync(request) as ErrorResponse;

            Assert.AreEqual("error", result.Status);
            Assert.AreEqual(MSG_INVALIDUSERNAME, result.Message);
        }


        [Test]
        public async Task InitializeAsync_WhenInvalidCellExceptionOcurrs_MustReturnError()
        {
            var request = new InitializeRequest
            {
                InitialClickCell = _fixture.Create<CellCoordinates>(),
                Username = _fixture.Create<string>(),
                Columns = _fixture.Create<int>(),
                Rows = _fixture.Create<int>(),
                Mines = _fixture.Create<int>()
            };


            A.CallTo(() => _service.InitializeAsync(A<Cell>._, A<string>._, A<int>._, A<int>._, A<int>._))
                    .Throws(new InvalidUsernameException(_resourceManager));

            var result = await _controller.InitializeAsync(request) as ErrorResponse;

            Assert.AreEqual("error", result.Status);
            Assert.AreEqual(MSG_INVALIDUSERNAME, result.Message);
        }

        [Test]
        public async Task InitializeAsync_WhenAnErrorOcurrs_MustReturnGenericError()
        {
            var request = new InitializeRequest
            {
                InitialClickCell = _fixture.Create<CellCoordinates>(),
                Username = _fixture.Create<string>(),
                Columns = _fixture.Create<int>(),
                Rows = _fixture.Create<int>(),
                Mines = _fixture.Create<int>()
            };

            A.CallTo(() => _service.InitializeAsync(A<Cell>._, A<string>._, A<int>._, A<int>._, A<int>._))
                    .Throws(new Exception());
            
            var result = await _controller.InitializeAsync(request) as ErrorResponse;

            Assert.AreEqual("error", result.Status);
            Assert.AreEqual(MSG_DEFAULTERRORMESSAGE, result.Message);
        }

        [Test]
        public async Task InitializeAsync_WhenIsSaved_MustReturnSuccess()
        {
            var board = _fixture.Create<Board>();
            var request = new InitializeRequest
            {
                InitialClickCell = _fixture.Create<CellCoordinates>(),
                Username = _fixture.Create<string>(),
                Columns = _fixture.Create<int>(),
                Rows = _fixture.Create<int>(),
                Mines = _fixture.Create<int>()
            };

            A.CallTo(() => _service.InitializeAsync(A<Cell>._, A<string>._, A<int>._, A<int>._, A<int>._))
                    .Returns(board);

            var result = await _controller.InitializeAsync(request) as SuccessResponse<Board>;

            Assert.AreEqual("success", result.Status);
            Assert.AreEqual(MSG_BOARDINITIALIZED, result.Message);
            Assert.AreEqual(board, result.Data);
        }


        [Test]
        public async Task InitializeAsync_WhenUsernameIsNotSet_DefaultIsNull()
        {
            var firstClickCell = _fixture.Create<CellCoordinates>();
            var columns = 5;
            var rows = 5;
            var mines = 3;
            var board = _fixture.Create<Board>();


            var request = new InitializeRequest
            {
                InitialClickCell = firstClickCell,
                Columns = columns,
                Rows = rows,
                Mines = mines
            };

            A.CallTo(() => _service.InitializeAsync(A<Cell>._, A<string>._, A<int>._, A<int>._, A<int>._))
                .Returns(board);

            A.CallTo(() => _mapper.Map<Cell>(A<CellCoordinates>._))
                .Returns(new Cell { Column = firstClickCell.Column, Row = firstClickCell.Row }); 

            var result = await _controller.InitializeAsync(request) as SuccessResponse<Board>;

            A.CallTo(() => _service.InitializeAsync(
                A<Cell>.That.Matches(i => i.Column == firstClickCell.Column && i.Row == firstClickCell.Row),
                A<string>.That.Matches(i => i == null),
                A<int>.That.Matches(i => i == columns),
                A<int>.That.Matches(i => i == rows),
                A<int>.That.Matches(i => i == mines))
            ).MustHaveHappenedOnceExactly();

        }


        [Test]
        public async Task InitializeAsync_WhenColumnsAreNotSet_DefaultIs10()
        {
            const int EXPECTEDDEFAULTNUMBER = 10;

            var firstClickCell = _fixture.Create<CellCoordinates>();
            var username = _fixture.Create<string>();
            var rows = 5;
            var mines = 3;
            var board = _fixture.Create<Board>();


            var request = new InitializeRequest
            {
                InitialClickCell = firstClickCell,
                Username = username,
                Rows = rows,
                Mines = mines
            };

            A.CallTo(() => _mapper.Map<Cell>(A<CellCoordinates>._))
                .Returns(new Cell { Column = firstClickCell.Column, Row = firstClickCell.Row });
            A.CallTo(() => _service.InitializeAsync(A<Cell>._, A<string>._, A<int>._, A<int>._, A<int>._))
                .Returns(board);

            var result = await _controller.InitializeAsync(request) as SuccessResponse<Board>;

            A.CallTo(() => _service.InitializeAsync(
                A<Cell>.That.Matches(i => i.Column == firstClickCell.Column && i.Row == firstClickCell.Row),
                A<string>.That.Matches(i=>i ==username),
                A<int>.That.Matches(i => i == EXPECTEDDEFAULTNUMBER), 
                A<int>.That.Matches(i => i == rows), 
                A<int>.That.Matches(i => i == mines))
            ).MustHaveHappenedOnceExactly();
        }



        [Test]
        public async Task InitializeAsync_WhenRowsAreNotSet_DefaultIs10()
        {
            const int EXPECTEDDEFAULTNUMBER = 10;

            var firstClickCell = _fixture.Create<CellCoordinates>();
            var username = _fixture.Create<string>();
            var columns = 5;
            var mines = 3;
            var board = _fixture.Create<Board>();

            var request = new InitializeRequest
            {
                InitialClickCell = firstClickCell,
                Username = username,
                Columns = columns,
                Mines = mines
            };

            A.CallTo(() => _mapper.Map<Cell>(A<CellCoordinates>._))
                .Returns(new Cell { Column = firstClickCell.Column, Row = firstClickCell.Row });

            A.CallTo(() => _service.InitializeAsync(A<Cell>._, A<string>._, A<int>._, A<int>._, A<int>._))
                .Returns(board);

            var result = await _controller.InitializeAsync(request) as SuccessResponse<Board>;

            A.CallTo(() => _service.InitializeAsync(
                A<Cell>.That.Matches(i => i.Column == firstClickCell.Column && i.Row == firstClickCell.Row),
                A<string>.That.Matches(i => i == username),
                A<int>.That.Matches(i => i == columns),
                A<int>.That.Matches(i => i == EXPECTEDDEFAULTNUMBER),
                A<int>.That.Matches(i => i == mines))
            ).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task InitializeAsync_WhenMinesAreNotSet_DefaultIs10()
        {
            const int EXPECTEDDEFAULTNUMBER = 10;

            var firstClickCell = _fixture.Create<CellCoordinates>();
            var username = _fixture.Create<string>();
            var columns = 5;
            var rows = 3;
            var board = _fixture.Create<Board>();


            var request = new InitializeRequest
            {
                InitialClickCell = firstClickCell,
                Username = username,
                Columns = columns,
                Rows = rows
            };

            A.CallTo(() => _mapper.Map<Cell>(A<CellCoordinates>._))
                .Returns(new Cell { Column = firstClickCell.Column, Row = firstClickCell.Row });
            A.CallTo(() => _service.InitializeAsync(A<Cell>._, A<string>._, A<int>._, A<int>._, A<int>._))
                    .Returns(board);

            var result = await _controller.InitializeAsync(request) as SuccessResponse<Board>;

            A.CallTo(() => _service.InitializeAsync(
                A<Cell>.That.Matches(i => i.Column == firstClickCell.Column && i.Row == firstClickCell.Row),
                A<string>.That.Matches(i => i == username),
                A<int>.That.Matches(i => i == columns),
                A<int>.That.Matches(i => i == rows),
                A<int>.That.Matches(i => i == EXPECTEDDEFAULTNUMBER))
            ).MustHaveHappenedOnceExactly();
        }

    }
}
