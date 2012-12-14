using System;

namespace Scurry.Framework
{
  /// <summary>
  /// Context for test execution.
  /// </summary>
  public interface ITestContext
  {
    object CreateInstance(Type type);
  }
}