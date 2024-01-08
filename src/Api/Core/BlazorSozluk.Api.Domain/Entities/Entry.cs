using BlazorSozluk.Api.Domain.Entities.Common;

namespace BlazorSozluk.Api.Domain.Entities;

public class Entry : BaseEntity
{
    public Entry()
    {
        this.EntryComments = new HashSet<EntryComment>();
        this.EntryVotes = new HashSet<EntryVote>();
        this.EntryFavorites = new HashSet<EntryFavorite>();
    }
    public string Subject { get; set; }
    public string Content { get; set; }
    public Guid CreatedById { get; set; }

    public virtual User CreatedBy { get; set; }
    public virtual ICollection<EntryComment> EntryComments { get; set; }
    public virtual ICollection<EntryVote> EntryVotes { get; set; }
    public virtual ICollection<EntryFavorite> EntryFavorites { get; set; }
}
