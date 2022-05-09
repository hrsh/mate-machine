using MateMachine.CodeChallenge.SocialApp.Data;
using MateMachine.CodeChallenge.SocialApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DataContext>(_configLocalDbOptions);

builder.Services.AddIdentity<AppUser, AppRole>()
	.AddDefaultTokenProviders()
	.AddEntityFrameworkStores<DataContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRouting(_ => _.LowercaseUrls = true);

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

await app.RunAsync();

void _configLocalDbOptions(IServiceProvider serviceProvider, DbContextOptionsBuilder dbContextOptionsBuilder) {
	dbContextOptionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), npgsqlOptionsAction => {
		npgsqlOptionsAction.MigrationsAssembly(typeof(Program).Assembly.GetName().Name);
	});
}
