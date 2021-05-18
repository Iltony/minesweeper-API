using AutoFixture;
using AutoMapper;
using FakeItEasy;
using MWEntities;
using MWServices;
using NUnit.Framework;
using System.Linq;

namespace TestProject
{
    [TestFixture]
    public class GameProcessorTests
    {
        private IBoardCreator _gameProcessor;
        private IMapper _mapper;
        private Fixture _fixture;

        [SetUp]
        public void SetUp() {
            _fixture = Utils.GetFixture();
            _mapper = A.Fake<IMapper>();
            _gameProcessor = new BoardCreator();
        }

        [TestCase(6, 8, 12)]
        [TestCase(4, 8, 3)]
        [TestCase(10, 5, 1)]
        [TestCase(10, 8, 0)]
        public void GenerateBoards_GeneratesTheCorrectNumberOfCells(int columns, int rows, int mines)
        {
            var expectedCells = columns * rows;
            var user = _fixture.Create<User>();
            var initialClickCell = _fixture.Build<Cell>()
                                            .With(c=>c.Column, 2)
                                            .With(c => c.Row, 2)
                                            .Create();


            var result = _gameProcessor.GenerateBoard(initialClickCell, user, columns, rows, mines);

            Assert.AreEqual(expectedCells, result.Cells.Count());
        }


        [TestCase(6, 8, 12)]
        [TestCase(4, 8, 3)]
        [TestCase(10, 5, 1)]
        [TestCase(10, 8, 0)]
        public void GenerateBoards_GeneratesTheCorrectNumberOfMines(int columns, int rows, int mines)
        {
            var user = _fixture.Create<User>();
            var initialClickCell = _fixture.Build<Cell>()
                                            .With(c => c.Column, 2)
                                            .With(c => c.Row, 2)
                                            .Create();


            var result = _gameProcessor.GenerateBoard(initialClickCell, user, columns, rows, mines);


            Assert.AreEqual(mines, result.Mines.Count());
        }

        [TestCase(6, 8, 12)]
        [TestCase(4, 8, 3)]
        [TestCase(10, 5, 1)]
        [TestCase(10, 8, 0)]
        [TestCase(10, 8, 80)]
        public void GenerateBoards_IncludeMinesInCells(int columns, int rows, int mines)
        {
            var user = _fixture.Create<User>();
            var initialClickCell = _fixture.Build<Cell>()
                                            .With(c => c.Column, 2)
                                            .With(c => c.Row, 2)
                                            .Create();



            var result = _gameProcessor.GenerateBoard(initialClickCell, user, columns, rows, mines);

            var minesInCells = result.Cells.Where(c => c.ItIsAMine == true).ToList();

            Assert.AreEqual(mines, minesInCells.Count());
        }

        [TestCase(6, 8, 12)]
        [TestCase(4, 8, 3)]
        [TestCase(10, 5, 1)]
        [TestCase(10, 8, 0)]
        [TestCase(10, 8, 80)]
        public void GenerateBoards_MatchBasicProperties(int columns, int rows, int mines)
        {
            var user = _fixture.Create<User>();
            var initialClickCell = _fixture.Build<Cell>()
                                            .With(c => c.Column, 4)
                                            .With(c => c.Row, 2)
                                            .Create();

            var result = _gameProcessor.GenerateBoard(initialClickCell, user, columns, rows, mines);

            Assert.AreEqual(GameStatus.Active, result.GameStatus);
            Assert.AreEqual(columns, result.Columns);
            Assert.AreEqual(rows, result.Rows);
            Assert.AreEqual(user, result.Owner);
            Assert.IsNull(result.Name);
            Assert.IsNotNull(result.Id);
            Assert.AreEqual(0, result.Milliseconds);
        }

        [TestCase(6, 8, 12)]
        [TestCase(4, 8, 3)]
        [TestCase(10, 5, 1)]
        [TestCase(10, 8, 0)]
        [TestCase(10, 8, 80)]
        public void GenerateBoards_IfUserisNull_HasNullOwner(int columns, int rows, int mines)
        {
            var initialClickCell = _fixture.Build<Cell>()
                                            .With(c => c.Column, 4)
                                            .With(c => c.Row, 2)
                                            .Create();

            var result = _gameProcessor.GenerateBoard(initialClickCell, null, columns, rows, mines);

            Assert.IsNull(result.Owner);
        }
    }
}
