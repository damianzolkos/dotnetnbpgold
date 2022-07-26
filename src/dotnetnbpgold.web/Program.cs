using dotnetnbpgold.web.Services;
using dotnetnbpgold.nbp.client.Extensions;
using dotnetnbpgold.db;
using Microsoft.EntityFrameworkCore;
using dotnetnbpgold.db.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDotNetNBPGoldClient();
builder.Services.AddTransient<IGoldPriceRepository, GoldPriceRepository>();
builder.Services.AddTransient<IGoldPriceService, GoldPriceService>();

builder.Services.AddDbContext<DBContext>(opt => opt.UseInMemoryDatabase(databaseName: "DotNetNBPGold"));

var app = builder.Build();

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
