using BlazorSozluk.Api.Domain.Entities;
using BlazorSozluk.Api.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BlazorSozluk.Infrastructure.Persistence.Context;

public class BlazorSozlukContext : DbContext
{
    public const string DEFAULT_SHEMA = "dbo";

    public BlazorSozlukContext()
    {

    }
    public BlazorSozlukContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // run olunmadan migration elavesini ede bilmek ucun burada elave olaraq yazilir. Migration veren zaman bos ctor cagirilacaq, bol olduguna gore asagidaki if bloku ise dusecek ve migration conStr ist ederek elave edilecek. Run olunan zaman ise diger ctor isleyecek ve configuration ile melumar elde edilecek 
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = "Data Source=localhost;Initial Catalog=blazorsozluk;Integrated Security = true; MultipleActiveResultSets = True";
            optionsBuilder.UseSqlServer(connectionString, option =>
            {
                option.EnableRetryOnFailure(); // connection zamani xeta alinarsa
            });
        }
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Entry> Entries { get; set; }
    public DbSet<EntryVote> EntryVotes { get; set; }
    public DbSet<EntryFavorite> EntryFavorites { get; set; }
    public DbSet<EntryComment> EntryComments { get; set; }
    public DbSet<EntryCommentFavorite> EntryCommentFavorites { get; set; }
    public DbSet<EntryCommentVote> EntryCommentVotes { get; set; }
    public DbSet<EmailConfirmation> EmailConfirmations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // cari assembly daxilinde ne qeder IEntityTypeConfiguration interfeysinden miras alan varsa onlari goturur
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override int SaveChanges()
    {
        OnBeforeSave();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSave();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        OnBeforeSave();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OnBeforeSave();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void OnBeforeSave()
    {
        var addedEntites = ChangeTracker.Entries()
                            .Where(i => i.State == EntityState.Added)
                            .Select(i => (BaseEntity)i.Entity);

        PrepareAddedEntities(addedEntites);
    }

    private void PrepareAddedEntities(IEnumerable<BaseEntity> entities)
    {
        foreach (var entity in entities)
            if (entity.CreatedDate == DateTime.MinValue)
                entity.CreatedDate = DateTime.Now;
    }
}
