using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestMasterService.Domain;

namespace QuestMasterService.Infrastructure.Postgres.Configurations;

public sealed class MatchConfiguration : IEntityTypeConfiguration<Match>
{
    public void Configure(EntityTypeBuilder<Match> builder)
    {
        builder.ToTable("matches");
        builder.HasKey(match => match.Id);
        builder.Property(match => match.Id).HasColumnName("id");
        builder.Property(match => match.QuestionId).HasColumnName("question_id");
        builder.Property(match => match.IsCorrect).HasColumnName("is_correct");
        builder.OwnsOne(match => match.Left, left =>
        {
            left.Property(value => value.Value).HasColumnName("left_value").HasMaxLength(500).IsRequired();
        });
        builder.OwnsOne(match => match.Right, right =>
        {
            right.Property(value => value.Value).HasColumnName("right_value").HasMaxLength(500).IsRequired();
        });
        builder.HasOne<MatchQuestion>().WithMany()
            .HasForeignKey(match => match.QuestionId).OnDelete(DeleteBehavior.Cascade);
    }
}
