using BlazorSozluk.Api.Domain.Entities.Common;

namespace BlazorSozluk.Api.Domain.Entities;

public class EntryComment : BaseEntity
{
    public EntryComment()
    {
        this.EntryCommentVotes = new HashSet<EntryCommentVote>();
        this.EntryCommentFavorites = new HashSet<EntryCommentFavorite>();
    }
    public string Content { get; set; }
    public Guid CreatedById { get; set; }
    public Guid EntryId { get; set; }

    public virtual User CreatedBy { get; set; }
    public virtual Entry Entry { get; set; }
    public virtual ICollection<EntryCommentVote> EntryCommentVotes { get; set; }
    public virtual ICollection<EntryCommentFavorite> EntryCommentFavorites { get; set; }
}