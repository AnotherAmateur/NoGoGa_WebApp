using BuissnesLayaer.Implementations;
using BuissnesLayaer.Interfaces;
using BuissnesLayaer;
using DataLayer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoGoGa.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EFDBContext>(options =>
	options.UseSqlServer(connectionString, b => b.MigrationsAssembly("DL")));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//	.AddEntityFrameworkStores<ApplicationDbContext>();
//builder.Services.AddMvc();
//builder.Services.AddMvcCore();
builder.Services.AddControllersWithViews();


builder.Services.AddAuthentication(         // 2) куки
	CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(option =>
	{
		option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
	});

builder.Services.AddAuthorization();

builder.Services.AddTransient<IUserRep, EFUser>();
builder.Services.AddTransient<IGameRep, EFGame>();
builder.Services.AddScoped<DataManager>();

builder.Services.AddRazorPages();

var app = builder.Build();

SeedDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseDefaultFiles();

app.UseRouting();

app.UseAuthentication();
app.UseCookiePolicy();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.MapDefaultControllerRoute();

app.Run();


void SeedDatabase()
{
	using (var scope = app.Services.CreateScope())
	{
		var dbInitializer = scope.ServiceProvider.GetRequiredService<EFDBContext>();

		SampleData.InitData(dbInitializer);
	}
}