using System;
using System.Linq;
using Scurry.Framework;
using Scurry.Framework.Reflection;
using Scurry.Runtime.Tests.Helpers;
using Xunit;
using Xunit.Sdk;

namespace Scurry.Runtime.Tests
{
  public class ReflectionDiscoveryTests
  {
    [Fact]
    public void DiscoverTestsFromMethods()
    {
      var session = SessionHelpers.CreateSession(new ReflectionDiscoveryService(typeof (ReflectionDiscoveryTestClass)));
      Assert.Equal(2, session.EnumerateTests().Count());
    }

    [Fact]
    public void FailToDiscoverTestsFromOpenGenericClass()
    {
      var session = SessionHelpers.CreateSession(() => new ReflectionDiscoveryService(typeof (ReflectionDiscoveryTestClass<>)));
      Assert.Throws<TestCompositionException>(() => Assert.Equal(2, session.EnumerateTests().Count()));
    }

    [Fact]
    public void DiscoverTestsFromGenericClass()
    {
      var session = SessionHelpers.CreateSession(() => new ReflectionDiscoveryService(typeof (ReflectionDiscoveryTestClass<string>)));
      Assert.Equal(1, session.EnumerateTests().Count());
    }

    private static void ShouldNotBeCalled()
    {
      throw new AssertException("Shouldn't call this method in discovery tests");
    }

    private class ReflectionDiscoveryTestClass<T>
    {
      public void NonGenericTest()
      {
        ShouldNotBeCalled();
      }

      public void GenericTest(T value)
      {
        ShouldNotBeCalled();
      }
    }

    private class ReflectionDiscoveryTestClass
    {
      public void InstanceTestFunction()
      {
        ShouldNotBeCalled();
      }

      public void GenericTestFunction<T>()
      {
        ShouldNotBeCalled();
      }

      public static void StaticTestFunction()
      {
        ShouldNotBeCalled();
      }
    }
  }
}