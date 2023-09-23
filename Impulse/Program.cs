using Impulse.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>

    options.UseSqlServer(builder.Configuration["Database:Connection"]));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

var app = builder.Build();



app.MapControllerRoute("Admin",
                      "{area:exists}/{controller=Account}/{action=AdminLogin}");
app.MapControllerRoute("Company",
                      "{area:exists}/{controller=Account}/{action=Register}");

app.MapControllerRoute("Company",
                     "{area:exists}/{controller=CompanyHome}/{action=AddVacancy}");


app.MapControllerRoute("default",
    "{controller=Home}/{action=Index}");


app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();


app.Run();
