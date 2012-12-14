using System;
using System.Collections.Generic;
using Scurry.Framework;

namespace Scurry.Runtime.Tests
{
  public class AnonymousTestDescriptor : ITestDescriptor
  {
    private readonly ITestIdentity myIdentity;
    private readonly Func<ITestContext, ITestContext> myExecute;

    public AnonymousTestDescriptor(ITestIdentity identity = null, Func<ITestContext, ITestContext> execute = null)
    {
      myIdentity = identity;
      myExecute = execute;
    }

    public ITestIdentity Identity
    {
      get { return myIdentity; }
    }

    public ITestContext Execute(ITestContext context)
    {
      return myExecute != null ? myExecute(context) : null;
    }

    public ITestDescriptor Container
    {
      get { return null; }
    }

    public IEnumerable<ITestDescriptor> Children
    {
      get { yield break; }
    }
  }
}