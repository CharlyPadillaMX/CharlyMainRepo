using FluentValidation;
using FreeBilling.Data.Entities;
using FreeBilling.Web.Apis;
using FreeBilling.Web.Data;
using FreeBilling.Web.Services;
using FreeBilling.Web.Validators;
using Mapster;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FreeBilling.Web.Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BillingDB") ?? throw new InvalidOperationException("Connection string 'BillingDB' not found.");

IConfigurationBuilder configurationBuilder = builder.Configuration;
configurationBuilder.Sources.Clear();
configurationBuilder.AddJsonFile("appsettings.json")
	.AddJsonFile("appsettings.development.json", true)
	.AddUserSecrets(Assembly.GetExecutingAssembly())
	.AddEnvironmentVariables()
	.AddCommandLine(args);

builder.Services.AddDbContext<BillingContext>();

builder.Services.AddIdentityApiEndpoints<TimeBillUser>(options =>
{
	options.SignIn.RequireConfirmedAccount = false;
	options.Password.RequiredLength = 8;
})
	.AddEntityFrameworkStores<BillingContext>();

builder.Services.AddAuthentication()
	.AddBearerToken();

builder.Services.AddAuthorizationBuilder()
	.AddPolicy("api", cfg =>
	{
		cfg.RequireAuthenticatedUser();
		cfg.AddAuthenticationSchemes(IdentityConstants.BearerScheme);
	});

builder.Services.AddScoped<IBillingRepository, BillingRepository>();

builder.Services.AddRazorPages();
builder.Services.AddTransient<IEmailService, DevTimeEmailService>();

builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<TimeBillModelValidator>();

TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetEntryAssembly()!);

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}

//Allows us to serve index.html as the default webpage
app.UseDefaultFiles();

//Allows us to serve files from wwwroot
app.UseStaticFiles();

//Add Routing
app.UseRouting();
app.UseAuthentication();

//Add Auth middleware
app.UseAuthorization();

app.MapRazorPages();

//app.Run(async ctx =>
//{
//    await ctx.Response.WriteAsync("<html><body><h1>Welcome to FreeVilling</h1></body></html>");
//});

TimeBillsApi.Register(app);

app.MapControllers();

app.MapGroup("api/auth")
	.MapIdentityApi<TimeBillUser>();

app.Run();
