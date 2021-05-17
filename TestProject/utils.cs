using AutoFixture;
using System.Linq;

namespace TestProject
{
    internal class Utils
    {
        public static Fixture GetFixture()
        {
            var fixture = new Fixture();

            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            return fixture;
        }
    }
}