using System;
using Scurry.Runtime.Tests.Helpers;
using Xunit;

namespace Scurry.Runtime.Tests
{
  public class ExecutionTests
  {
    [Fact]
    public void TestExecutes()
    {
      bool executed = false;
      var configuration = SessionHelpers.CreateSession(() => { executed = true; });

      Assert.False(executed);
      configuration.Execute();
      Assert.True(executed);
    }

    [Fact]
    public void TwoTestsExecutes()
    {
      int executed = 0;
      var test1 = new AnonymousTestDescriptor(FactoryTests.TestIdentity, factoryService =>
      {
        executed++;
        return null;
      });
      var test2 = new AnonymousTestDescriptor(FactoryTests.TestIdentity, factoryService =>
      {
        executed++;
        return null;
      });
      var configuration = SessionHelpers.CreateSession(test1, test2);

      Assert.Equal(0, executed);
      configuration.Execute();
      Assert.Equal(2, executed);
    }
  }
}