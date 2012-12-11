using System;
using Scurry.Framework;

namespace Scurry.Runtime.Tests
{
  public class AnonymousTestInstance : ITestInstance
  {
    private readonly Action myAction;

    public AnonymousTestInstance(Action action)
    {
      myAction = action;
    }

    public void Run()
    {
      myAction();
    }
  }
}