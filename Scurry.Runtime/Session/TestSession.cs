using System;
using System.Collections.Generic;
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
      var environment = CreateEnvironment();
      var testDescriptors = CreateTestGraph(environment);

      foreach (var testDescriptor in testDescriptors)
        Execute(new EnvironmentTestContext(environment), testDescriptor);
    }

    private static void Execute(ITestContext parentContext, ITestDescriptor testDescriptor)
    {
      if (testDescriptor.Identity == null)
        throw new TestCompositionException(string.Format("Test {0} should have identity", testDescriptor.GetType()));

      var context = testDescriptor.Execute(parentContext);
      foreach (var childDescriptor in testDescriptor.Children)
        Execute(context, childDescriptor);
      var disposable = context as IDisposable;
      if (disposable != null)
        disposable.Dispose();
    }

    public IEnumerable<ITestDescriptor> CreateTestGraph()
    {
      var environment = CreateEnvironment();
      return CreateTestGraph(environment);
    }

    private IEnumerable<ITestDescriptor> CreateTestGraph(ITestEnvironment environment)
    {
      var discovery = environment.DiscoveryService;
      if (discovery == null)
        throw new TestConfigurationException(string.Format("Test session environment {0} should provide discovery service", environment.GetType()));

      var testDescriptors = discovery.CreateTestGraph();
      if (testDescriptors == null)
        throw new TestCompositionException(string.Format("Test session discovery {0} should return empty enumerable instead of null", discovery.GetType()));
      return testDescriptors;
    }

    private ITestEnvironment CreateEnvironment()
    {
      var environment = myConfiguration.CreateEnvironment();
      if (environment == null)
        throw new TestConfigurationException(string.Format("Test session configuration {0} should provide environment", myConfiguration.GetType()));
      return environment;
    }
  }
}