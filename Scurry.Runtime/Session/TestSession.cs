using System;
using Scurry.Framework;

namespace Scurry.Runtime
{
  public class TestSession : ITestSession
  {
    private readonly ITestSessionConfiguration myConfiguration;

    public TestSession(ITestSessionConfiguration configuration)
    {
      if (configuration == null)
        throw new ArgumentNullException("configuration");
      myConfiguration = configuration;
    }

    public void Execute()
    {
      var environment = myConfiguration.CreateEnvironment();
      if (environment == null)
        throw new TestConfigurationException(string.Format("Test session configuration {0} should provide environment", myConfiguration.GetType()));
      Execute(environment);
    }

    private void Execute(ITestEnvironment environment)
    {
      var discovery = environment.DiscoveryService;
      if (discovery == null)
        throw new TestConfigurationException(string.Format("Test session environment {0} should provide discovery service", environment.GetType()));

      var testDescriptors = discovery.EnumerateTests();
      if (testDescriptors == null)
        throw new TestCompositionException(string.Format("Test session discovery {0} should return empty enumerable instead of null", discovery.GetType()));

      ITestFactoryService testFactoryService = null;
      foreach (var testDescriptor in testDescriptors)
        Execute(environment, testDescriptor, ref testFactoryService);
    }

    private static void Execute(ITestEnvironment environment, ITestDescriptor testDescriptor, ref ITestFactoryService testFactoryService)
    {
      if (testDescriptor.Identity == null)
        throw new TestCompositionException(string.Format("Test {0} should have identity", testDescriptor.GetType()));

      if (testFactoryService == null)
      {
        testFactoryService = environment.FactoryService;
        if (testFactoryService == null)
          throw new TestCompositionException(string.Format("Test session environment {0} should provide factory service", environment.GetType()));
      }

      var test = testDescriptor.CreateInstance(testFactoryService);
      if (test != null)
        test.Run();
    }
  }
}