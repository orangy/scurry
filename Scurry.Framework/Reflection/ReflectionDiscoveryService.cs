using System;
using System.Collections.Generic;

namespace Scurry.Framework.Reflection
{
  public class ReflectionDiscoveryService : ITestDiscoveryService
  {
    private readonly Func<IEnumerable<ITestDescriptor>> myDiscover;

    public ReflectionDiscoveryService(Type type)
    {
      if (type.IsGenericTypeDefinition)
        throw new TestCompositionException("Cannot produce tests from open generic type");
      myDiscover = () => Discover(type);
    }

    private IEnumerable<ITestDescriptor> Discover(Type type)
    {
      foreach (var method in type.GetMethods())
      {
        if (method.IsPublic && !method.IsGenericMethod && method.ReturnType == typeof(void) && method.GetParameters().Length == 0)
          yield return new ReflectionTestDescriptor(method);
      }
    }

    public IEnumerable<ITestDescriptor> EnumerateTests()
    {
      return myDiscover();
    }
  }
}