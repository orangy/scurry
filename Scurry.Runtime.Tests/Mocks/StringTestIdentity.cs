using System;
using Scurry.Framework;

namespace Scurry.Runtime.Tests
{
  public class StringTestIdentity : ITestIdentity
  {
    private readonly string myValue;

    public StringTestIdentity(string value)
    {
      myValue = value;
    }

    public string Value
    {
      get { return myValue; }
    }
  }
}