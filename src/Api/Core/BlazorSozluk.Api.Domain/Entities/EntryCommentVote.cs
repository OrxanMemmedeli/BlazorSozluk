using BlazorSozluk.Api.Domain.Entities.Common;
using BlazorSozluk.Common.ViewModels;

namespace BlazorSozluk.Api.Domain.Entities;

public class EntryCommentVote : BaseEntity
{
    public VoteType VoteType { get; set; }
    public Guid CreatedById { get; set; }
    public Guid EntryCommentId { get; set; }

    public virtual User CreatedBy { get; set; }
    public virtual EntryComment EntryComment { get; set; }
}