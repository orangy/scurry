using System;
using Scurry.Framework;

namespace Scurry.Runtime.Tests
{
  public class AnonymousTestDescriptor : ITestDescriptor
  {
    private readonly ITestIdentity myIdentity;

    public AnonymousTestDescriptor(ITestIdentity identity)
    {
      myIdentity = identity;
    }

    public ITestDescriptor Container
    {
      get { return null; }
    }

    public ITestIdentity Identity
    {
      get { return myIdentity; }
    }
  }
}