using MateMachine.CodeChallenge.SocialApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MateMachine.CodeChallenge.SocialApp.Entities.Configs;

public class AppRoleConfig : IEntityTypeConfiguration<AppRole> {
	public void Configure(EntityTypeBuilder<AppRole> builder) {
		builder.ToTable("Roles");
		builder.Property(_ => _.Name).HasMaxLength(128);
		builder.Property(_ => _.NormalizedName).HasMaxLength(512);
	}
}
