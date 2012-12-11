using System;

namespace Scurry.Framework
{
  /// <summary>
  /// Element of test discovery, could be fixture, test, row, etc
  /// </summary>
  /// <remarks>
  /// Hierarchies of tests are being created by creating parent/child relationships of descriptors, which instantiates recursively
  /// </remarks>
  public interface ITestDescriptor
  {
    /// <summary>
    /// Test identity
    /// </summary>
    ITestIdentity Identity { get; }

    /// <summary>
    /// Creates instance of test from this descriptor using provided factory to get other instances. 
    /// </summary>
    /// <param name="factoryService"></param>
    /// <returns></returns>
    ITestInstance CreateInstance(ITestFactoryService factoryService);
  }
}