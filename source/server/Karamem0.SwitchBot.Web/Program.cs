//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

using Azure.Identity;
using Karamem0.SwitchBot;
using Karamem0.SwitchBot.Controllers;
using Karamem0.SwitchBot.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.AddAzureBlobServiceClient(
    "storage",
    configureSettings: settings =>
    {
        var options = configuration
            .GetSection("AzureStorageBlobs")
            .Get<AzureStorageBlobsOptions>();
        _ = options ?? throw new InvalidOperationException();
        settings.ServiceUri = options.Endpoint;
        settings.Credential = new DefaultAzureCredential();
    }
);

var services = builder.Services;
_ = services.ConfigureOptions(configuration);
_ = services.AddHttpClient();
_ = services.AddMicrosoftIdentityWebApiAuthentication(configuration, "MicrosoftIdentity");
_ = services.AddApplicationInsightsTelemetry();
_ = services.AddMapper();
_ = services.AddServices();
_ = services.AddRepsitories();

var app = builder.Build();
_ = app.UseHttpsRedirection();
_ = app.UseHsts();
_ = app.UseStaticFiles();
_ = app.MapFallbackToFile("/index.html");

var api = app.MapGroup("/api");

_ = api
    .MapPost("/createWebHook", CreateWebhookController.PostAsync)
    .RequireAuthorization()
    .Produces(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status500InternalServerError);

_ = api
    .MapPost("/deleteWebHook", DeleteWebhookController.PostAsync)
    .RequireAuthorization()
    .Produces(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status500InternalServerError);

_ = api
    .MapPost("/executeWebHook", ExecuteWebhookController.PostAsync)
    .Produces(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status500InternalServerError);

_ = api
    .MapPost("/getEventData", GetEventDataController.PostAsync)
    .RequireAuthorization()
    .Produces(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status500InternalServerError);

_ = api
    .MapPost("/getWebhook", GetWebhookController.PostAsync)
    .RequireAuthorization()
    .Produces(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status500InternalServerError);

await app.RunAsync();
