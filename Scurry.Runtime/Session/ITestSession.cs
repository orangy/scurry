using System;

namespace Scurry.Runtime
{
  /// <summary>
  /// Test session, usually constructed based on <see cref="ITestSessionConfiguration"/>
  /// </summary>
  public interface ITestSession
  {
    void Execute();
  }
}