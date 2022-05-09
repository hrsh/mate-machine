using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MateMachine.CodeChallenge.SocialApp.Entities.Configs;

public class FriendRequestConfig : IEntityTypeConfiguration<FriendRequest> {
	public void Configure(EntityTypeBuilder<FriendRequest> builder) {
		builder.HasOne<AppUser>()
			.WithMany()
			.HasForeignKey(_ => _.FromId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}
