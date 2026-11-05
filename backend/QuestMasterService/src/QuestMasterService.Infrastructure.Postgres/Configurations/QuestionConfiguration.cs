using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestMasterService.Domain;

namespace QuestMasterService.Infrastructure.Postgres.Configurations;

public sealed class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("questions");
        builder.HasKey(question => question.Id);
        builder.Property(question => question.Id).HasColumnName("id");
        builder.Property(question => question.TestId).HasColumnName("test_id");
        builder.OwnsOne(question => question.Description, description =>
        {
            description.Property(value => value.Value).HasColumnName("description").HasMaxLength(2000).IsRequired();
        });
        builder.OwnsOne(question => question.PointsCost, points =>
        {
            points.Property(value => value.Value).HasColumnName("points_cost").HasPrecision(18, 2);
        });
        builder.OwnsOne(question => question.PositionIndex, position =>
        {
            position.Property(value => value.Value).HasColumnName("position_index");
        });
        builder.HasDiscriminator(question => question.Type)
            .HasValue<TextQuestion>(QuestionType.Text)
            .HasValue<MatchQuestion>(QuestionType.Match)
            .HasValue<OptionQuestion>(QuestionType.Option);
    }
}
