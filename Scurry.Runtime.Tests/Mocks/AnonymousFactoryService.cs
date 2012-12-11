using System;
using Scurry.Framework;

namespace Scurry.Runtime.Tests
{
  public class AnonymousFactoryService : ITestFactoryService
  {
    private readonly Func<Type, object> myFactory;

    public AnonymousFactoryService(Func<Type, object> factory = null)
    {
      myFactory = factory;
    }

    public object CreateInstance(Type type)
    {
      if (myFactory != null)
        return myFactory(type);
      return null;
    }
  }
}