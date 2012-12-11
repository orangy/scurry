using System;

namespace Scurry.Framework
{
  /// <summary>
  /// There was an issue with test environment configuration
  /// </summary>
  public class TestConfigurationException : Exception
  {
    public TestConfigurationException(string message) : base(message)
    {
    }
  }
}