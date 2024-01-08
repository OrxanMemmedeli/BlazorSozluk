using BlazorSozluk.Api.Domain.Entities.Common;

namespace BlazorSozluk.Api.Domain.Entities;

public class EntryFavorite : BaseEntity
{
    public Guid CreatedById { get; set; }
    public Guid EntryId { get; set; }

    public virtual User CreatedBy { get; set; }
    public virtual Entry Entry { get; set; }
}