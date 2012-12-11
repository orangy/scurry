using System;
using System.Linq;
using Scurry.Framework;
using Xunit;

namespace Scurry.Runtime.Tests
{
  public class EnvironmentTests
  {
    [Fact]
    public void FailOnNullConfiguration()
    {
      Assert.Throws<ArgumentNullException>(() => { new TestSession(null); });
    }

    [Fact]
    public void FailOnProvidedNullEnvironment()
    {
      var configuration = new AnonymousConfiguration(() => null);
      Assert.Throws<TestConfigurationException>(() => new TestSession(configuration).Execute());
    }

    [Fact]
    public void EnvironmentCreatedOnExecute()
    {
      var created = false;
      var configuration = new AnonymousConfiguration(() =>
      {
        created = true;
        return new AnonymousEnvironment(() => new AnonymousDiscoveryService(Enumerable.Empty<ITestDescriptor>));
      });

      var session = new TestSession(configuration);
      Assert.False(created);
      session.Execute();
      Assert.True(created);
    }
  }
}