using Impulse.Data;
using Impulse.Interfaces;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Impulse.Middlewares
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {

            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            IConfiguration cfg = builder.Build();

            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IAddVacancyService, AddVacancyService>();
            services.AddTransient<ICvUploadService, CvUploadService>();
            services.AddSingleton<ITelegramService, TelegramService>();


            services.AddDbContext<ApplicationDbContext>(options =>

            options.UseSqlServer(cfg["Database:Connection"]));

            services.AddHttpClient();


        }
    }
}
