using System;

namespace Scurry.Framework
{
  /// <summary>
  /// Identity of test. Should be easy to serialize to string data and restore from it. Used to identify tests across sessions, appdomains, reports, etc.
  /// </summary>
  public interface ITestIdentity
  {
  }
}