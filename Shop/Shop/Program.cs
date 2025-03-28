using Blazored.LocalStorage;
using Blazored.Toast;
using NLog;
using NLog.Web;
using Shop;
using Shop.Application;
using Shop.Components;
using Shop.ExceptionHandlers;
using Shop.Infrastructure;
using Shop.Infrastructure.Persistence;
using Shop.Services;
using Shop.Shared.Settings;
using System.Text.Json.Serialization;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents();

    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructureServices(builder.Configuration);
    builder.Services.AddHttpClients(builder.Configuration);

    builder.Services.AddExceptionHandler<CustomExceptionHandler>();

    // NLog: 
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    // AutoMapper:
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    // MediatR:
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

    // Blazored.Toast:
    builder.Services.AddBlazoredToast();

    // Blazored.LocalStorage:
    builder.Services.AddBlazoredLocalStorage();

    // Swashbuckle.AspNetCore:
    builder.Services.AddSwaggerGen();

    builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));

    builder.Services.AddSingleton<AppStateService>();
    builder.Services.AddScoped<CartService>();


    var app = builder.Build();

    // Seed database:
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        DatabaseInitializer.SeedDatabaseAsync(services).Wait();
    }

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseExceptionHandler(opt => { });
    }
    else
    {
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
    }

    app.UseAntiforgery();

    app.MapStaticAssets();
    app.MapRazorComponents<App>()
        .AddInteractiveServerRenderMode();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    LogManager.Shutdown();
}