using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MateMachine.CodeChallenge.SocialApp.Entities.Configs;

public class FriendConfig : IEntityTypeConfiguration<Friend> {
	public void Configure(EntityTypeBuilder<Friend> builder) {
		builder.HasKey(x => x.Id);

		builder.HasOne<AppUser>().WithMany(_ => _.Friends).HasForeignKey(_ => _.UserId).OnDelete(DeleteBehavior.Cascade);
	}
}
