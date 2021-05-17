using FakeItEasy;
using MWEntities;
using NUnit.Framework;
using System.Resources;

namespace TestProject
{
    [TestFixture]
    public class ExceptionsTests
    {
        private ResourceManager _resourceManager;
        const string ERRORMESSAGE = "The error message";

        [SetUp] 
        public void SetUp() { 
            _resourceManager = A.Fake<ResourceManager>();
            A.CallTo(() => _resourceManager.GetString(A<string>._)).Returns(ERRORMESSAGE);
        }

        [Test]
        public void DuplicatedUsernameException_TakesTheCorrectResourceForMessage()
        {
            var resourceNameToBeTaken = "DuplicatedUsername";
            var exception = new DuplicatedUsernameException(_resourceManager);

            A.CallTo(() => _resourceManager.GetString(resourceNameToBeTaken)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void InvalidUsernameException_TakesTheCorrectResourceForMessage()
        {
            var resourceNameToBeTaken = "InvalidUsername";
            var exception = new InvalidUsernameException(_resourceManager);

            A.CallTo(() => _resourceManager.GetString(resourceNameToBeTaken)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void InvalidBirthDateException_TakesTheCorrectResourceForMessage()
        {
            var resourceNameToBeTaken = "InvalidBirthDate";
            var exception = new InvalidBirthDateException(_resourceManager);

            A.CallTo(() => _resourceManager.GetString(resourceNameToBeTaken)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void InvalidBoardException_TakesTheCorrectResourceForMessage()
        {
            var resourceNameToBeTaken = "InvalidBoard";
            var exception = new InvalidBoardException(_resourceManager);

            A.CallTo(() => _resourceManager.GetString(resourceNameToBeTaken)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void InvalidBoardForCurrentUserException_TakesTheCorrectResourceForMessage()
        {
            var resourceNameToBeTaken = "InvalidBoardForCurrentUser";
            var exception = new InvalidBoardForCurrentUserException(_resourceManager);

            A.CallTo(() => _resourceManager.GetString(resourceNameToBeTaken)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void InvalidCellException_TakesTheCorrectResourceForMessage()
        {
            var resourceNameToBeTaken = "InvalidCell";
            var exception = new InvalidCellException(_resourceManager);

            A.CallTo(() => _resourceManager.GetString(resourceNameToBeTaken)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void GameNotStartedException_TakesTheCorrectResourceForMessage()
        {
            var resourceNameToBeTaken = "GameNotStarted";
            var exception = new GameNotStartedException(_resourceManager);

            A.CallTo(() => _resourceManager.GetString(resourceNameToBeTaken)).MustHaveHappenedOnceExactly();
        }
    }
}
