using System;
using System.Collections.Generic;
using System.Linq;

namespace Scurry.Framework.NUnit
{
  public class NUnitTestDiscoveryService : ITestDiscoveryService
  {
    private static readonly Dictionary<SpecialMethodKind, string> SpecialMethods = new Dictionary<SpecialMethodKind, string>
    {
      {SpecialMethodKind.Setup, "NUnit.Framework.SetUpAttribute"},
      {SpecialMethodKind.Teardown, "NUnit.Framework.TearDownAttribute"},
      {SpecialMethodKind.FixtureSetup, "NUnit.Framework.TestFixtureSetUpAttribute"},
      {SpecialMethodKind.FixtureTeardown, "NUnit.Framework.TestFixtureTearDownAttribute"},
    };

    private readonly Type myType;

    public NUnitTestDiscoveryService(Type type)
    {
      myType = type;
      if (myType.IsGenericTypeDefinition)
        throw new TestCompositionException("Cannot produce tests from open generic type");
      if (myType.IsAbstract)
        throw new TestCompositionException("Cannot produce tests from abstract type");
    }

    public IEnumerable<ITestDescriptor> CreateTestGraph()
    {
      var fixtureDescriptor = new NUnitTestFixtureDescriptor(myType);
      foreach (var method in myType.GetMethods())
      {
        var attributes = method.GetCustomAttributes(false);

        foreach (var specialMethod in SpecialMethods)
        {
          if (attributes.Any(o => String.Equals(o.GetType().FullName, specialMethod.Value, StringComparison.Ordinal)))
            fixtureDescriptor.AddSpecialMethod(specialMethod.Key, method);
        }

        if (attributes.Any(o => String.Equals(o.GetType().FullName, "NUnit.Framework.TestAttribute", StringComparison.Ordinal)))
        {
          if (!method.IsPublic || method.IsGenericMethod || method.ReturnType != typeof (void) || method.GetParameters().Length != 0)
            continue;

          fixtureDescriptor.AddTest(new NUnitTestDescriptor(fixtureDescriptor, method));
        }
      }
      yield return fixtureDescriptor;
    }
  }
}