using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mockaco.Templating.Providers.DatabaseProvider.Models;

namespace Mockaco.Templating.Providers.DatabaseProvider.EntityConfigurations;

public class DefaultPersistMockakoEntityTypeConfiguration<TKey>
    : IEntityTypeConfiguration<MockakoRestConfig<TKey>>
    where TKey : IEquatable<TKey>
{
    private readonly string _tableName;
    
    public DefaultPersistMockakoEntityTypeConfiguration(string tableName)
    {
        _tableName = tableName;
    }

    public void Configure(EntityTypeBuilder<MockakoRestConfig<TKey>> builder)
    {
        builder.ToTable(_tableName);

        builder.Property(x => x.Id)
            .HasColumnName("id");
        
        // TODO continue configuring
    }
}