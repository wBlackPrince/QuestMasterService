using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestMasterService.Domain;

namespace QuestMasterService.Infrastructure.Postgres.Configurations;

public sealed class MatchQuestionConfiguration : IEntityTypeConfiguration<MatchQuestion>
{
    public void Configure(EntityTypeBuilder<MatchQuestion> builder)
    {
        builder.HasBaseType<Question>();

        builder.PrimitiveCollection(question => question.Matches)
            .HasField("_matchIds")
            .HasColumnName("match_ids");
    }
}
