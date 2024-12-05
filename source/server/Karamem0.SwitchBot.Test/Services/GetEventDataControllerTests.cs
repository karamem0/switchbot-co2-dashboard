//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

using Karamem0.SwitchBot.Models;
using Karamem0.SwitchBot.Repositories;
using Karamem0.SwitchBot.Repositories.Models;
using Mapster;
using MapsterMapper;
using NSubstitute;
using NUnit.Framework;

namespace Karamem0.SwitchBot.Services.Test;

public class GetEventDataServiceTests
{

    [Test()]
    public async Task InvokeAsync_Success()
    {
        // Setup
        var blobsRepository = Substitute.For<IBlobsRepository>();
        _ = blobsRepository
            .GetDeviceDataAsync(default)
            .Returns(
                new List<DeviceData>()
                {
                    new()
                    {
                        DeviceName = "Living Room",
                        DeviceMac = "12345678"
                    }
                }.ToAsyncEnumerable()
            );
        _ = blobsRepository
            .GetEventDataAsync(Arg.Any<string>(), default)
            .Returns(
                new EventData()
                {
                    DeviceType = "MeterPro(CO2)",
                    DeviceMac = "12345678",
                    Temperature = 20.0f,
                    Scale = "CELSIUS",
                    Humidity = 50.0f,
                    CO2 = 400,
                    Battery = 100,
                    TimeOfSample = DateTimeOffset.Parse("2020-01-01T00:00:00Z")
                }
            );
        var mapper = new Mapper(TypeAdapterConfig.GlobalSettings);
        // Execute
        var target = new GetEventDataService(blobsRepository, mapper);
        var request = new GetEventDataRequest();
        var actual = await target.InvokeAsync(request);
        // Asset
        using (Assert.EnterMultipleScope())
        {
            Assert.That(actual, Is.Not.Null);
        }
    }

}
