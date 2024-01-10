using BlazorSozluk.Api.Application.Abstract.Repositories;
using BlazorSozluk.Api.Domain.Entities;
using BlazorSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BlazorSozluk.Infrastructure.Persistence.Repositories;

public class EntryCommentVoteRepository : GenericRepository<EntryCommentVote>, IEntryCommentVoteRepository
{
    public EntryCommentVoteRepository(DbContext context) : base(context)
    {
    }
}
