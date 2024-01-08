using BlazorSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorSozluk.Infrastructure.Persistence.EntityConfigurations.Entry;

public class EntryEntityConfiguration : BaseEntityConfiguration<Api.Domain.Entities.Entry>
{
    public override void Configure(EntityTypeBuilder<Api.Domain.Entities.Entry> builder)
    {
        base.Configure(builder);

        builder.ToTable("entry", BlazorSozlukContext.DEFAULT_SHEMA);

        builder.HasOne(i => i.CreatedBy)
            .WithMany(i => i.Entries)
            .HasForeignKey(i => i.CreatedById)
            .OnDelete(DeleteBehavior.Restrict); // set null
    }
}
