using dotnetnbpgold.web.Services;
using dotnetnbpgold.nbp.client.Extensions;
using dotnetnbpgold.db;
using Microsoft.EntityFrameworkCore;
using dotnetnbpgold.db.Repositories;
using Serilog;
using dotnetnbpgold.web.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add logging.
builder.Logging.ClearProviders();
Serilog.ILogger logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
builder.Logging.AddSerilog(logger);
builder.Services.AddSingleton(logger);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDotNetNBPGoldClient();
builder.Services.AddFileService();
builder.Services.AddTransient<IGoldPriceRepository, GoldPriceRepository>();
builder.Services.AddTransient<IGoldPriceService, GoldPriceService>();

// Add database.
builder.Services.AddDbContext<DotNetNbpGoldDbContext>(opt => opt.UseSqlite("Data Source=DataBase.db"));

var app = builder.Build();

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<DotNetNbpGoldDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
