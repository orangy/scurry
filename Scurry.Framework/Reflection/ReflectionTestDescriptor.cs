using System;
using System.Reflection;

namespace Scurry.Framework.Reflection
{
  public class ReflectionTestDescriptor : ITestDescriptor, ITestIdentity
  {
    private readonly MethodInfo myMethod;

    public ReflectionTestDescriptor(MethodInfo methodInfo)
    {
      if (methodInfo == null)
        throw new ArgumentNullException("methodInfo");
      myMethod = methodInfo;
    }

    public ITestIdentity Identity
    {
      get { return this; }
    }

    public ITestInstance CreateInstance(ITestFactoryService factoryService)
    {
      if (myMethod.IsStatic)
        return new Static(myMethod);

      var type = myMethod.DeclaringType;
      var instance = factoryService.CreateInstance(type);
      return new Instance(instance, myMethod);
    }

    private class Instance : ITestInstance
    {
      private readonly object myInstance;
      private readonly MethodInfo myMethod;

      public Instance(object instance, MethodInfo method)
      {
        myInstance = instance;
        myMethod = method;
      }

      public void Run()
      {
        myMethod.Invoke(myInstance, new object[0]);
      }
    }

    private class Static : ITestInstance
    {
      private readonly MethodInfo myMethod;

      public Static(MethodInfo method)
      {
        myMethod = method;
      }

      public void Run()
      {
        myMethod.Invoke(null, new object[0]);
      }
    }
  }
}