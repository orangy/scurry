using System;
using Scurry.Framework.Reflection;
using Scurry.Runtime.Tests.Helpers;
using Xunit;

namespace Scurry.Runtime.Tests
{
  public class ReflectionExecutionTests
  {
    [Fact]
    public void ExecuteInstanceTest()
    {
      var testDescriptor = new ReflectionTestDescriptor(typeof(ReflectionExecutionTestClass).GetMethod("InstanceTestFunction"));
      var configuration = ConfigurationHelpers.CreateConfiguration(testDescriptor);
      Assert.Equal(0, ReflectionExecutionTestClass.InstanceCount);
      new TestSession(configuration).Execute();
      Assert.Equal(1, ReflectionExecutionTestClass.InstanceCount);
    }

    [Fact]
    public void ExecuteStaticTest()
    {
      var testDescriptor = new ReflectionTestDescriptor(typeof(ReflectionExecutionTestClass).GetMethod("StaticTestFunction"));
      var configuration = ConfigurationHelpers.CreateConfiguration(testDescriptor);
      Assert.Equal(0, ReflectionExecutionTestClass.StaticCount);
      new TestSession(configuration).Execute();
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
}