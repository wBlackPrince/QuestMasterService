using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestMasterService.Domain;

namespace QuestMasterService.Infrastructure.Postgres.Configurations;

public sealed class MatchAnswerConfiguration : IEntityTypeConfiguration<MatchAnswer>
{
    public void Configure(EntityTypeBuilder<MatchAnswer> builder)
    {
        builder.PrimitiveCollection(answer => answer.UserMatches)
            .HasField("_userMatches")
            .HasColumnName("user_matches");
    }
}
