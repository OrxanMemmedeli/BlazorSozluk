using BlazorSozluk.Api.Application.Abstract.Repositories;
using BlazorSozluk.Infrastructure.Persistence.Context;
using BlazorSozluk.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace BlazorSozluk.Infrastructure.Persistence.Extensions;

public static class Registration
{
    public static IServiceCollection AddInfrastuructureRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DbContext, BlazorSozlukContext>(conf =>
        {
            var connectionString = configuration["BlazorSozlukDbConnectionString"].ToString();
            conf.UseSqlServer(connectionString, option =>
            {
                option.EnableRetryOnFailure(); // connection zamani xeta alinarsa
            });
        });


        SeedData seedData = new SeedData();
        seedData.SeedAsync(configuration).GetAwaiter().GetResult();

        AddRepositories(services);

        return services;
    }

    //private static void AddRepositories(IServiceCollection services)
    //{
    //    services.AddScoped<IEmailConfirmationRepository, EmailConfirmationRepository>();
    //    services.AddScoped<IEntryCommentFavoriteRepository, EntryCommentFavoriteRepository>();
    //    services.AddScoped<IEntryCommentRepository, EntryCommentRepository>();
    //    services.AddScoped<IEntryCommentVoteRepository, EntryCommentVoteRepository>();
    //    services.AddScoped<IEntryFavoriteRepository, EntryFavoriteRepository>();
    //    services.AddScoped<IEntryRepository, EntryRepository>();
    //    services.AddScoped<IEntryVoteRepository, EntryVoteRepository>();
    //    services.AddScoped<IUserRepository, UserRepository>();
    //}

    private static void AddRepositories(IServiceCollection services)
    {
        // BlazorSozluk.Infrastructure.Persistence.Repositories unvanindaki repo siniflerinin type-larini tapir.
        var repositoryTypes = Assembly.GetAssembly(typeof(GenericRepository<>))
            .GetTypes()
            .Where(type => type.IsClass && !type.IsAbstract && !type.IsNested && !type.IsGenericType && type.Namespace == "BlazorSozluk.Infrastructure.Persistence.Repositories")
            .ToList();

        //BlazorSozluk.Api.Application.Abstract.Repositories unvanindaki repo interfeyslerinin type-larini tapir.
        var repositoryInterfaceTypes = Assembly.GetAssembly(typeof(IGenericRepository<>))
            .GetTypes()
            .Where(type => !type.IsClass && type.IsAbstract && !type.IsNested && !type.IsGenericType && type.Namespace == "BlazorSozluk.Api.Application.Abstract.Repositories")
            .ToList();

        // tipleri donerek melumatlar add edilir.
        foreach (var repositoryType in repositoryTypes)
        {
            //reponun adina gore interface-in tapilmasi
            Type repositoryInterfaceType = repositoryInterfaceTypes.SingleOrDefault(x => x.Name.Equals($"I{repositoryType.Name}", StringComparison.OrdinalIgnoreCase));

            services.AddScoped(repositoryInterfaceType, repositoryType);
        }
    }

}
