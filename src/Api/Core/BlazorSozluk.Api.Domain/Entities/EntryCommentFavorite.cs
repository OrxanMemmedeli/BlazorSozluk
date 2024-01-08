using BlazorSozluk.Api.Domain.Entities.Common;

namespace BlazorSozluk.Api.Domain.Entities;

public class EntryCommentFavorite : BaseEntity
{
    public Guid CreatedById { get; set; }
    public Guid EntryCommentId { get; set; }

    public virtual User CreatedBy { get; set; }
    public virtual EntryComment EntryComment { get; set; }
}