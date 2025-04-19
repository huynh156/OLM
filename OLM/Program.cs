using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using OLM.Data;
using OLM.Helper;

var builder = WebApplication.CreateBuilder(args);
// Initialize Firebase Admin SDK
try
{
    FirebaseApp.Create(new AppOptions()
    {
        Credential = GoogleCredential.FromFile("D:\\source\\OLM\\OLM\\olm-project-8b75a-firebase-adminsdk-fbsvc-2c7f058257.json"),
    });
    Console.WriteLine("Firebase Admin SDK initialized successfully.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error initializing Firebase Admin SDK: {ex.Message}");
    // Handle the error appropriately, perhaps log it and prevent further Firebase operations
}

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<OlmContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("OLM"));
});

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/User/Login";  // Đường dẫn đến trang đăng nhập
        options.AccessDeniedPath = "/Home/AccessDenied";  // Trang truy cập bị từ chối
    });


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


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
