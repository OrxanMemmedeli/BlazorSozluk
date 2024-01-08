using BlazorSozluk.Api.Domain.Entities.Common;
using BlazorSozluk.Common.ViewModels;

namespace BlazorSozluk.Api.Domain.Entities;

public class EntryVote : BaseEntity
{
    public VoteType VoteType { get; set; }
    public Guid CreatedById { get; set; }
    public Guid EntryId { get; set; }

    public virtual User CreatedBy { get; set; }
    public virtual Entry Entry { get; set; }

}