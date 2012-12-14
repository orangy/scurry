using System;
using System.Reflection;
using Scurry.Framework;
using Scurry.Runtime;
using Scurry.Runtime.NUnit;

namespace Scurry.Console
{
  internal static class Program
  {
    private static void Main(string[] args)
    {
      foreach (var arg in args)
      {
        var configuration = new ConsoleTestConfiguration(arg);
        var session = new TestSession(configuration);
        session.Execute();
      }
    }
  }

  internal class ConsoleTestConfiguration : ITestSessionConfiguration
  {
    private readonly Assembly myAssembly;

    public ConsoleTestConfiguration(string assemblyPath)
    {
      myAssembly = Assembly.LoadFrom(assemblyPath);
    }

    public ITestEnvironment CreateEnvironment()
    {
      return new NUnitTestEnvironment(myAssembly);
    }

  }
}