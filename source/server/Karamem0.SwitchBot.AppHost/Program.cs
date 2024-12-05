//
// Copyright (c) 2024-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot/blob/main/LICENSE
//

using Aspire.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

var builder = DistributedApplication.CreateBuilder(args);

var storageAccountName = builder.AddParameter("AzureStorageAccountName");
var storageAccountResourceGroup = builder.AddParameter("AzureStorageAccountResourceGroup");
var storageAccount = builder
    .AddAzureStorage("storage")
    .AsExisting(storageAccountName, storageAccountResourceGroup);
var deviceDataContainer = storageAccount.AddBlobContainer("device-data", "switchbot-device-data");
var eventDataContainer = storageAccount.AddBlobContainer("event-data", "switchbot-event-data");

_ = builder
    .AddProject<Projects.Karamem0_SwitchBot_Web>("server")
    .WithReference(deviceDataContainer)
    .WaitFor(deviceDataContainer)
    .WithReference(eventDataContainer)
    .WaitFor(eventDataContainer);

var app = builder.Build();

await app.RunAsync();
