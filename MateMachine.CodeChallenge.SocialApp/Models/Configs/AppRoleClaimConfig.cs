using MateMachine.CodeChallenge.SocialApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MateMachine.CodeChallenge.SocialApp.Models.Configs;

public class AppRoleClaimConfig : IEntityTypeConfiguration<AppRoleClaim> {
	public void Configure(EntityTypeBuilder<AppRoleClaim> builder) {
		builder.ToTable("RoleClaims");
		builder.Property(_ => _.ClaimType).HasMaxLength(512);
		builder.Property(_ => _.ClaimValue).HasMaxLength(512);
		builder.HasOne(roleClaim => roleClaim.Role)
			.WithMany(role => role.Claims)
			.HasForeignKey(roleClaim => roleClaim.RoleId);
	}
}
