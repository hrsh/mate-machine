using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MateMachine.CodeChallenge.SocialApp.Models.Configs;

public class AppUserConfig : IEntityTypeConfiguration<AppUser> {
	public void Configure(EntityTypeBuilder<AppUser> builder) {
		builder.ToTable("Users");
		builder.HasKey(_ => _.Id);
		builder.Property(_ => _.FirstName).HasMaxLength(64);
		builder.Property(_ => _.LastName).HasMaxLength(64);
		builder.Property(_ => _.UserName).HasMaxLength(512);
		builder.Property(_ => _.NormalizedUserName).HasMaxLength(512);
		builder.Property(_ => _.Email).HasMaxLength(512);
		builder.Property(_ => _.NormalizedEmail).HasMaxLength(512);
		builder.Property(_ => _.PhoneNumber).HasMaxLength(16);
	}
}
