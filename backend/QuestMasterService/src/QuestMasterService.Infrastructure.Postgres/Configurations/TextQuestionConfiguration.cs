using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestMasterService.Domain;

namespace QuestMasterService.Infrastructure.Postgres.Configurations;

public sealed class TextQuestionConfiguration : IEntityTypeConfiguration<TextQuestion>
{
    public void Configure(EntityTypeBuilder<TextQuestion> builder)
    {
        builder.OwnsOne(question => question.RightAnswer, answer =>
        {
            answer.Property(value => value.Value).HasColumnName("right_answer").HasMaxLength(500).IsRequired();
        });
    }
}
