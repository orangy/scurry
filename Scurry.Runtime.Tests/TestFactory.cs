using Scurry.Framework;
using Xunit;

namespace Scurry.Runtime.Tests
{
  public class TestFactory
  {
    private readonly StringTestIdentity myTestIdentity = new StringTestIdentity("Test");

    [Fact]
    public void FailOnNullFactory()
    {
      var discovery = new AnonymousDiscovery(() => new[] {new AnonymousTestDescriptor(myTestIdentity)});
      var environment = new AnonymousEnvironment(() => discovery);
      var configuration = new AnonymousConfiguration(() => environment);
      Assert.Throws<TestCompositionException>(() => new TestSession(configuration).Execute());
    }

    [Fact]
    public void Discover()
    {
      var discovery = new AnonymousDiscovery(() => new[] {new AnonymousTestDescriptor(myTestIdentity)});
      var factory = new AnonymousFactory();
      var environment = new AnonymousEnvironment(() => discovery, () => factory);
      var configuration = new AnonymousConfiguration(() => environment);
      new TestSession(configuration).Execute();
    }
  }
}