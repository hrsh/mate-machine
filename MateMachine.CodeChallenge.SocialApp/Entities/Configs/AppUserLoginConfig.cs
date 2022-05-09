using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MateMachine.CodeChallenge.SocialApp.Entities.Configs;

public class AppUserLoginConfig : IEntityTypeConfiguration<AppUserLogin> {
	public void Configure(EntityTypeBuilder<AppUserLogin> builder) {
		builder.ToTable("UserLogins");
		builder.Property(_ => _.LoginProvider).HasMaxLength(512);
		builder.Property(_ => _.ProviderKey).HasMaxLength(512);
		builder.Property(_ => _.ProviderDisplayName).HasMaxLength(512);
	}
}
