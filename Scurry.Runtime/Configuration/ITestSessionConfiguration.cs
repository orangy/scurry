using System;
using Scurry.Framework;

namespace Scurry.Runtime
{
  public interface ITestSessionConfiguration
  {
    ITestEnvironment CreateEnvironment();
  }
}