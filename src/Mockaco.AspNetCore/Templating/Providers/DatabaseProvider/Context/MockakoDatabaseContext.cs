using Microsoft.EntityFrameworkCore;
using Mockaco.Templating.Providers.DatabaseProvider.EntityConfigurations;
using Mockaco.Templating.Providers.DatabaseProvider.Models;

namespace Mockaco.Templating.Providers.DatabaseProvider.Context;

public partial class MockakoDatabaseContext<TKey> : DbContext where TKey : IEquatable<TKey>
{
    public virtual DbSet<MockakoRestConfig<TKey>> MockakoRestConfigs { get; set; }
    private const string DefaultTableName = "mockako_rest_configs";

    private readonly IEntityTypeConfiguration<MockakoRestConfig<TKey>> _mockakoRestConfig;
    private readonly string _schemaName;

    public MockakoDatabaseContext()
    {
        _mockakoRestConfig = new DefaultPersistMockakoEntityTypeConfiguration<TKey>(DefaultTableName);
    }

    public MockakoDatabaseContext(DbContextOptions<MockakoDatabaseContext<TKey>> options,
        string schemaName = null, string tableName = DefaultTableName)
    :base(options)
    {
        _mockakoRestConfig = new DefaultPersistMockakoEntityTypeConfiguration<TKey>(tableName);
        _schemaName = schemaName;
    }

    protected MockakoDatabaseContext(DbContextOptions options,
        string schemaName = null,
        string tableName = DefaultTableName)
        : base(options)
    {
        _mockakoRestConfig = new DefaultPersistMockakoEntityTypeConfiguration<TKey>(tableName);
        _schemaName = schemaName;
    }
}