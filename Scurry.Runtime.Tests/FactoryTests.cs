using Scurry.Framework;
using Scurry.Runtime.Tests.Helpers;
using Xunit;

namespace Scurry.Runtime.Tests
{
  public class FactoryTests
  {
    public static readonly StringTestIdentity TestIdentity = new StringTestIdentity("Test");

    [Fact]
    public void FailOnNullFactory()
    {
      var discovery = new AnonymousDiscoveryService(() => new[]
      {
        new AnonymousTestDescriptor(TestIdentity, context =>
        {
          context.CreateInstance(typeof (object));
          return null;
        })
      });
      var environment = new AnonymousEnvironment(() => discovery, null);
      var configuration = new AnonymousConfiguration(() => environment);
      Assert.Throws<TestConfigurationException>(() => new TestSession(configuration).Execute());
    }

    [Fact]
    public void Discover()
    {
      var testDescriptor = new AnonymousTestDescriptor(TestIdentity);
      var configuration = SessionHelpers.CreateSession(testDescriptor);
      configuration.Execute();
    }
  }
}