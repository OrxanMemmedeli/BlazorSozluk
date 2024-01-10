using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BlazorSozluk.Api.Application.Extensions;

public static class Registration
{
    public static IServiceCollection AddApplicationRegistration(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly(); // isleyen assembly tapir. (WebApi isleyecek - isleyen zaman referans aldiqlarinida isledeceyine gore avtomatik burani gore bilecekdir)

        services.AddMediatR(assembly);
        services.AddAutoMapper(assembly);
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
