using AllSports.Data;
using AllSports.Helpers;
using AllSports.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<HelperPathProvider>();
string connectionString = builder.Configuration.GetConnectionString("SqlAllSports");
//RESOLVEMOS EL REPOSITORY CON TRANSIENT
builder.Services.AddTransient<RepositoryDeportes>();
builder.Services.AddTransient<RepositoryUsuarios>();

builder.Services.AddDbContext<AllSportsContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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

app.Run();
