using System;
using System.Reflection;
using Scurry.Framework;

namespace Scurry.Runtime.NUnit
{
  public class NUnitTestEnvironment : ITestEnvironment
  {
    private readonly Assembly myAssembly;
    private readonly Type myType;

    public NUnitTestEnvironment(Assembly assembly)
    {
      myAssembly = assembly;
    }
    
    public NUnitTestEnvironment(Type type)
    {
      myType = type;
    }

    public ITestDiscoveryService DiscoveryService
    {
      get
      {
        if (myAssembly != null)
          return new NUnitTestDiscoveryService(myAssembly);
        if (myType != null)
          return new NUnitTestDiscoveryService(myType);
        return null;
      }
    }

    public ITestFactoryService FactoryService
    {
      get { return new NUnitTestFactoryService(); }
    }
  }
}