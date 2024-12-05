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

public class GetWebhookControllerTests
{

    [Test()]
    public async Task PostAsync_Success()
    {
        // Setup
        var switchBotService = Substitute.For<ISwitchBotService>();
        _ = switchBotService
            .GetWebhookUrlsAsync(default)
            .Returns(
                [
                    new Uri("https://example.com/Webhook1"),
                    new Uri("https://example.com/Webhook2")
                ]
            );
        _ = switchBotService
            .GetWebhookDetailsAsync(Arg.Any<Uri[]>(), default)
            .Returns(
                [
                    new WebhookDetails()
                    {
                        Url = "https://example.com/Webhook1",
                        CreateTime = 1577836800000,
                        LastUpdateTime = 1577836800000,
                        DeviceList = "ALL",
                        Enable = true
                    },
                    new WebhookDetails()
                    {
                        Url = "https://example.com/Webhook2",
                        CreateTime = 1577836800000,
                        LastUpdateTime = 1577836800000,
                        DeviceList = "ALL",
                        Enable = true
                    }
                ]
            );
        _ = TypeAdapterConfig.GlobalSettings.Scan(typeof(GetWebhookController).Assembly);
        var mapper = new Mapper(TypeAdapterConfig.GlobalSettings);
        var logger = Substitute.For<ILogger<GetWebhookController>>();
        // Execute
        var target = new GetWebhookController(
            switchBotService,
            mapper,
            logger
        );
        var request = new GetWebhookRequest();
        var actual = await target.PostAsync(request);
        // Asset
        using (Assert.EnterMultipleScope())
        {
            Assert.That(actual, Is.TypeOf<OkObjectResult>());
            var result = actual as OkObjectResult;
            var response = result?.Value as GetWebhookResponse;
            Assert.That(response, Is.Not.Null);
            Assert.That(response?.Value, Is.Not.Null);
            Assert.That(response?.Value[0].Url, Is.EqualTo("https://example.com/Webhook1"));
            Assert.That(response?.Value[0].CreateTime, Is.EqualTo(DateTimeOffset.Parse("2020-01-01T00:00:00Z")));
            Assert.That(response?.Value[0].LastUpdateTime, Is.EqualTo(DateTimeOffset.Parse("2020-01-01T00:00:00Z")));
            Assert.That(response?.Value[1].Url, Is.EqualTo("https://example.com/Webhook2"));
            Assert.That(response?.Value[1].CreateTime, Is.EqualTo(DateTimeOffset.Parse("2020-01-01T00:00:00Z")));
            Assert.That(response?.Value[1].LastUpdateTime, Is.EqualTo(DateTimeOffset.Parse("2020-01-01T00:00:00Z")));
        }
    }

}
