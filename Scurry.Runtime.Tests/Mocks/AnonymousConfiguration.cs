using System;
using Scurry.Framework;

namespace Scurry.Runtime.Tests
{
  public class AnonymousConfiguration : ITestSessionConfiguration
  {
    private readonly Func<ITestEnvironment> myCreateEnvironment;

    public AnonymousConfiguration(Func<ITestEnvironment> createEnvironment)
    {
      myCreateEnvironment = createEnvironment;
    }

    public ITestEnvironment CreateEnvironment()
    {
      return myCreateEnvironment();
    }
  }
}