using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestMasterService.Domain;

namespace QuestMasterService.Infrastructure.Postgres.Configurations;

public sealed class TextAnswerConfiguration : IEntityTypeConfiguration<TextAnswer>
{
    public void Configure(EntityTypeBuilder<TextAnswer> builder)
    {
        builder.OwnsOne(answer => answer.UserAnswer, userAnswer =>
        {
            userAnswer.Property(value => value.Value).HasColumnName("user_answer").HasMaxLength(500).IsRequired();
        });
    }
}
