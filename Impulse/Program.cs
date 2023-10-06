using Impulse.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
    });

builder.Services.AddDbContext<ApplicationDbContext>(options =>

    options.UseSqlServer(builder.Configuration["Database:Connection"]));



builder.Services.AddHttpContextAccessor();

var app = builder.Build();


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


app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();


app.Run();



// TODO: interface e bax istifade et ViewComponentlere ayir Controlleri  loglama qalib input validation ele
// MyAuth problemlidi redirect elemir AddVacany islemir evvel request icindekiler null idi (elnen yazilanlardan basqa)
// indi Count = 0 gelir View da Model.WorkForms falan null gelir
