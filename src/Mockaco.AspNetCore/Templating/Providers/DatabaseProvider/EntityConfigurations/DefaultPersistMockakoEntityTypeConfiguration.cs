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

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasMaxLength(100)
            .HasColumnName("task_id");

        builder.Property(x => x.IsActive)
            .HasColumnName("is_active")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.Config)
            .HasColumnName("config")
            .IsRequired();

        builder.Property(x => x.ModifiedDateTime)
            .HasColumnName("modified_dt")
            .IsRequired();

        builder.Property(x => x.ApplicationId)
            .HasColumnName("application_id")
            .IsRequired();



        // TODO continue configuring
    }
}
