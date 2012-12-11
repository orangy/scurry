using System;

namespace Scurry.Framework
{
  /// <summary>
  /// Factory for objects of various types, usually bound to environment
  /// </summary>
  public interface ITestFactoryService
  {
    object CreateInstance(Type type);
  }
}