//
// Copyright (c) 2024-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot/blob/main/LICENSE
//

using Azure.Storage.Blobs;
using Karamem0.SwitchBot.Services.Models;
using Microsoft.Extensions.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Karamem0.SwitchBot.Services;

public interface IBlobsService
{

    Task CreateEventDataAsync(EventData value, CancellationToken cancellationToken = default);

    IAsyncEnumerable<DeviceData> GetDeviceDataAsync(CancellationToken cancellationToken = default);

    Task<EventData?> GetEventDataAsync(string deviceMac, CancellationToken cancellationToken = default);

}

public class BlobsService(BlobServiceClient blobServiceClient) : IBlobsService
{

    private readonly BlobServiceClient blobServiceClient = blobServiceClient;

    public async Task CreateEventDataAsync(EventData value, CancellationToken cancellationToken = default)
    {
        var blobContainerClient = this.blobServiceClient.GetBlobContainerClient("switchbot-event-data");
        _ = await blobContainerClient
            .GetBlobClient($"{value.DeviceMac}.json")
            .UploadAsync(
                BinaryData.FromObjectAsJson(value),
                true,
                cancellationToken
            )
            .ConfigureAwait(false);
        _ = await blobContainerClient
            .GetBlobClient($"{value.DeviceMac}/{value.TimeOfSample:yyyy/MM/dd/HHmmss}.json")
            .UploadAsync(
                BinaryData.FromObjectAsJson(value),
                true,
                cancellationToken
            )
            .ConfigureAwait(false);
    }

    public async IAsyncEnumerable<DeviceData> GetDeviceDataAsync([EnumeratorCancellation()] CancellationToken cancellationToken = default)
    {
        var blobContainerClient = this.blobServiceClient.GetBlobContainerClient("switchbot-device-data");
        var blobs = blobContainerClient.GetBlobsAsync(cancellationToken: cancellationToken);
        await foreach (var blob in blobs)
        {
            var downloadInfo = await blobContainerClient
                .GetBlobClient(blob.Name)
                .DownloadAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            var downloadContent = downloadInfo?.Value?.Content;
            if (downloadContent is not null)
            {
                var deviceData = JsonSerializer.Deserialize<DeviceData>(downloadContent);
                if (deviceData is not null)
                {
                    yield return deviceData;
                }
            }
        }
    }

    public async Task<EventData?> GetEventDataAsync(string deviceMac, CancellationToken cancellationToken = default)
    {
        var blobContainerClient = this.blobServiceClient.GetBlobContainerClient("switchbot-event-data");
        var downloadInfo = await blobContainerClient
            .GetBlobClient($"{deviceMac}.json")
            .DownloadAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        var downloadContent = downloadInfo?.Value?.Content;
        if (downloadContent is null)
        {
            return null;
        }
        return JsonSerializer.Deserialize<EventData>(downloadContent);
    }

}
