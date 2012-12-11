using System;
using System.Collections.Generic;

namespace Scurry.Framework
{
  public interface ITestDiscovery
  {
    IEnumerable<ITestDescriptor> EnumerateTests();
  }

  public class AnonymousDiscovery : ITestDiscovery
  {
    private readonly Func<IEnumerable<ITestDescriptor>> myTests;

    public AnonymousDiscovery(Func<IEnumerable<ITestDescriptor>> tests)
    {
      myTests = tests;
    }

    public IEnumerable<ITestDescriptor> EnumerateTests()
    {
      return myTests();
    }
  }
}