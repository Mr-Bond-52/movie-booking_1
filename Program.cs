using System.Globalization;
using Microsoft.EntityFrameworkCore;
using MovieBooking.Api;
using MovieBooking.Data;
using MovieBooking.Services;

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseSqlite(builder.Configuration.GetConnectionString("Default") ?? "Data Source=cinetix.db"));
builder.Services.AddScoped<DataStore>();      // service layer shared by the pages and the JSON API

var app = builder.Build();

// Create the SQLite database on first run and add sample data.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    SeedData.Initialize(db);
}

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.MapApi();
app.Run();
