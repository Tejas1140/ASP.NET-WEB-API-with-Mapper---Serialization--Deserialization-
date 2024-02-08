using Microsoft.Extensions.DependencyInjection;
using WebApp2.DataAccessLayer.Implementation;
using WebApp2.DataAccessLayer.Interface;
using Microsoft.EntityFrameworkCore;
namespace WebApp2.DataAccessLayer
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
        {
            services.AddDbContext<NpgSqlDBContext>();
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("Agent"));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
