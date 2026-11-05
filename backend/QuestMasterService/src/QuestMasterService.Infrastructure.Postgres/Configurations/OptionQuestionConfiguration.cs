using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestMasterService.Domain;

namespace QuestMasterService.Infrastructure.Postgres.Configurations;

public sealed class OptionQuestionConfiguration : IEntityTypeConfiguration<OptionQuestion>
{
    public void Configure(EntityTypeBuilder<OptionQuestion> builder)
    {
        builder.PrimitiveCollection(question => question.Options)
            .HasField("_optionIds")
            .HasColumnName("option_ids");
    }
}
