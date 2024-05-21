namespace Mockaco.Templating.Providers.DatabaseProvider.Models;

public class MockakoRestConfig<TKey> where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; }
    public bool IsActive { get; set; }
    public string Config { get; set; }
    public DateTime ModifiedDateTime { get; set; }
}