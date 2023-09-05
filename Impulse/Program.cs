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
app.MapControllerRoute("default",
    "{controller=Home}/{action=Index}");


app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();


app.Run();
