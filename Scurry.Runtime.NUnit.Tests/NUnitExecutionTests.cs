using System.Linq;
using NUnit.Framework;
using Scurry.Runtime.NUnit;
using Scurry.Runtime.Tests.Helpers;
using Xunit;
using Assert = Xunit.Assert;

namespace Scurry.Runtime.Tests.NUnit
{
  public class NUnitExecutionTests
  {
    private static int Tests = 0;
    private static int Setup = 0;
    private static int Constructor = 0;
    private static int Teardown = 0;
    private static int FixtureSetup = 0;
    private static int FixtureTeardown = 0;

    [Fact]
    public void ExecuteTests()
    {
      var session = SessionHelpers.CreateSession(new NUnitTestDiscoveryService(typeof (NunitExecutionTestClass)));
      Tests = 0;
      session.Execute();
      Assert.Equal(2, Tests);
    }

    [Fact]
    public void ExecuteInheritedTests()
    {
      var session = SessionHelpers.CreateSession(new NUnitTestDiscoveryService(typeof (NunitExecutionTestClass2)));
      Tests = 0;
      session.Execute();
      Assert.Equal(2, Tests);
    }

    [Fact]
    public void ExecuteSetups()
    {
      var session = SessionHelpers.CreateSession(new NUnitTestDiscoveryService(typeof (NunitExecutionTestClass)));
      Setup = 0;
      session.Execute();
      Assert.Equal(2, Setup);
    }

    [Fact]
    public void ExecuteConstructors()
    {
      var session = SessionHelpers.CreateSession(new NUnitTestDiscoveryService(typeof (NunitExecutionTestClass)));
      Constructor = 0;
      session.Execute();
      Assert.Equal(1, Constructor);
    }

    [Fact]
    public void ExecuteTeardowns()
    {
      var session = SessionHelpers.CreateSession(new NUnitTestDiscoveryService(typeof (NunitExecutionTestClass)));
      Teardown = 0;
      session.Execute();
      Assert.Equal(2, Teardown);
    }

    [Fact]
    public void ExecuteFixtureSetups()
    {
      var session = SessionHelpers.CreateSession(new NUnitTestDiscoveryService(typeof (NunitExecutionTestClass)));
      FixtureSetup = 0;
      session.Execute();
      Assert.Equal(1, FixtureSetup);
    }

    [Fact]
    public void ExecuteFixtureTeardowns()
    {
      var session = SessionHelpers.CreateSession(new NUnitTestDiscoveryService(typeof (NunitExecutionTestClass)));
      FixtureTeardown = 0;
      session.Execute();
      Assert.Equal(1, FixtureTeardown);
    }

    [Fact]
    public void ExecuteAssembly()
    {
      var session = SessionHelpers.CreateSession(new NUnitTestDiscoveryService(typeof(OuterFixture).Assembly));
      session.Execute();
    }


    private static void CountTests()
    {
      Tests++;
    }

    private static void CountSetup()
    {
      Setup++;
    }

    private static void CountConstructor()
    {
      Constructor++;
    }

    private static void CountTeardown()
    {
      Teardown++;
    }

    private static void CountFixtureTeardown()
    {
      FixtureTeardown++;
    }

    private static void CountFixtureSetup()
    {
      FixtureSetup++;
    }

    [TestFixture]
    private class NunitExecutionTestClass
    {
      public NunitExecutionTestClass()
      {
        CountConstructor();
      }

      [SetUp]
      public void SetupMethod()
      {
        CountSetup();
      }

      [TestFixtureSetUp]
      public void FixtureSetupMethod()
      {
        CountFixtureSetup();
      }

      [TearDown]
      public void TeardownMethod()
      {
        CountTeardown();
      }

      [TestFixtureTearDown]
      public void FixtureTeardownMethod()
      {
        CountFixtureTeardown();
      }

      [Test]
      public void PublicTestFunction()
      {
        CountTests();
      }

      [Test]
      public void AnotherPublicTestFunction()
      {
        CountTests();
      }

      [Test]
      private void PrivateTestFunction()
      {
        CountTests();
      }
    }

    private class NunitExecutionTestClass2 : NunitExecutionTestClass
    {
       
    }
  }
}