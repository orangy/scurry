using System;
using System.Linq;
using NUnit.Framework;
using Scurry.Runtime.NUnit;
using Scurry.Runtime.Tests.Helpers;
using Xunit;
using Xunit.Sdk;
using Assert = Xunit.Assert;

namespace Scurry.Runtime.Tests.NUnit
{
  public class NUnitDiscoveryTests
  {
    [Fact]
    public void DiscoverTestsFromType()
    {
      var session = SessionHelpers.CreateSession(new NUnitTestEnvironment(typeof (NUnitDiscoveryTestClass)));
      var testGraph = session.CreateTestGraph();
      Assert.Equal(1, testGraph.Count());
      Assert.Equal(1, testGraph.Single().Children.Count());
    }

    [Fact]
    public void DiscoverTestsFromAssembly()
    {
      var session = SessionHelpers.CreateSession(new NUnitTestEnvironment(typeof(OuterFixture).Assembly));
      var testGraph = session.CreateTestGraph();
      Assert.Equal(2, testGraph.Count());
    }

    private static void ShouldNotBeCalled()
    {
      throw new AssertException("Shouldn't call this method in discovery tests");
    }

    [TestFixture]
    private class NUnitDiscoveryTestClass
    {
      [Test]
      public void InstanceTestFunction()
      {
        ShouldNotBeCalled();
      }
    }
  }
}