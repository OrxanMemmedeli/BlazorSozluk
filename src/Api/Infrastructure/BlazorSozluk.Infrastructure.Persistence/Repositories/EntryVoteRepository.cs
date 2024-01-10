using BlazorSozluk.Api.Application.Abstract.Repositories;
using BlazorSozluk.Api.Domain.Entities;
using BlazorSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BlazorSozluk.Infrastructure.Persistence.Repositories;

public class EntryVoteRepository : GenericRepository<EntryVote>, IEntryVoteRepository
{
    public EntryVoteRepository(DbContext context) : base(context)
    {
    }
}
