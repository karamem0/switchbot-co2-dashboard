//
// Copyright (c) 2024-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot/blob/main/LICENSE
//

using Azure.Identity;
using Karamem0.SwitchBot;
using Karamem0.SwitchBot.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.AddAzureBlobServiceClient("storage", configureSettings: settings =>
{
    var options = configuration
        .GetSection("AzureStorageBlobs")
        .Get<AzureStorageBlobsOptions>();
    _ = options ?? throw new InvalidOperationException();
    settings.ServiceUri = options.Endpoint;
    settings.Credential = new DefaultAzureCredential(
        new DefaultAzureCredentialOptions()
        {
            ManagedIdentityClientId = options.ClientId
        }
    );
});

var services = builder.Services;
_ = services.ConfigureOptions(configuration);
_ = services.AddControllers();
_ = services.AddHttpClient();
_ = services.AddMicrosoftIdentityWebApiAuthentication(configuration, "MicrosoftIdentity");
_ = services.AddApplicationInsightsTelemetry();
_ = services.AddMapper();
_ = services.AddServices();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    _ = app.UseDeveloperExceptionPage();
    _ = app.UseCors();
}
_ = app.UseHttpsRedirection();
_ = app.UseHsts();
_ = app.UseDefaultFiles();
_ = app.UseStaticFiles();
_ = app.UseRouting();
_ = app.UseAuthentication();
_ = app.UseAuthorization();
_ = app.MapControllers();
_ = app.MapFallbackToFile("/index.html");

await app.RunAsync();
