using BlazorSozluk.Api.Domain.Entities;
using BlazorSozluk.Common.Infrasturucture;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infrastructure.Persistence.Context;

/// <summary>
/// Fake datalarin elave edilmesi ucun (1 defeye mexsus ist olunacaq)
/// </summary>
internal class SeedData
{
    /// <summary>
    /// User melumatlarinin hazirlanmasi ucun method
    /// </summary>
    /// <returns></returns>
    private static List<User> GetUsers()
    {
        var users = new Faker<User>("az") // az dilinde melumat
            .RuleFor(x => x.Id, x => Guid.NewGuid())
            .RuleFor(x => x.CreatedDate, x => x.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now)) // tarix araligi ucun
            .RuleFor(x => x.FirstName, x => x.Person.FirstName)
            .RuleFor(x => x.LastName, x => x.Person.LastName)
            //.RuleFor(x => x.EmailAdress, x => x.Internet.Email())      
            .RuleFor(x => x.EmailAdress, x => x.Person.Email)
            .RuleFor(x => x.UserName, x => x.Person.UserName)
            .RuleFor(x => x.Password, x => PasswordEncryptor.Encrpt(x.Internet.Password()))
            .RuleFor(x => x.EmailConfirmed, x => x.PickRandom(true, false)) // verilen deyerlerden birini secmesi ucun
            .Generate(500);

        return users;
    }

    /// <summary>
    /// EntryComment melumatlarinin hazirlanmasi ucun method
    /// </summary>
    /// <param name="userIds"></param>
    /// <param name="entryGuids"></param>
    /// <returns></returns>
    private static List<EntryComment> GetEntryComments(IEnumerable<Guid> userIds, List<Guid> entryGuids)
    {
        var entryComments = new Faker<EntryComment>("az") // az dilinde melumat
            .RuleFor(x => x.Id, x => Guid.NewGuid())
            .RuleFor(x => x.CreatedDate, x => x.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now)) // tarix araligi ucun
            .RuleFor(x => x.Content, x => x.Lorem.Paragraph(2))
            .RuleFor(x => x.CreatedById, x => x.PickRandom(userIds))
            .RuleFor(x => x.EntryId, x => x.PickRandom(entryGuids))
            .Generate(1000);

        return entryComments;
    }

    /// <summary>
    /// Entry melumatlarinin hazirlanmasi ucun method
    /// </summary>
    /// <param name="userIds"></param>
    /// <param name="entryGuids"></param>
    /// <returns></returns>
    private static List<Entry> GetEntries(IEnumerable<Guid> userIds, List<Guid> entryGuids)
    {
        int counter = 0;

        var entries = new Faker<Entry>("az") // az dilinde melumat
            .RuleFor(x => x.Id, x => entryGuids[counter++])
            .RuleFor(x => x.CreatedDate, x => x.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now)) // tarix araligi ucun
            .RuleFor(x => x.Subject, x => x.Lorem.Sentence(5, 7))
            .RuleFor(x => x.Content, x => x.Lorem.Paragraph(2))
            .RuleFor(x => x.CreatedById, x => x.PickRandom(userIds))
            .Generate(150);

        return entries;
    }

    public async Task SeedAsync(IConfiguration configuration)
    {
        //configurationdan melumatin oxunmasi
        var connectionString = configuration["BlazorSozlukDbConnectionString"].ToString();

        //DbContextOptionsBuilder modelini yaradaraq conStr melumatinin verilmesi
        var dbContextBuilder = new DbContextOptionsBuilder();
        dbContextBuilder.UseSqlServer(connectionString);

        //Context sinifinin ikinci ctor-nun cagirilmasi
        var context = new BlazorSozlukContext(dbContextBuilder.Options);

        //Eger melumatlar varsa islememesi ucun
        if (context.Users.Any())
        {
            await Task.CompletedTask;
            return;
        }

        //userler
        var users = GetUsers();

        //novbeti melumatlarda istifade ucun user id-leri
        var userIds = users.Select(users => users.Id);

        //userlerin elave edilmesi
        await context.Users.AddRangeAsync(users);

        //entriler ucun id - sonraki commentde istifade ucun nezerde tutulub
        var entryGuids = Enumerable.Range(0, 150).Select(i => Guid.NewGuid()).ToList();

        //entriler ve elave edilmesi
        var entries = GetEntries(userIds, entryGuids);
        await context.Entries.AddRangeAsync(entries);

        //entricommentler ve elave edilmesi
        var entryComments = GetEntryComments(userIds, entryGuids);
        await context.EntryComments.AddRangeAsync(entryComments);

        await context.SaveChangesAsync();
    }

}
