using GamingVIVE.Data.Entities;
using GamingVIVE.Services.GameService;
using GamingVIVE.Services.GamesServices;
using GamingVIVE.Services.GamingSystemAssignmentService;
using GamingVIVE.Services.GamingSystemService;
using GamingVIVE.Services.GenreAssignmentService;
using GamingVIVE.Services.GenreService;
using GamingVIVE.Services.MontizedCategoryAssignmentService;
using GamingVIVE.Services.MontizedCategoryService;
using GamingVIVE_MVC.Data;
using GamingVIVE_MVC.Data.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IGamingSystemService, GamingSystemService>();
builder.Services.AddScoped<IGamingSystemGameAssignment, GamingSystemGameAssignment>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IMontizedCategoryService, MontizedCategoryService>();
builder.Services.AddScoped<IMontizedCategoryGameAssignment, MontizedCategoryGameAssignment>();
builder.Services.AddScoped<IGenreGameAssignment, GenreGameAssignment>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
