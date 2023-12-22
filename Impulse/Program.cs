using EcommerceApp.MVC.Middlewares;
using Impulse.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Impulse.Middlewares;
using Impulse.Telegram;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddLogging();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();


builder.Services.AddOutputCache();

builder.Services.AddResponseCaching();


builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    });



builder.Services.AddServices();

//builder.Services.AddScoped<TelegramNotifier>();
//builder.Services.AddHttpClient();

builder.Services.AddDbContext<ApplicationDbContext>(options =>

    options.UseSqlServer(builder.Configuration["Database:Connection"]));


builder.Services.AddHttpContextAccessor();

var app = builder.Build();



app.UseMyLogging();

app.UseStaticFiles();


app.UseAuthentication();
app.UseAuthorization();
app.UseOutputCache();

app.MapControllerRoute("Admin",
                      "{area:exists}/{controller=Account}/{action=AdminLogin}");

app.MapControllerRoute("Admin",
                        "{area:exists}/{controller=Dashboard}/{action=Index}");

app.MapControllerRoute("Company",
                      "{area:exists}/{controller=Account}/{action=Register}");

app.MapControllerRoute("Company",
                     "{area:exists}/{controller=CompanyHome}/{action=AddVacancy}");


app.MapControllerRoute("default",
    "{controller=Home}/{action=Index}");




app.Run();



