//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

using Azure.Storage.Blobs;
using Karamem0.SwitchBot.Repositories.Models;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;

namespace Karamem0.SwitchBot.Repositories;

public interface IBlobsRepository
{

    Task CreateEventDataAsync(EventData value, CancellationToken cancellationToken = default);

    Task CreateEventDataHistoryAsync(EventData value, CancellationToken cancellationToken = default);

    IAsyncEnumerable<DeviceData> GetDeviceDataAsync(CancellationToken cancellationToken = default);

    Task<EventData?> GetEventDataAsync(string deviceMac, CancellationToken cancellationToken = default);

}

public class BlobsRepository(BlobServiceClient blobRepositoryClient) : IBlobsRepository
{

    private readonly BlobServiceClient blobRepositoryClient = blobRepositoryClient;

    public async Task CreateEventDataAsync(EventData value, CancellationToken cancellationToken = default)
    {
        var blobContainerClient = this.blobRepositoryClient.GetBlobContainerClient("switchbot-event-data");
        _ = await blobContainerClient
            .GetBlobClient($"{value.DeviceMac}.json")
            .UploadAsync(
                BinaryData.FromObjectAsJson(value),
                true,
                cancellationToken
            )
            .ConfigureAwait(false);
    }

    public async Task CreateEventDataHistoryAsync(EventData value, CancellationToken cancellationToken = default)
    {
        var blobContainerClient = this.blobRepositoryClient.GetBlobContainerClient("switchbot-event-data-history");
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
        var blobContainerClient = this.blobRepositoryClient.GetBlobContainerClient("switchbot-device-data");
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
        var blobContainerClient = this.blobRepositoryClient.GetBlobContainerClient("switchbot-event-data");
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
