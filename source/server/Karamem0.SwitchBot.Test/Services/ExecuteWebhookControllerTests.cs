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

public class ExecuteWebhookServiceTests
{

    [Test()]
    public async Task InvokeAsync_Success()
    {
        // Setup
        var blobRepository = Substitute.For<IBlobsRepository>();
        _ = blobRepository
            .CreateEventDataAsync(Arg.Any<EventData>(), default)
            .Returns(Task.CompletedTask);
        _ = TypeAdapterConfig.GlobalSettings.Scan(typeof(ExecuteWebhookService).Assembly);
        var mapper = new Mapper(TypeAdapterConfig.GlobalSettings);
        // Execute
        var target = new ExecuteWebhookService(blobRepository, mapper);
        var request = new ExecuteWebhookRequest()
        {
            EventType = "changeReport",
            EventVersion = "1",
            Context = new ExecuteWebhookRequestContext()
            {
                DeviceType = "MeterPro(CO2)",
                DeviceMac = "12345678",
                Temperature = 20.0f,
                Scale = "CELSIUS",
                Humidity = 50.0f,
                CO2 = 400,
                Battery = 100,
                TimeOfSample = 946684800000
            }
        };
        var actual = await target.InvokeAsync(request);
        // Asset
        using (Assert.EnterMultipleScope())
        {
            Assert.That(actual, Is.Not.Null);
        }
    }

}
