using BLL.Infrastructure;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Volunteering.Services;
using Serilog;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

//var keyVaultEndpoint = new Uri("https://volunteeringvault.vault.azure.net/");
//builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());

//serilog
builder.Host.UseSerilog((hostingContext, configuration) =>
{
    configuration.WriteTo.File(builder.Environment.WebRootPath + "/Log.txt");
});

// Add services to the container.
var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Volunteering;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";//builder.Configuration.GetValue(typeof(string), "DefaultConnection").ToString();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var identityBuilder = builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>();


BLL.Infrastructure.Configuration.ConfigurationService(builder.Services, connectionString, identityBuilder);//Config Business

//builder.Services.AddTransient<IEmailSender, SenGridEmailSender>();

Volunteering.Infrastructure.Configuration.ConfigurationService(builder.Services);

builder.Services.AddControllersWithViews();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();