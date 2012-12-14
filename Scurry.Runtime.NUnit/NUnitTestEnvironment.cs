using System;
using System.Reflection;
using Scurry.Framework;

namespace Scurry.Runtime.NUnit
{
  public class NUnitTestEnvironment : ITestEnvironment
  {
    private readonly Assembly myAssembly;

    public NUnitTestEnvironment(Assembly assembly)
    {
      myAssembly = assembly;
    }

    public ITestDiscoveryService DiscoveryService
    {
      get { return new NUnitTestDiscoveryService(myAssembly); }
    }

    public ITestFactoryService FactoryService
    {
      get { return new NUnitTestFactoryService(); }
    }
  }
}