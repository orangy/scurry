using System;
using Scurry.Framework;

namespace Scurry.Runtime.Tests
{
  public class AnonymousFactory : ITestFactory
  {
    public ITestInstance CreateInstance(ITestDescriptor testDescriptor)
    {
      return null;
    }
  }
}