using BlazorSozluk.Api.Application.Abstract.Repositories;
using BlazorSozluk.Api.Domain.Entities;
using BlazorSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BlazorSozluk.Infrastructure.Persistence.Repositories;

public class EntryCommentFavoriteRepository : GenericRepository<EntryCommentFavorite>, IEntryCommentFavoriteRepository
{
    public EntryCommentFavoriteRepository(DbContext context) : base(context)
    {
    }
}
