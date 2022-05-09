using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MateMachine.CodeChallenge.SocialApp.Entities.Configs;

public class AppUserTokenConfig : IEntityTypeConfiguration<AppUserToken> {
	public void Configure(EntityTypeBuilder<AppUserToken> builder) {
		builder.ToTable("UserTokens");
		builder.Property(_ => _.Name).HasMaxLength(512);
		builder.Property(_ => _.Value).HasMaxLength(512);
		builder.Property(_ => _.LoginProvider).HasMaxLength(512);
	}
}
