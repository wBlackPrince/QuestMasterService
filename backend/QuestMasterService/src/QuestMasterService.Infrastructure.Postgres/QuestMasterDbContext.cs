using Microsoft.EntityFrameworkCore;
using QuestMasterService.Domain;

namespace QuestMasterService.Infrastructure.Postgres;

public sealed class QuestMasterDbContext(DbContextOptions<QuestMasterDbContext> options) : DbContext(options)
{
    public DbSet<Attempt> Attempts => Set<Attempt>();
    public DbSet<Answer> Answers => Set<Answer>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<Match> Matches => Set<Match>();
    public DbSet<QuestionOption> QuestionOptions => Set<QuestionOption>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(QuestMasterDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
