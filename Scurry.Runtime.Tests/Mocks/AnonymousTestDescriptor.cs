using System;
using Scurry.Framework;

namespace Scurry.Runtime.Tests
{
  public class AnonymousTestDescriptor : ITestDescriptor
  {
    private readonly ITestIdentity myIdentity;
    private readonly Func<ITestFactoryService, ITestInstance> myFactory;

    public AnonymousTestDescriptor(ITestIdentity identity = null, Func<ITestFactoryService, ITestInstance> factory = null)
    {
      myIdentity = identity;
      myFactory = factory;
    }

    public ITestIdentity Identity
    {
      get { return myIdentity; }
    }

    public ITestInstance CreateInstance(ITestFactoryService factoryService)
    {
      if (myFactory != null)
        return myFactory(factoryService);
      return null;
    }
  }
}