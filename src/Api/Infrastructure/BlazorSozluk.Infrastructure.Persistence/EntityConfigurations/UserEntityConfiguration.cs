using BlazorSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorSozluk.Infrastructure.Persistence.EntityConfigurations;

public class UserEntityConfiguration : BaseEntityConfiguration<Api.Domain.Entities.User>
{
    public override void Configure(EntityTypeBuilder<Api.Domain.Entities.User> builder)
    {
        base.Configure(builder);

        builder.ToTable("user", BlazorSozlukContext.DEFAULT_SHEMA);
    }
}
