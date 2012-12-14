using System;
using System.Collections.Generic;
using System.Reflection;

namespace Scurry.Framework.NUnit
{
  public class NUnitTestDescriptor : ITestDescriptor, ITestIdentity
  {
    private readonly NUnitTestFixtureDescriptor myFixtureDescriptor;
    private readonly MethodInfo myMethod;

    public NUnitTestDescriptor(NUnitTestFixtureDescriptor fixtureDescriptor, MethodInfo method)
    {
      myFixtureDescriptor = fixtureDescriptor;
      myMethod = method;
    }

    public ITestIdentity Identity
    {
      get { return this; }
    }

    public ITestContext Execute(ITestContext context)
    {
      var nunitContext = (NUnitTestFixtureDescriptor.Context)context;
      nunitContext.TestSetup();
      myMethod.Invoke(nunitContext.Value, new object[0]);
      nunitContext.TestTeardown();
      return null;
    }

    public ITestDescriptor Container
    {
      get { return myFixtureDescriptor; }
    }

    public IEnumerable<ITestDescriptor> Children
    {
      get { yield break; }
    }
  }
}