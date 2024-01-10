using BlazorSozluk.Api.Application.Abstract.Repositories;
using BlazorSozluk.Api.Domain.Entities;
using BlazorSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BlazorSozluk.Infrastructure.Persistence.Repositories;

public class EntryCommentRepository : GenericRepository<EntryComment>, IEntryCommentRepository
{
    public EntryCommentRepository(DbContext context) : base(context)
    {
    }
}
