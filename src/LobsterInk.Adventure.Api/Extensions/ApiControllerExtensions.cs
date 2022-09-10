namespace LobsterInk.Adventure.Api.Extensions;

public static class ApiControllerExtensions
{
    public static IServiceCollection AddApiControllers(this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddControllersAsServices();
        
        services.AddEndpointsApiExplorer();
        
        return services;
    }
}