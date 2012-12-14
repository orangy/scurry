using System;
using Scurry.Framework;

namespace Scurry.Runtime.Tests.Helpers
{
  public static class SessionHelpers
  {
    public static ITestSession CreateSession(params ITestDescriptor[] testDescriptors)
    {
      return CreateSession(new AnonymousDiscoveryService(() => testDescriptors));
    }

    public static ITestSession CreateSession(ITestDiscoveryService discoveryService)
    {
      return CreateSession(() => discoveryService);
    }

    public static ITestSession CreateSession(Action action)
    {
      return CreateSession(new AnonymousTestDescriptor(FactoryTests.TestIdentity, testFactory =>
      {
        action();
        return null;
      }));
    }

    public static ITestSession CreateSession(Func<ITestDiscoveryService> discoveryService)
    {
      var factoryService = new AnonymousFactoryService(Activator.CreateInstance);
      var environment = new AnonymousEnvironment(discoveryService, () => factoryService);
      var configuration = new AnonymousConfiguration(() => environment);
      return new TestSession(configuration);
    }
  }
}