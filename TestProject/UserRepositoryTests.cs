using AutoFixture;
using Microsoft.EntityFrameworkCore;
using MWEntities;
using MWPersistence;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace TestProject
{
    [TestFixture]
    public class UserRepositoryTests
    {
        [Test]
        public async Task ExistsUserAsync_IfTheUserExists_ReturnsTrue()
        {
            var options = new DbContextOptionsBuilder<MWContext>()
                .UseInMemoryDatabase(databaseName: "testdatabase")
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
                .UseInMemoryDatabase(databaseName: "testdatabase")
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
                .UseInMemoryDatabase(databaseName: "testdatabase")
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
                .UseInMemoryDatabase(databaseName: "testdatabase")
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
                .UseInMemoryDatabase(databaseName: "testdatabase")
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
                .UseInMemoryDatabase(databaseName: "testdatabase")
                .Options;

            using (var context = new MWContext(options))
            {
                var repo = new UserRepository(context);
                var user = Utils.GetFixture().Create<User>();

                Assert.AreEqual(user, await repo.RegisterUserAsync(user));
            }
        }

    }
}
