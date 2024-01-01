using MassTransit;
using Serilog;
using System.Net;

namespace SchoolManagementSystem.Core.Api.Extensions;


public static class HostExtensions
{
    public static WebApplicationBuilder CreateWebHostBuilder<T>(string[] args)
        where T : class
    {
        string appName = typeof(T).Assembly.GetName().Name;

        var builder = WebApplication.CreateBuilder(args);
        builder.Host.ConfigureAppConfiguration((host, builder) =>
        {
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json", optional: true);
            builder.AddJsonFile($"appsettings.{host.HostingEnvironment.EnvironmentName}.json", optional: true);
            builder.AddEnvironmentVariables();
            builder.AddCommandLine(args);
        }).ConfigureLogging((host, builder) => builder.UseSerilog(host.Configuration, appName).AddSerilog());

        return builder;
    }

    public static ILoggingBuilder UseSerilog(this ILoggingBuilder builder, IConfiguration configuration, string appName)
    {
        var seqServerUrl = configuration["Serilog:SeqServerUrl"];
        var hostName = Dns.GetHostName();
        var ipAddress = Dns.GetHostEntry(hostName).AddressList[0].ToString();

        Log.Logger = new LoggerConfiguration()
           .Enrich.WithProperty("Context", appName)
           .Enrich.WithProperty("HostName", hostName)
           .Enrich.WithProperty("IP", ipAddress)
           .Enrich.FromLogContext()
           .WriteTo.Console()
           .WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl)
           .ReadFrom.Configuration(configuration)
           .CreateLogger();

        return builder;
    }

    public static IServiceCollection AddBusRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks();
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["EventBusConnection"], "/", h =>
                {
                    h.Username(configuration["username"]);
                    h.Password(configuration["password"]);
                });
            });
        });     
        return services;
    }

    public static IBusControl Configure(IConfiguration configuration, Action<IRabbitMqBatchPublishConfigurator> registrationAction = null)
    {
        var connection = configuration["EventBusConnection"];

        return Bus.Factory.CreateUsingRabbitMq(cfg =>
        {
            cfg.Host(connection, h =>
            {
                h.Username(configuration["username"]);
                h.Password(configuration["password"]);
            });
        });
    }
}
