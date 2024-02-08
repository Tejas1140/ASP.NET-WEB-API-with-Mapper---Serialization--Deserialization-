using Microsoft.Extensions.DependencyInjection;
using WebApp2.BusinessLayer.Services.Implementation;
using WebApp2.BusinessLayer.Services.Interface;
using WebApp2.DataAccessLayer;
using WebApp2.DataAccessLayer.HttpService;

namespace WebApp2.BusinessLayer;

public static class DependancyInjection
{
    public static IServiceCollection AddBussinessLayer(this IServiceCollection services)
    {
        services.AddDataAccessLayer();
        services.AddScoped<IMemberService, MemberService>();
        services.AddScoped<IAgentService, AgentService>();
        services.AddScoped<IApiClientService, ApiClientService>();
        services.AddScoped<HttpClient>();

        return services;
    }
}
