//
// Copyright (c) 2024-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var storageAccountName = builder.AddParameter("AzureStorageAccountName");
var storageAccountResourceGroup = builder.AddParameter("AzureStorageAccountResourceGroup");
var storageAccount = builder
    .AddAzureStorage("storage")
    .AsExisting(storageAccountName, storageAccountResourceGroup);
var deviceDataContainer = storageAccount.AddBlobContainer("device-data", "switchbot-device-data");
var eventDataContainer = storageAccount.AddBlobContainer("event-data", "switchbot-event-data");
var eventDataHistoryContainer = storageAccount.AddBlobContainer("event-data-history", "switchbot-event-data-history");

var microsoftIdentityClientId = builder.AddParameter("MicrosoftIdentityClientId");
var microsoftIdentityClientSecret = builder.AddParameter("MicrosoftIdentityClientSecret");
var microsoftIdentityTenantId = builder.AddParameter("MicrosoftIdentityTenantId");

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
