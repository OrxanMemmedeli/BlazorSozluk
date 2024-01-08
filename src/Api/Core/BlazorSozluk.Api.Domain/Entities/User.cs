using BlazorSozluk.Api.Domain.Entities.Common;

namespace BlazorSozluk.Api.Domain.Entities;

public class User : BaseEntity
{
    public User()
    {
        this.Entries = new HashSet<Entry>();
        this.EntryVotes = new HashSet<EntryVote>();
        this.EntryFavorites = new HashSet<EntryFavorite>();
        this.EntryComments = new HashSet<EntryComment>();
        this.EntryCommentFavorites = new HashSet<EntryCommentFavorite>();
        this.EntryCommentVotes = new HashSet<EntryCommentVote>();
    }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAdress { get; set; }
    public string Password { get; set; }
    public bool EmailConfirmed { get; set; }

    public virtual ICollection<Entry> Entries { get; set; }
    public virtual ICollection<EntryVote> EntryVotes { get; set; }
    public virtual ICollection<EntryFavorite> EntryFavorites { get; set; }
    public virtual ICollection<EntryComment> EntryComments { get; set; }
    public virtual ICollection<EntryCommentFavorite> EntryCommentFavorites { get; set; }
    public virtual ICollection<EntryCommentVote> EntryCommentVotes { get; set; }
}