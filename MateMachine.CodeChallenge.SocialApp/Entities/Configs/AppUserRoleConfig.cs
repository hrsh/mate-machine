using MateMachine.CodeChallenge.SocialApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MateMachine.CodeChallenge.SocialApp.Entities.Configs;

public class AppUserRoleConfig : IEntityTypeConfiguration<AppUserRole> {
	public void Configure(EntityTypeBuilder<AppUserRole> builder) {
		builder.ToTable("UserRoles");
		builder.HasOne(userRole => userRole.Role)
		   .WithMany(role => role.Users)
		   .HasForeignKey(userRole => userRole.RoleId);

		builder.HasOne(userRole => userRole.User)
			.WithMany(user => user.Roles)
			.HasForeignKey(userRole => userRole.UserId);
	}
}
