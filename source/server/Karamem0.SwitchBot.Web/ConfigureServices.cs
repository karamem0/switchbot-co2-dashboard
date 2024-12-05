//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

using Karamem0.SwitchBot.Clients;
using Karamem0.SwitchBot.Options;
using Karamem0.SwitchBot.Repositories;
using Karamem0.SwitchBot.Services;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;

namespace Karamem0.SwitchBot;

public static class ConfigureServices
{

    public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.Configure<MicrosoftIdentityOptions>(configuration.GetSection("MicrosoftIdentity"));
        _ = services.Configure<AzureStorageBlobsOptions>(configuration.GetSection("AzureStorageBlobs"));
        _ = services.Configure<SwitchBotOptions>(configuration.GetSection("SwitchBot"));
        return services;
    }

    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        _ = TypeAdapterConfig.GlobalSettings.Scan(typeof(ConfigureServices).Assembly);
        _ = services.AddSingleton(TypeAdapterConfig.GlobalSettings);
        _ = services.AddSingleton<IMapper, ServiceMapper>();
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        _ = services.AddSingleton<ICreateWebhookService, CreateWebhookService>();
        _ = services.AddSingleton<IDeleteWebhookService, DeleteWebhookService>();
        _ = services.AddSingleton<IExecuteWebhookService, ExecuteWebhookService>();
        _ = services.AddSingleton<IGetEventDataService, GetEventDataService>();
        _ = services.AddSingleton<IGetWebhookService, GetWebhookService>();
        return services;
    }

    public static IServiceCollection AddRepsitories(this IServiceCollection services)
    {
        _ = services.AddSingleton<SwitchBotClient>();
        _ = services.AddSingleton<IBlobsRepository, BlobsRepository>();
        _ = services.AddSingleton<ISwitchBotRepository, SwitchBotRepository>();
        return services;
    }

}
