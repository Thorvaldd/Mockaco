using System;
using Microsoft.EntityFrameworkCore;
using Mockako.DAL.Entities;
using Mockako.DAL.EntityConfigurations;

namespace Mockako.DAL.Context;

public class MockakoDatabaseContext<TKey> : DbContext where TKey : IEquatable<TKey>
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
        : base(options)
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

    public MockakoDatabaseContext(DbContextOptions<MockakoDatabaseContext<TKey>> options,
        IEntityTypeConfiguration<MockakoRestConfig<TKey>> modelConfig,
        string schemaName = null)
        : base(options)
    {
        _mockakoRestConfig = modelConfig;
        _schemaName = schemaName;
    }

    protected MockakoDatabaseContext(DbContextOptions options,
        IEntityTypeConfiguration<MockakoRestConfig<TKey>> modelConfig,
        string schemaName = null)
        : base(options)
    {
        _mockakoRestConfig = modelConfig;
        _schemaName = schemaName;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (string.IsNullOrEmpty(_schemaName) is false)
        {
            modelBuilder.HasDefaultSchema(_schemaName);
        }

        if (_mockakoRestConfig is not null)
        {
            modelBuilder.ApplyConfiguration(_mockakoRestConfig);
        }
    }
}