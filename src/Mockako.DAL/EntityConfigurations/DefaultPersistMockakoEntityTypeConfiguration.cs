using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mockako.DAL.Entities;

namespace Mockako.DAL.EntityConfigurations;

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