using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestMasterService.Domain;

namespace QuestMasterService.Infrastructure.Postgres.Configurations;

public sealed class AnswerConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.ToTable("answers");
        builder.HasKey(answer => answer.Id);
        builder.Property(answer => answer.Id).HasColumnName("id");
        builder.Property(answer => answer.QuestionId).HasColumnName("question_id");
        builder.Property(answer => answer.AttemptId).HasColumnName("attempt_id");
        builder.Property(answer => answer.IsCorrect).HasColumnName("correctness").HasConversion<string>();
        builder.OwnsOne(answer => answer.PointsAwarded, points =>
        {
            points.Property(value => value.Value).HasColumnName("points_awarded").HasPrecision(18, 2);
        });
        builder.HasDiscriminator(answer => answer.Type)
            .HasValue<TextAnswer>(AnswerType.Text)
            .HasValue<MatchAnswer>(AnswerType.Match)
            .HasValue<OptionsAnswer>(AnswerType.Option);
    }
}
