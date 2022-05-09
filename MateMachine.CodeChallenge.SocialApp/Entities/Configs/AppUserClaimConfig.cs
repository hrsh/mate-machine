using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MateMachine.CodeChallenge.SocialApp.Entities.Configs;

public class AppUserClaimConfig : IEntityTypeConfiguration<AppUserClaim> {
	public void Configure(EntityTypeBuilder<AppUserClaim> builder) {
		builder.ToTable("UserClaims");
		builder.Property(_ => _.ClaimValue).HasMaxLength(512);
		builder.Property(_ => _.ClaimType).HasMaxLength(512);
		builder.HasOne(userClaim => userClaim.User)
			.WithMany(user => user.Claims)
			.HasForeignKey(userClaim => userClaim.UserId);
	}
}
