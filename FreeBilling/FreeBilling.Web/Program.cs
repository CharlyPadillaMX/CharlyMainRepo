using FreeBilling.Web.Data;
using FreeBilling.Web.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

IConfigurationBuilder configurationBuilder = builder.Configuration;
configurationBuilder.Sources.Clear();
configurationBuilder.AddJsonFile("appsettings.json")
	.AddJsonFile("appsettings.development.json", true)
	.AddUserSecrets(Assembly.GetExecutingAssembly())
	.AddEnvironmentVariables()
	.AddCommandLine(args);

builder.Services.AddDbContext<BillingContext>();
builder.Services.AddScoped<IBillingRepository, BillingRepository>();

builder.Services.AddRazorPages();
builder.Services.AddTransient<IEmailService, DevTimeEmailService>();

builder.Services.AddControllers();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}

//Allows us to serve index.html as the default webpage
app.UseDefaultFiles();

//Allows us to serve files from wwwroot
app.UseStaticFiles();
app.MapRazorPages();

//app.Run(async ctx =>
//{
//    await ctx.Response.WriteAsync("<html><body><h1>Welcome to FreeVilling</h1></body></html>");
//});

app.MapControllers();

app.Run();
