using System;
using Scurry.Framework;

namespace Scurry.Runtime.Tests.Helpers
{
  public static class ConfigurationHelpers
  {
    public static AnonymousConfiguration CreateConfiguration(params ITestDescriptor[] testDescriptors)
    {
      var discoveryService = new AnonymousDiscoveryService(() => testDescriptors);
      var factoryService = new AnonymousFactoryService(Activator.CreateInstance);
      var environment = new AnonymousEnvironment(() => discoveryService, () => factoryService);
      return new AnonymousConfiguration(() => environment);
    }

    public static AnonymousConfiguration CreateConfiguration(Action action)
    {
      var testDescriptor = new AnonymousTestDescriptor(FactoryTests.TestIdentity, testFactory => new AnonymousTestInstance(action));
      return CreateConfiguration(testDescriptor);
    }
  }
}