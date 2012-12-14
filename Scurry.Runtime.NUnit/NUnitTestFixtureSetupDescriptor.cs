using System;
using System.Collections.Generic;
using System.Reflection;
using Scurry.Framework;

namespace Scurry.Runtime.NUnit
{
  public class NUnitTestFixtureSetupDescriptor : ITestDescriptor, ITestIdentity
  {
    private readonly Type myType;
    private readonly List<ITestDescriptor> myChildren = new List<ITestDescriptor>();
    private readonly Dictionary<SpecialMethodKind, MethodInfo> mySpecialMethods = new Dictionary<SpecialMethodKind, MethodInfo>();

    public NUnitTestFixtureSetupDescriptor(Type type)
    {
      myType = type;
    }

    public ITestIdentity Identity
    {
      get { return this; }
    }

    public ITestContext Execute(ITestContext context)
    {
      return new Context(context, myType, mySpecialMethods);
    }

    public ITestDescriptor Container
    {
      get { return null; }
    }

    public IEnumerable<ITestDescriptor> Children
    {
      get { return myChildren; }
    }

    public void AddChild(ITestDescriptor descriptor)
    {
      myChildren.Add(descriptor);
    }

    private class Context : ITestContext, IDisposable
    {
      private readonly ITestContext myParentContext;
      private readonly Dictionary<SpecialMethodKind, MethodInfo> mySpecialMethods;
      private readonly object myValue;

      public Context(ITestContext parentContext, Type type, Dictionary<SpecialMethodKind, MethodInfo> specialMethods)
      {
        myParentContext = parentContext;
        mySpecialMethods = specialMethods;
        myValue = CreateInstance(type);
        MethodInfo methodInfo;
        if (mySpecialMethods.TryGetValue(SpecialMethodKind.Setup, out methodInfo))
          methodInfo.Invoke(myValue, new object[0]);
      }

      public object Value
      {
        get { return myValue; }
      }

      public object CreateInstance(Type type)
      {
        return myParentContext.CreateInstance(type);
      }

      public void Dispose()
      {
        MethodInfo methodInfo;
        if (mySpecialMethods.TryGetValue(SpecialMethodKind.Teardown, out methodInfo))
          methodInfo.Invoke(myValue, new object[0]);
      }
    }

    public void AddSpecialMethod(SpecialMethodKind setup, MethodInfo method)
    {
      mySpecialMethods[setup] = method;
    }
  }
}