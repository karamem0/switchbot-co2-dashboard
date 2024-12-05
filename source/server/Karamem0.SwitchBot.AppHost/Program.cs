//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var storageAccountName = builder.AddParameterFromConfiguration("storage-name", "AzureStorageAccount:Name");
var storageAccountResourceGroup = builder.AddParameterFromConfiguration("storage-resourcegroup", "AzureStorageAccount:ResourceGroup");
var storageAccount = builder
    .AddAzureStorage("storage")
    .AsExisting(storageAccountName, storageAccountResourceGroup);
var deviceDataContainer = storageAccount.AddBlobContainer("device-data", "switchbot-device-data");
var eventDataContainer = storageAccount.AddBlobContainer("event-data", "switchbot-event-data");
var eventDataHistoryContainer = storageAccount.AddBlobContainer("event-data-history", "switchbot-event-data-history");

var microsoftIdentityClientId = builder.AddParameterFromConfiguration("client-id", "MicrosoftIdentity:ClientId");
var microsoftIdentityClientSecret = builder.AddParameterFromConfiguration(
    "client-secret",
    "MicrosoftIdentity:ClientSecret",
    secret: true
);
var microsoftIdentityTenantId = builder.AddParameterFromConfiguration("tenant-id", "MicrosoftIdentity:TenantId");

_ = builder
    .AddProject<Projects.Karamem0_SwitchBot_Web>("server")
    .WaitFor(deviceDataContainer)
    .WaitFor(eventDataContainer)
    .WaitFor(eventDataHistoryContainer)
    .WithEnvironment("AzureStorageBlobs:Endpoint", storageAccount.Resource.BlobEndpoint)
    .WithEnvironment("MicrosoftIdentity:ClientId", microsoftIdentityClientId)
    .WithEnvironment("MicrosoftIdentity:ClientSecret", microsoftIdentityClientSecret)
    .WithEnvironment("MicrosoftIdentity:TenantId", microsoftIdentityTenantId);

var app = builder.Build();

await app.RunAsync();
