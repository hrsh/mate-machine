using MateMachine.CodeChallenge.SocialApp.Data;
using MateMachine.CodeChallenge.SocialApp.Entities;
using MateMachine.CodeChallenge.SocialApp.Mappers;
using MateMachine.CodeChallenge.SocialApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(ConfigLocalDbOptions);

builder.Services.AddIdentity<AppUser, AppRole>()
	.AddEntityFrameworkStores<DataContext>()
	.AddDefaultTokenProviders()
	.AddDefaultUI();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddRouting(_ => _.LowercaseUrls = true);

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddSingleton<DefaultMapper>();
builder.Services.AddScoped<IFriendService, FriendService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
	app.UseMigrationsEndPoint();
}
else {
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

using var scope = app.Services.CreateScope();
await scope.ServiceProvider.GetRequiredService<DataContext>().Database.MigrateAsync();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

await app.RunAsync();

void ConfigLocalDbOptions(IServiceProvider serviceProvider, DbContextOptionsBuilder dbContextOptionsBuilder) {
	dbContextOptionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), npgsqlOptionsAction => {
		npgsqlOptionsAction.MigrationsAssembly(typeof(Program).Assembly.GetName().Name);
	});
}
