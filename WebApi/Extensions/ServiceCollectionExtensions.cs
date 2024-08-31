using Asp.Versioning;
using FluentValidation;
using Infrastructure.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Metrics;
using Persistence.EntityFramework;
using Services.Mapper;
using Services.Models.Request;
using Services.Repositories.Interfaces;
using Services.Services.Interfaces;
using Services.Validation;
using Services.Validation.Validators;
using WebApi.Mapper;
using WebApi.Middlewares;

namespace WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataContext(this IServiceCollection services, 
        string connectionString)
    {
        services.AddDbContext<DataContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        services.AddScoped<DbContext, DataContext>();
        
        return services;
    }
    
     public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
    
    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddScoped<IValidator<GetLocationModel>, 
            GetLocationValidator>();
        services.AddScoped<IValidator<GetContainersLocationModel>, 
            GetContainersLocationValidator>();
        services.AddScoped<IValidator<GetContainersByOrderIdModel>, 
            GetContainersByOrderIdValidator>();
        services.AddScoped<IValidator<UpdateLocationModel>, 
            UpdateLocationValidator>();
        services.AddScoped<IValidator<UpdateContainersLocationModel>, 
            UpdateContainersLocationValidator>();

        services.AddScoped<ContainerValidator>();
        
        return services;
    }
    
    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddAutoMapper(
            typeof(ServiceMappingProfile),
            typeof(ApiMappingProfile));
        
        return services;
    }
    
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IContainerService, Services.Services.Implementations.ContainerService>();
        
        return services;
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IContainerRepository, ContainerRepository>();
        
        return services;
    }
    
    public static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version"));
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
    
    public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
    {
        services.AddTransient<ExceptionHandlerMiddleware>();
        
        return services;
    }
    
    public static IServiceCollection AddTelemetry(this IServiceCollection services)
    {
        services.AddOpenTelemetry()
            .WithMetrics(builder =>
            {
                builder.AddPrometheusExporter();

                builder.AddMeter("Microsoft.AspNetCore.Hosting",
                    "Microsoft.AspNetCore.Server.Kestrel");
                builder.AddView("http.server.request.duration",
                    new ExplicitBucketHistogramConfiguration
                    {
                        Boundaries = [ 0, 0.005, 0.01, 0.025, 0.05,
                            0.075, 0.1, 0.25, 0.5, 0.75, 1, 2.5, 5, 7.5, 10 ]
                    });
            });
        
        return services;
    }
}