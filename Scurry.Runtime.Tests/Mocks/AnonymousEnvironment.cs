using System;
using Scurry.Framework;

namespace Scurry.Runtime.Tests
{
  public class AnonymousEnvironment : ITestEnvironment
  {
    private readonly Func<ITestDiscoveryService> myDiscoveryFactory;
    private readonly Func<ITestFactoryService> myFactory;

    public AnonymousEnvironment(Func<ITestDiscoveryService> discovery = null, Func<ITestFactoryService> factory = null)
    {
      myDiscoveryFactory = discovery;
      myFactory = factory;
    }

    public ITestDiscoveryService DiscoveryService
    {
      get
      {
        if (myDiscoveryFactory != null)
          return myDiscoveryFactory();
        return null;
      }
    }

    public ITestFactoryService FactoryService
    {
      get
      {
        if (myFactory != null)
          return myFactory();
        return null;
      }
    }
  }
}