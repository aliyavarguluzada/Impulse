using Impulse.Data;
using Impulse.Middlewares;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddLogging();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();


builder.Services.AddOutputCache();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
            policy.WithOrigins().AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    });



builder.Services.AddServices();


builder.Services.AddHttpContextAccessor();

var app = builder.Build();

//app.UseMyLogging();

app.UseOutputCache();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute("Admin",
                      "{area:exists}/{controller=Account}/{action=AdminLogin}");
app.MapControllerRoute("Admin",
                      "{area:exists}/{controller=Account}/{action=AdminRegister}");

app.MapControllerRoute("Admin",
                        "{area:exists}/{controller=Dashboard}/{action=Index}");

app.MapControllerRoute("Company",
                      "{area:exists}/{controller=Account}/{action=Register}");

app.MapControllerRoute("Company",
                     "{area:exists}/{controller=CompanyHome}/{action=AddVacancy}");


app.MapControllerRoute("default",
    "{controller=Home}/{action=Index}");




app.Run();



