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
      var discovery = environment.CreateDiscovery();
      if (discovery == null)
        throw new TestConfigurationException(string.Format("Test session environment {0} should provide test discovery service", environment.GetType()));

      var testDescriptors = discovery.EnumerateTests();
      if (testDescriptors == null)
        throw new TestCompositionException(string.Format("Test session discovery {0} should return empty enumerable instead of null", discovery.GetType()));

      ITestFactory testFactory = null;
      foreach (var testDescriptor in testDescriptors)
        Execute(environment, testDescriptor, ref testFactory);
    }

    private static void Execute(ITestEnvironment environment, ITestDescriptor testDescriptor, ref ITestFactory testFactory)
    {
      if (testDescriptor.Identity == null)
        throw new TestCompositionException(string.Format("Test {0} should have identity", testDescriptor.GetType()));

      if (testFactory == null)
      {
        testFactory = environment.CreateFactory();
        if (testFactory == null)
          throw new TestCompositionException(string.Format("Test session environment {0} should provide test factory service", environment.GetType()));
      }

      var test = testFactory.CreateInstance(testDescriptor);
      if (test == null)
        return;

      test.Execute();
    }
  }
}