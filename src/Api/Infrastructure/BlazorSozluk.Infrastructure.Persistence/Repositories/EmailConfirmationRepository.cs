using BlazorSozluk.Api.Application.Abstract.Repositories;
using BlazorSozluk.Api.Domain.Entities;
using BlazorSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BlazorSozluk.Infrastructure.Persistence.Repositories;

public class EmailConfirmationRepository : GenericRepository<EmailConfirmation>, IEmailConfirmationRepository
{
    public EmailConfirmationRepository(DbContext context) : base(context)
    {
    }
}
