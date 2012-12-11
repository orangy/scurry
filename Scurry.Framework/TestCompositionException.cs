using System;

namespace Scurry.Framework
{
  /// <summary>
  /// There was an issue with test composition
  /// </summary>
  public class TestCompositionException : Exception
  {
    public TestCompositionException(string message) : base(message)
    {
    }
  }
}