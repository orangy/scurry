namespace Scurry.Framework
{
  public interface ITestFactory
  {
    ITestInstance CreateInstance(ITestDescriptor testDescriptor);
  }
}