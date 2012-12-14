using System;
using System.Linq;
using NUnit.Framework;
using Scurry.Framework.NUnit;
using Scurry.Runtime.Tests.Helpers;
using Xunit;
using Xunit.Sdk;
using Assert = Xunit.Assert;

namespace Scurry.Runtime.Tests.NUnit
{
  public class NUnitDiscoveryTests
  {
    [Fact]
    public void DiscoverTestsFromMethods()
    {
      var session = SessionHelpers.CreateSession(new NUnitTestDiscoveryService(typeof (NunitDiscoveryTestClass)));
      Assert.Equal(1, session.CreateTestGraph().Count());
    }

    private static void ShouldNotBeCalled()
    {
      throw new AssertException("Shouldn't call this method in discovery tests");
    }

    [TestFixture]
    private class NunitDiscoveryTestClass
    {
      [Test]
      public void InstanceTestFunction()
      {
        ShouldNotBeCalled();
      }
    }
  }
}