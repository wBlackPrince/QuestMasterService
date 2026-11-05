using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestMasterService.Domain;

namespace QuestMasterService.Infrastructure.Postgres.Configurations;

public sealed class AttemptConfiguration : IEntityTypeConfiguration<Attempt>
{
    public void Configure(EntityTypeBuilder<Attempt> builder)
    {
        builder.ToTable("attempts");
        builder.HasKey(attempt => attempt.Id);
        builder.Property(attempt => attempt.Id).HasColumnName("id");
        builder.Property(attempt => attempt.TestId).HasColumnName("test_id");
        builder.Property(attempt => attempt.UserId).HasColumnName("user_id");
        builder.Property(attempt => attempt.StartedAt).HasColumnName("started_at");
        builder.Property(attempt => attempt.FinishedAt).HasColumnName("finished_at");
        builder.OwnsOne(attempt => attempt.TotalScore, score =>
        {
            score.Property(value => value.Value).HasColumnName("total_score").HasPrecision(18, 2);
        });
        builder.Navigation(attempt => attempt.TotalScore).IsRequired(false);
        builder.HasMany(attempt => attempt.Answers).WithOne()
            .HasForeignKey(answer => answer.AttemptId).OnDelete(DeleteBehavior.Cascade);
        builder.Navigation(attempt => attempt.Answers).HasField("_answers");
    }
}
