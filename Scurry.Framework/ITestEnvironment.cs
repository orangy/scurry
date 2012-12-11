namespace Scurry.Framework
{
  public interface ITestEnvironment
  {
    ITestDiscovery CreateDiscovery();
    ITestFactory CreateFactory();
  }
}