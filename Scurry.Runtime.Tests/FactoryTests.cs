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
      var discovery = new AnonymousDiscoveryService(() => new[] {new AnonymousTestDescriptor(TestIdentity)});
      var environment = new AnonymousEnvironment(() => discovery, null);
      var configuration = new AnonymousConfiguration(() => environment);
      Assert.Throws<TestCompositionException>(() => new TestSession(configuration).Execute());
    }

    [Fact]
    public void Discover()
    {
      var testDescriptor = new AnonymousTestDescriptor(TestIdentity);
      var configuration = ConfigurationHelpers.CreateConfiguration(testDescriptor);
      new TestSession(configuration).Execute();
    }
  }
}