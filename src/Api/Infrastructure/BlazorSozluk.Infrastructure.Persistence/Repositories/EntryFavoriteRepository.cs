using BlazorSozluk.Api.Application.Abstract.Repositories;
using BlazorSozluk.Api.Domain.Entities;
using BlazorSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BlazorSozluk.Infrastructure.Persistence.Repositories;

public class EntryFavoriteRepository : GenericRepository<EntryFavorite>, IEntryFavoriteRepository
{
    public EntryFavoriteRepository(DbContext context) : base(context)
    {
    }
}
