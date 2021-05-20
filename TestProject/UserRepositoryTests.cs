using AutoFixture;
using Microsoft.EntityFrameworkCore;
using MWEntities;
using MWPersistence;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace TestProject
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private Fixture _fixture;

        [SetUp]
        public void setup() {
            _fixture = Utils.GetFixture();
        }

        [Test]
        public async Task ExistsUserAsync_IfTheUserExists_ReturnsTrue()
        {
            var options = new DbContextOptionsBuilder<MWContext>()
                .UseInMemoryDatabase(databaseName: _fixture.Create<string>())
                .Options;

            using (var context = new MWContext(options))
            {
                var repo = new UserRepository(context);
                var user = Utils.GetFixture().Create<User>();

                context.Users.Add(user);
                context.SaveChanges();

                Assert.IsTrue(await repo.ExistsUserAsync(user.Username));
            }

        }

        [Test]
        public async Task ExistsUserAsync_IfTheUserDoesNotExists_ReturnsFalse()
        {
            var options = new DbContextOptionsBuilder<MWContext>()
                .UseInMemoryDatabase(databaseName: _fixture.Create<string>())
                .Options;


            using (var context = new MWContext(options))
            {
                var repo = new UserRepository(context);
                var user = Utils.GetFixture().Create<User>();

                context.Users.Add(user);
                context.SaveChanges();

                Assert.IsFalse(await repo.ExistsUserAsync(DateTime.Now.ToString()));
            }
        }


        [Test]
        public async Task GetUserAsync_IfTheUserExists_ReturnsTheUser()
        {
            var options = new DbContextOptionsBuilder<MWContext>()
                .UseInMemoryDatabase(databaseName: _fixture.Create<string>())
                .Options;

            using (var context = new MWContext(options))
            {
                var repo = new UserRepository(context);
                var user = Utils.GetFixture().Create<User>();

                context.Users.Add(user);
                context.SaveChanges();

                Assert.AreEqual(user, await repo.GetUserAsync(user.Username));
            }

        }

        [Test]
        public async Task GetUserAsync_IfTheUserDoesExists_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<MWContext>()
                .UseInMemoryDatabase(databaseName: _fixture.Create<string>())
                .Options;

            using (var context = new MWContext(options))
            {
                var repo = new UserRepository(context);
                var user = Utils.GetFixture().Create<User>();

                context.Users.Add(user);
                context.SaveChanges();

                Assert.IsNull(await repo.GetUserAsync(DateTime.Now.ToString()));
            }
        }

        [Test]
        public async Task RegisterUserAsync_MustCreateTheUserInTheContext()
        {
            var options = new DbContextOptionsBuilder<MWContext>()
                .UseInMemoryDatabase(databaseName: _fixture.Create<string>())
                .Options;

            using (var context = new MWContext(options))
            {
                var repo = new UserRepository(context);
                var user = Utils.GetFixture().Create<User>();

                await repo.RegisterUserAsync(user);

                context.Users.Find(user.Username);

            }
        }


        [Test]
        public async Task RegisterUserAsync_MustReturnTheUser()
        {
            var options = new DbContextOptionsBuilder<MWContext>()
                .UseInMemoryDatabase(databaseName: _fixture.Create<string>())
                .Options;

            using (var context = new MWContext(options))
            {
                var repo = new UserRepository(context);
                var user = Utils.GetFixture().Create<User>();

                Assert.AreEqual(user, await repo.RegisterUserAsync(user));
            }
        }


        [Test]
        public async Task GetUserBoardsAsync_MustReturnTheUserBoards()
        {
            var options = new DbContextOptionsBuilder<MWContext>()
                .UseInMemoryDatabase(databaseName: _fixture.Create<string>())
                .Options;

            using (var context = new MWContext(options))
            {
                var repo = new UserRepository(context);
                
                const string user1 = "user1";
                var b1 = _fixture.Create<Board>();
                var b2 = _fixture.Create<Board>();

                var pb1 = _fixture.Build<PersistibleBoard>()
                                .With(b => b.BoardDefinition, JsonConvert.SerializeObject(b1).ToString())
                                .With(b => b.Username, user1).Create();
                var pb2 = _fixture.Build<PersistibleBoard>()
                                .With(b => b.BoardDefinition, JsonConvert.SerializeObject(b2).ToString())
                                .With(b => b.Username, user1).Create();
                var pb3 = _fixture.Create<PersistibleBoard>();
                var pb4 = _fixture.Create<PersistibleBoard>();

                context.PersistibleBoards.Add(pb1);
                context.PersistibleBoards.Add(pb2);
                context.PersistibleBoards.Add(pb3);
                context.PersistibleBoards.Add(pb4);
                context.SaveChanges();

                var result = await repo.GetUserBoardsAsync(user1);
                Assert.AreEqual(2, result.Count);
            }
        }


        [Test]
        public async Task GetUserBoardsAsync_WhenTheUserDoesNotHaveBoards_MustReturnEmptyList()
        {
            var options = new DbContextOptionsBuilder<MWContext>()
                .UseInMemoryDatabase(databaseName: _fixture.Create<string>())
                .Options;

            using (var context = new MWContext(options))
            {
                var repo = new UserRepository(context);

                const string user1 = "user1";
                var pb1 = _fixture.Create<PersistibleBoard>();
                var pb2 = _fixture.Create<PersistibleBoard>();
                var pb3 = _fixture.Create<PersistibleBoard>();
                
                context.PersistibleBoards.Add(pb1);
                context.PersistibleBoards.Add(pb2);
                context.PersistibleBoards.Add(pb3);
                context.SaveChanges();

                var result = await repo.GetUserBoardsAsync(user1);
                Assert.IsNotNull(result);
                Assert.AreEqual(0, result.Count);
            }
        }


        [Test]
        public async Task GetUserAsync_MustReturnTheUser()
        {
            var options = new DbContextOptionsBuilder<MWContext>()
                .UseInMemoryDatabase(databaseName: _fixture.Create<string>())
                .Options;

            using (var context = new MWContext(options))
            {
                var repo = new UserRepository(context);

                const string user1 = "user1";
                var u1 = _fixture.Build<User>()
                                .With(b => b.Username, user1).Create();

                context.Users.Add(u1);
                context.SaveChanges();

                var result = await repo.GetUserAsync(user1);
                Assert.AreEqual(u1, result);
            }
        }


        [Test]
        public async Task GetUserAsync_WhenTheUserDoesNotExist_MustReturnNull()
        {
            var options = new DbContextOptionsBuilder<MWContext>()
                .UseInMemoryDatabase(databaseName: _fixture.Create<string>())
                .Options;

            using (var context = new MWContext(options))
            {
                var repo = new UserRepository(context);

                const string user1 = "existingUser";
                const string user2 = "nonExistingUser";
                var u1 = _fixture.Build<User>()
                                .With(b => b.Username, user1).Create();

                context.Users.Add(u1);
                context.SaveChanges();

                var result = await repo.GetUserAsync(user2);

                Assert.IsNull(result);
            }
        }
    }
}
