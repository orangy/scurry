using System;
using System.Linq;
using Scurry.Framework;
using Xunit;

namespace Scurry.Runtime.Tests
{
  public class TestDiscovery
  {
    [Fact]
    public void FailOnNullDiscovery()
    {
      var environment = new AnonymousEnvironment();
      var configuration = new AnonymousConfiguration(() => environment);
      Assert.Throws<TestConfigurationException>(() => new TestSession(configuration).Execute());
    }

    [Fact]
    public void FailOnNullTestsEnumerable()
    {
      var discovery = new AnonymousDiscovery(() => null);
      var environment = new AnonymousEnvironment(() => discovery);
      var configuration = new AnonymousConfiguration(() => environment);
      Assert.Throws<TestCompositionException>(() => new TestSession(configuration).Execute());
    }

    [Fact]
    public void FailOnNullIdentity()
    {
      var discovery = new AnonymousDiscovery(() => new[] {new AnonymousTestDescriptor(null)});
      var environment = new AnonymousEnvironment(() => discovery);
      var configuration = new AnonymousConfiguration(() => environment);
      Assert.Throws<TestCompositionException>(() => new TestSession(configuration).Execute());
    }

    [Fact]
    public void DiscoveryCreated()
    {
      var discovery = new AnonymousDiscovery(Enumerable.Empty<ITestDescriptor>);
      var environment = new AnonymousEnvironment(() => discovery);
      new TestSession(new AnonymousConfiguration(() => environment)).Execute();
    }

  }
}