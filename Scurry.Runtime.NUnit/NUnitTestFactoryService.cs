using System;
using Scurry.Framework;

namespace Scurry.Runtime.NUnit
{
  public class NUnitTestFactoryService : ITestFactoryService
  {
    public object CreateInstance(Type type)
    {
      return Activator.CreateInstance(type);
    }
  }
}