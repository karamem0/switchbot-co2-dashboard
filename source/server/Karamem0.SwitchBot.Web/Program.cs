//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

#pragma warning disable IDE0053

using Azure.Identity;
using Karamem0.SwitchBot;
using Karamem0.SwitchBot.Controllers;
using Karamem0.SwitchBot.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

_ = builder.WebHost.ConfigureKestrel(options =>
    {
        options.AddServerHeader = false;
    }
);

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
_ = app.Use(async (context, next) =>
    {
        var headers = context.Response.Headers;
        headers.ContentSecurityPolicy = "default-src 'self'; " +
                                        "connect-src 'self' *.azure.com *.microsoftonline.com *.visualstudio.com; " +
                                        "frame-ancestors 'self' *.cloud.microsoft *.microsoft365.com *.office.com teams.microsoft.com; " +
                                        "img-src 'self' blob: data:; " +
                                        "script-src 'self'; " +
                                        "style-src 'self' 'unsafe-inline';";
        headers.XContentTypeOptions = "nosniff";
        headers.XFrameOptions = "SAMEORIGIN";
        headers["Permissions-Policy"] = "camera=(), fullscreen=(), geolocation=(), microphone=()";
        headers["Referrer-Policy"] = "same-origin";
        await next();
    }
);

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

#pragma warning restore IDE0053
