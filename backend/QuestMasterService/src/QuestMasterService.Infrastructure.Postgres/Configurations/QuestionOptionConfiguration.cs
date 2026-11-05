using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestMasterService.Domain;

namespace QuestMasterService.Infrastructure.Postgres.Configurations;

public sealed class QuestionOptionConfiguration : IEntityTypeConfiguration<QuestionOption>
{
    public void Configure(EntityTypeBuilder<QuestionOption> builder)
    {
        builder.ToTable("question_options");
        builder.HasKey(option => option.Id);
        builder.Property(option => option.Id).HasColumnName("id");
        builder.Property(option => option.QuestionId).HasColumnName("question_id");
        builder.Property(option => option.IsCorrect).HasColumnName("is_correct");
        builder.OwnsOne(option => option.Text, text =>
        {
            text.Property(value => value.Value).HasColumnName("text").HasMaxLength(500).IsRequired();
        });
        builder.HasOne<OptionQuestion>().WithMany()
            .HasForeignKey(option => option.QuestionId).OnDelete(DeleteBehavior.Cascade);
    }
}
