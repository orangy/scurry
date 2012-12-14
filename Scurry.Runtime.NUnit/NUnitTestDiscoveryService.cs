using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Scurry.Framework;

namespace Scurry.Runtime.NUnit
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

    class IntrospectionNode
    {
      private readonly Type mySetupFixture;
      private readonly List<Type> myFixtures = new List<Type>();
      private readonly List<IntrospectionNode> myChildren = new List<IntrospectionNode>();

      public IntrospectionNode()
      {
      }

      public IntrospectionNode(Type setup)
      {
        if (setup == null)
          throw new ArgumentNullException("setup");
        mySetupFixture = setup;
      }

      public Type SetupFixture
      {
        get { return mySetupFixture; }
      }

      public IList<Type> Fixtures
      {
        get { return myFixtures; }
      }

      public IList<IntrospectionNode> Children
      {
        get { return myChildren; }
      }
    }

    private readonly List<IntrospectionNode> myNodes = new List<IntrospectionNode>();

    public NUnitTestDiscoveryService(Type fixtureType)
    {
      if (fixtureType == null)
        throw new ArgumentNullException("fixtureType");
      if (fixtureType.IsGenericTypeDefinition)
        throw new TestCompositionException("Cannot produce tests from open generic type");
      if (fixtureType.IsAbstract)
        throw new TestCompositionException("Cannot produce tests from abstract type");

      var node = new IntrospectionNode();
      node.Fixtures.Add(fixtureType);
      myNodes.Add(node);
    }

    public NUnitTestDiscoveryService(Assembly assembly)
    {
      if (assembly == null)
        throw new ArgumentNullException("assembly");
      var fixtureTypes = new List<Type>();
      var setupTypes = new List<Type>();
      
      // ExportedTypes include all public types, including nested
      foreach (var type in assembly.ExportedTypes)
        IntrospectType(type, fixtureTypes, setupTypes);

      var setupNodes = IntrospectSetupNodes(setupTypes);

      IntrospectionNode globalNode = null;
      foreach (var fixtureType in fixtureTypes)
      {
        var node = FindNode(setupNodes, fixtureType);
        if (node == null)
        {
          if (globalNode == null)
            globalNode = new IntrospectionNode();
          globalNode.Fixtures.Add(fixtureType);
        } else
          node.Fixtures.Add(fixtureType);
      }

      if (globalNode != null)
        myNodes.Add(globalNode);
      foreach (var setupNode in setupNodes)
        myNodes.Add(setupNode);
    }

    private IList<IntrospectionNode> IntrospectSetupNodes(IEnumerable<Type> setupTypes)
    {
      var setupNodes = new List<IntrospectionNode>();
      foreach (var setupType in setupTypes)
      {
        var node = FindNode(setupNodes, setupType);
        if (node == null)
          setupNodes.Add(new IntrospectionNode(setupType));
        else
          node.Children.Add(new IntrospectionNode(setupType));
      }
      return setupNodes;
    }

    private static void IntrospectType(Type type, List<Type> fixtureTypes, List<Type> setupTypes)
    {
      if (type.IsGenericTypeDefinition)
        return;
      if (type.IsAbstract)
        return;
      var attributes = type.GetCustomAttributes(true);
      if (attributes.Any(o => String.Equals(o.GetType().FullName, "NUnit.Framework.TestFixtureAttribute", StringComparison.Ordinal)))
        fixtureTypes.Add(type);
      else if (attributes.Any(o => String.Equals(o.GetType().FullName, "NUnit.Framework.SetUpFixtureAttribute", StringComparison.Ordinal)))
        setupTypes.Add(type);
    }

    private IntrospectionNode FindNode(IEnumerable<IntrospectionNode> nodes, Type type)
    {
      foreach (var node in nodes)
      {
        var setupFixture = node.SetupFixture;
        if (setupFixture != null)
        {
          var ns = setupFixture.Namespace;
          if (ns == null || type.Namespace.StartsWith(ns, StringComparison.Ordinal))
          {
            var childNode = FindNode(node.Children, type);
            return childNode ?? node;
          }
        }
      }
      return null;
    }

    public IEnumerable<ITestDescriptor> CreateTestGraph()
    {
      return myNodes.SelectMany(CreateDescriptor);
    }

    private static IEnumerable<ITestDescriptor> CreateDescriptor(IntrospectionNode node)
    {
      if (node.SetupFixture != null)
      {
        var setupDescriptor = CreateFixtureSetup(node.SetupFixture);
        foreach (var fixtureType in node.Fixtures)
          setupDescriptor.AddChild(CreateFixture(fixtureType));
        
        foreach (var childNode in node.Children)
          foreach (var childDescriptor in CreateDescriptor(childNode))
            setupDescriptor.AddChild(childDescriptor);

        yield return setupDescriptor;
      } else
      {
        foreach (var fixtureType in node.Fixtures)
          yield return CreateFixture(fixtureType);
      }
    }

    private static NUnitTestFixtureSetupDescriptor CreateFixtureSetup(Type type)
    {
      var fixtureSetup = new NUnitTestFixtureSetupDescriptor(type);
      foreach (var method in type.GetMethods())
      {
        var attributes = method.GetCustomAttributes(false);
        foreach (var specialMethod in SpecialMethods)
        {
          if (attributes.Any(o => String.Equals(o.GetType().FullName, specialMethod.Value, StringComparison.Ordinal)))
            fixtureSetup.AddSpecialMethod(specialMethod.Key, method);
        }
      }
      return fixtureSetup;
    }

    private static NUnitTestFixtureDescriptor CreateFixture(Type type)
    {
      var fixtureDescriptor = new NUnitTestFixtureDescriptor(type);
      foreach (var method in type.GetMethods())
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
      return fixtureDescriptor;
    }
  }
}