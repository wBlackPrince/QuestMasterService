using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestMasterService.Domain;

namespace QuestMasterService.Infrastructure.Postgres.Configurations;

public sealed class OptionAnswerConfiguration : IEntityTypeConfiguration<OptionsAnswer>
{
    public void Configure(EntityTypeBuilder<OptionsAnswer> builder)
    {
        builder.PrimitiveCollection(answer => answer.UserOptions)
            .HasField("_userOptions")
            .HasColumnName("user_options");
    }
}
