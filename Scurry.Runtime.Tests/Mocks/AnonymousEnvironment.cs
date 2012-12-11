using System;
using Scurry.Framework;

namespace Scurry.Runtime.Tests
{
  public class AnonymousEnvironment : ITestEnvironment
  {
    private readonly Func<ITestDiscovery> myDiscoveryFactory;
    private readonly Func<ITestFactory> myFactory;

    public AnonymousEnvironment(Func<ITestDiscovery> discovery = null, Func<ITestFactory> factory = null)
    {
      myDiscoveryFactory = discovery;
      myFactory = factory;
    }

    public ITestDiscovery CreateDiscovery()
    {
      if (myDiscoveryFactory != null)
        return myDiscoveryFactory();
      return null;
    }

    public ITestFactory CreateFactory()
    {
      if (myFactory != null)
        return myFactory();
      return null;
    }
  }
}