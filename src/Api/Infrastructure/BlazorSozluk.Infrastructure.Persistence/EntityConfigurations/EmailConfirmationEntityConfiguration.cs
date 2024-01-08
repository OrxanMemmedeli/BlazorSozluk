using BlazorSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorSozluk.Infrastructure.Persistence.EntityConfigurations;

public class EmailConfirmationEntityConfiguration : BaseEntityConfiguration<Api.Domain.Entities.EmailConfirmation>
{
    public override void Configure(EntityTypeBuilder<Api.Domain.Entities.EmailConfirmation> builder)
    {
        base.Configure(builder);

        builder.ToTable("emailconfirmation", BlazorSozlukContext.DEFAULT_SHEMA);
    }
}
