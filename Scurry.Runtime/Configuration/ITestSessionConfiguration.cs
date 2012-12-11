using System;
using Scurry.Framework;

namespace Scurry.Runtime
{
  /// <summary>
  /// Configuration for creating environments
  /// </summary>
  public interface ITestSessionConfiguration
  {
    ITestEnvironment CreateEnvironment();
  }
}