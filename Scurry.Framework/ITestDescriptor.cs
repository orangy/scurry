using System;

namespace Scurry.Framework
{
  public interface ITestDescriptor
  {
    ITestDescriptor Container { get; }

    ITestIdentity Identity { get; }



  }
}