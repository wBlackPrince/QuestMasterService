using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestMasterService.Domain;

namespace QuestMasterService.Infrastructure.Postgres.Configurations;

public sealed class TestConfiguration : IEntityTypeConfiguration<Test>
{
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        builder.ToTable("tests");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(x => x.Title)
            .HasColumnName("title")
            .HasConversion(
                title => title.Value,
                value => TestTitle.Create(value).Value)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasConversion(
                description => description.Value,
                value => TestDescription.Create(value).Value)
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(x => x.MaxAttempts)
            .HasColumnName("max_attempts")
            .HasConversion(
                attempts => attempts.Value,
                value => MaxAttempts.Create(value).Value)
            .IsRequired();

        builder.HasMany(x => x.Questions)
            .WithOne()
            .HasForeignKey("TestId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}