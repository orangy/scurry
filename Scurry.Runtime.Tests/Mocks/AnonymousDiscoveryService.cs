using System;
using System.Collections.Generic;
using Scurry.Framework;

namespace Scurry.Runtime.Tests
{
  public class AnonymousDiscoveryService : ITestDiscoveryService
  {
    private readonly Func<IEnumerable<ITestDescriptor>> myTests;

    public AnonymousDiscoveryService(Func<IEnumerable<ITestDescriptor>> tests)
    {
      myTests = tests;
    }

    public IEnumerable<ITestDescriptor> EnumerateTests()
    {
      return myTests();
    }
  }
}