namespace Trainer.Persistence.Extensions
{
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public static class EntityTypeBuilderExtensions
    {
        public static EntityTypeBuilder<TEntity> MapTable<TEntity>(this EntityTypeBuilder<TEntity> builder,
            string tableName, string? schemaName = null)
            where TEntity : class, IBaseEntity
        {
            builder.ToTable(tableName, schemaName);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("newsequentialid()");

            return builder;
        }
    }
}
