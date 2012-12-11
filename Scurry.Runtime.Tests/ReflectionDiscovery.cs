using System;
using System.Collections.Generic;
using System.Reflection;
using Scurry.Framework;
using Scurry.Framework.Reflection;
using Scurry.Runtime.Tests.Helpers;
using Xunit;

namespace Scurry.Runtime.Tests
{
  public class ReflectionDiscovery
  {
    [Fact]
    public void DiscoverTestsFromMethods()
    {
      var configuration = ConfigurationHelpers.CreateConfiguration(new ReflectionDiscoveryService(typeof(ReflectionExecutionTestClass)));
      Assert.Equal(0, ReflectionExecutionTestClass.InstanceCount);
      Assert.Equal(0, ReflectionExecutionTestClass.StaticCount);
      new TestSession(configuration).Execute();
      Assert.Equal(1, ReflectionExecutionTestClass.InstanceCount);
      Assert.Equal(1, ReflectionExecutionTestClass.StaticCount);
    }

    private class ReflectionExecutionTestClass
    {
      public static int InstanceCount;
      public static int StaticCount;

      public void InstanceTestFunction()
      {
        InstanceCount++;
      }

      public static void StaticTestFunction()
      {
        StaticCount++;
      }
    }

  }

  public class ReflectionDiscoveryService : ITestDiscoveryService
  {
    private readonly Func<IEnumerable<ITestDescriptor>> myDiscover;

    public ReflectionDiscoveryService(Type type)
    {
      myDiscover = () => Discover(type);
    }

    private IEnumerable<ITestDescriptor> Discover(Type type)
    {
      foreach (var method in type.GetMethods())
      {
        if (method.IsPublic && !method.IsGenericMethod && method.ReturnType == typeof(void) && method.GetParameters().Length == 0)
          yield return new ReflectionTestDescriptor(method);
      }
    }

    public IEnumerable<ITestDescriptor> EnumerateTests()
    {
      return myDiscover();
    }
  }
}