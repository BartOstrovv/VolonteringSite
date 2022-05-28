using BLL.Services;
using DLL.Repository;
using Microsoft.AspNetCore.Identity;

namespace Volunteering.Infrastructure
{
    public static class Configuration
    {
        public static void ConfigurationService(IServiceCollection services)
        {
            services.AddTransient<AdvertisementRepository>();
            services.AddTransient<CommentRepository>();
            services.AddTransient<DonationRepository>();
            services.AddTransient<PersonDataRepository>();
            services.AddTransient<PhotoRepository>();
            services.AddTransient<UserRepository>();

            services.AddTransient<AdvertisementService>();
            services.AddTransient<CommentService>();
            services.AddTransient<DonationService>();
            services.AddTransient<PersonDataService>();
            services.AddTransient<UserService>();
        }
    }
}
