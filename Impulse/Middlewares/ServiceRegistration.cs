using Impulse.Interfaces;

namespace Impulse.Middlewares
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IAddVacancyService, AddVacancyService>();
            services.AddTransient<ICvUploadService, CvUploadService>();
        }
    }
}
