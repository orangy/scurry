using System;
using System.Collections.Generic;

namespace Scurry.Framework
{
  /// <summary>
  /// Service for discovering tests. 
  /// </summary>
  /// <remarks>
  /// Composition pattern could be used to provide multiple discovery mechanisms for a given environment
  /// </remarks>
  public interface ITestDiscoveryService
  {
    IEnumerable<ITestDescriptor> EnumerateTests();
  }
}