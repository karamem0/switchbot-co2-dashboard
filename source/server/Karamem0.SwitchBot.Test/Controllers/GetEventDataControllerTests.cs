//
// Copyright (c) 2024-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot/blob/main/LICENSE
//

using Karamem0.SwitchBot.Controllers.Models;
using Karamem0.SwitchBot.Services;
using Karamem0.SwitchBot.Services.Models;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.SwitchBot.Controllers.Test;

public class GetEventDataControllerTests
{

    [Test()]
    public async Task PostAsync_Success()
    {
        // Setup
        var blobsService = Substitute.For<IBlobsService>();
        _ = blobsService
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
        _ = blobsService
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
        var logger = Substitute.For<ILogger<GetEventDataController>>();
        // Execute
        var target = new GetEventDataController(
            blobsService,
            mapper,
            logger
        );
        var request = new GetEventDataRequest();
        var actual = await target.PostAsync(request);
        // Asset
        using (Assert.EnterMultipleScope())
        {
            Assert.That(actual, Is.TypeOf<OkObjectResult>());
        }
    }

}
