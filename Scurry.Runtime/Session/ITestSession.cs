using System;
using System.Collections.Generic;
using Scurry.Framework;

namespace Scurry.Runtime
{
  /// <summary>
  /// Test session, usually constructed based on <see cref="ITestSessionConfiguration"/>
  /// </summary>
  public interface ITestSession
  {
    void Execute();
    IEnumerable<ITestDescriptor> EnumerateTests();
  }
}