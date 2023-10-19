using DreamerStore2.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SonungvienContext>(options =>
                                                 options.UseSqlServer("Data Source=.\\;Initial Catalog=SONUNGVIEN;Integrated Security=True;TrustServerCertificate=true"));
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

app.Run();
