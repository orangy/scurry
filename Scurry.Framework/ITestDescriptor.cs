using System;
using System.Collections.Generic;

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
    /// Executes descriptor in the given context. Can provide context for children execution.
    /// </summary>
    /// <param name="context">Context for test execution</param>
    /// <returns>Nested context for children, or null</returns>
    ITestContext Execute(ITestContext context);

    ITestDescriptor Container { get; }

    IEnumerable<ITestDescriptor> Children { get; }
  }
}