using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using umfgcloud.loja.dominio.service.Entidades;

namespace umfgcloud.infraestrutura.service.Map;

internal abstract class AbstractEntityMap<T> : IEntityTypeConfiguration<T> where T : AbstractEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("ID");
        builder.Property(x => x.CreatedAt).HasColumnName("DT_CREATE");
        builder.Property(x => x.UpdatedAt).HasColumnName("DT_UPDATE");
        builder.Property(x => x.CreatedByUserId).HasColumnName("ID_USER_CREEATE");
        builder.Property(x => x.UpdatedByUserId).HasColumnName("ID_USER_UPDATE");
        builder.Property(x => x.CreatedByEmail).HasColumnName("DS_USER_EMAIL_CREATE");
        builder.Property(x => x.UpdatedByEmail).HasColumnName("DS_USER_EMAIL_UPDATE");
        builder.Property(x => x.IsActive).HasColumnName("IN_ACTIVE");
    }
}