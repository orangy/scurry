namespace Scurry.Framework
{
  /// <summary>
  /// Environment in which tests are being run. Service provider, root of extensibility. 
  /// </summary>
  public interface ITestEnvironment
  {
    /// <summary>
    /// Get service for discovering tests
    /// </summary>
    /// <value></value>
    ITestDiscoveryService DiscoveryService { get; }

    /// <summary>
    /// Gets service for creating instances of various types
    /// </summary>
    ITestFactoryService FactoryService { get; }
  }
}