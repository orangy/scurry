using NUnit.Framework;

namespace Scurry.Runtime.Tests.NUnit
{
  [TestFixture]
  public class OuterFixture
  {
    [Test]
    public void Test()
    {
    }
  }
 
}

namespace Scurry.Runtime.Tests.NUnit.FixtureDen
{
  [SetUpFixture]
  public class Setup
  {
    [SetUp]
    public void SetUp()
    {
    }

    [TearDown]
    public void TearDown()
    {
    }
  }

  namespace NestedSetup
  {
    [SetUpFixture]
    public class NestedSetup
    {
      [SetUp]
      public void SetUp()
      {
      }

      [TearDown]
      public void TearDown()
      {
      }
    }

    [TestFixture]
    public class NestedSetupFixture
    {
      [Test]
      public void Test()
      {
      }

    }

  }

  namespace Nested
  {
    [TestFixture]
    public class NestedFixture
    {
      [Test]
      public void Test()
      {
      }
    }
  }

  [TestFixture]
  public class SiblingFixture
  {
    [Test]
    public void Test()
    {
    }
  }

}