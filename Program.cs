﻿using DreamerStore2.Models;
using DreamerStore2.Service.ImageUploading;
using Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var context = new DbContextOptionsBuilder();
context.EnableSensitiveDataLogging();
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    // Thiết lập các tùy chọn sessions (nếu cần)
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian tối đa mà session có thể không hoạt động
    options.Cookie.HttpOnly = true; // Ngăn chặn truy cập từ phía JavaScript
    options.Cookie.IsEssential = true; // Đảm bảo rằng cookie session luôn được gửi
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication("MyCookieAuthenticationScheme")
        .AddCookie("MyCookieAuthenticationScheme", options =>
        {
            options.Cookie.Name = "MyAppCookie";
            options.LoginPath = "/Mama/Login"; // Đường dẫn đến trang đăng nhập
            options.LogoutPath = "/Mama/Logout"; // Đường dẫn đến trang đăng xuất
        });
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
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
