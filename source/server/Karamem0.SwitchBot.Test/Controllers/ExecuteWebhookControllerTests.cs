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

public class ExecuteWebhookControllerTests
{

    [Test()]
    public async Task PostAsync_Success()
    {
        // Setup
        var blobService = Substitute.For<IBlobsService>();
        _ = blobService
            .CreateEventDataAsync(Arg.Any<EventData>(), default)
            .Returns(Task.CompletedTask);
        _ = TypeAdapterConfig.GlobalSettings.Scan(typeof(ExecuteWebhookController).Assembly);
        var mapper = new Mapper(TypeAdapterConfig.GlobalSettings);
        var logger = Substitute.For<ILogger<ExecuteWebhookController>>();
        // Execute
        var target = new ExecuteWebhookController(
            blobService,
            mapper,
            logger
        );
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
        var actual = await target.PostAsync(request);
        // Asset
        using (Assert.EnterMultipleScope())
        {
            Assert.That(actual, Is.TypeOf<OkObjectResult>());
            var result = actual as OkObjectResult;
            var response = result?.Value as ExecuteWebhookResponse;
            Assert.That(response, Is.Not.Null);
        }
    }

}
