using AppLogicLayer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection RegisterSettings(this IServiceCollection services)
        {
            // Inject IHttpContextAccessor to access Http contexts
            services.AddHttpContextAccessor();

            services.AddScoped<SaveInterceptor>();

            services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {
                var interceptor = serviceProvider.GetRequiredService<SaveInterceptor>();
                options.UseNpgsql("Host=localhost;;Port=5432;Database=sport_book_local;Username=postgres;Password=password123;Trust Server Certificate=true").AddInterceptors(interceptor);
            });

            // Inject current user service
            //services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}
