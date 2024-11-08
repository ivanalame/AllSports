using AllSports.Data;
using AllSports.Helpers;
using AllSports.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options=> options.EnableEndpointRouting = false).AddSessionStateTempDataProvider();
builder.Services.AddSession();
builder.Services.AddSingleton<HelperPathProvider>();
builder.Services.AddSingleton<HelperMails>();
builder.Services.AddSingleton<HelperUploadFiles>();
string connectionString = builder.Configuration.GetConnectionString("SqlAllSports");
//RESOLVEMOS EL REPOSITORY CON TRANSIENT
builder.Services.AddTransient<RepositoryDeportes>();
builder.Services.AddTransient<RepositoryUsuarios>();

builder.Services.AddDbContext<AllSportsContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddAntiforgery();

builder.Configuration.GetValue<string>("Key1:Key2");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme=CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme=CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme=CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie();

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
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.UseMvc(routes =>
{
    routes.MapRoute(
         name: "default",
    template: "{controller=Deportes}/{action=Index}/{id?}");
});

app.Run();
