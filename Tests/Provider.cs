using Microsoft.Extensions.DependencyInjection;
using WebApi.Extensions;

namespace Tests;

public static class Provider
{
    private static readonly ServiceProvider _serviceProvider;

    static Provider()
    {
        var services = new ServiceCollection();

        services.AddValidation();
        services.AddServices();
        services.AddMappers();
        
        _serviceProvider = services.BuildServiceProvider();
    }
    
    public static T Get<T>() => _serviceProvider.GetRequiredService<T>();
}