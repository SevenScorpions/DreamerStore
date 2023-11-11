using DreamerStore2.Models;
using DreamerStore2.Service.ImageUploading;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SonungvienContext>(options =>
                                                 options.UseSqlServer(configuration.GetConnectionString("MyConnectionString")));
builder.Services.AddScoped<ImageUploadingService>();
builder.Services.AddScoped<GoogleUploadingService>(provider => new GoogleUploadingService("Service/GoogleUploading/cre.json"));
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

app.MapControllerRoute(
    name: "DetailedProducts",
    pattern: "DetailedProducts/Index/{id?}");
app.Run();
