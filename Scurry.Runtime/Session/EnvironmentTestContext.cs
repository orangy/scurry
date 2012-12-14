using System;
using Scurry.Framework;

namespace Scurry.Runtime
{
  public class EnvironmentTestContext : ITestContext
  {
    private readonly ITestEnvironment myEnvironment;
    private ITestFactoryService myFactoryService;

    public EnvironmentTestContext(ITestEnvironment environment)
    {
      myEnvironment = environment;
    }

    public object CreateInstance(Type type)
    {
      if (myFactoryService == null)
      {
        myFactoryService = myEnvironment.FactoryService;
        if (myFactoryService == null)
          throw new TestConfigurationException("Factory service should be provided by environment");
      }
      return myFactoryService.CreateInstance(type);
    }
  }
}