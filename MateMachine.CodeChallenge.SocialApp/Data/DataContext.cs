using MateMachine.CodeChallenge.SocialApp.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MateMachine.CodeChallenge.SocialApp.Data;

public class DataContext : IdentityDbContext<
	AppUser,
	AppRole,
	long,
	AppUserClaim,
	AppUserRole,
	AppUserLogin,
	AppRoleClaim,
	AppUserToken> {
	public DataContext(DbContextOptions<DataContext> options) : base(options) {
	}

	protected override void OnModelCreating(ModelBuilder builder) {
		base.OnModelCreating(builder);

		builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
	}

	public DbSet<Friend> Friends { get; set; }
}
