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
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace Karamem0.SwitchBot.Services.Test;

public class GetWebhookServiceTests
{

    [Test()]
    public async Task InvokeAsync_Success()
    {
        // Setup
        var switchBotRepository = Substitute.For<ISwitchBotRepository>();
        _ = switchBotRepository
            .GetWebhookUrlsAsync(default)
            .Returns(
                [
                    new Uri("https://example.com/Webhook1"),
                    new Uri("https://example.com/Webhook2")
                ]
            );
        _ = switchBotRepository
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
        _ = TypeAdapterConfig.GlobalSettings.Scan(typeof(GetWebhookService).Assembly);
        var mapper = new Mapper(TypeAdapterConfig.GlobalSettings);
        // Execute
        var target = new GetWebhookService(switchBotRepository, mapper);
        var request = new GetWebhookRequest();
        var actual = await target.InvokeAsync(request);
        // Asset
        using (Assert.EnterMultipleScope())
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Value, Is.Not.Null);
            Assert.That(actual.Value[0].Url, Is.EqualTo("https://example.com/Webhook1"));
            Assert.That(actual.Value[0].CreateTime, Is.EqualTo(DateTimeOffset.Parse("2020-01-01T00:00:00Z")));
            Assert.That(actual.Value[0].LastUpdateTime, Is.EqualTo(DateTimeOffset.Parse("2020-01-01T00:00:00Z")));
            Assert.That(actual.Value[1].Url, Is.EqualTo("https://example.com/Webhook2"));
            Assert.That(actual.Value[1].CreateTime, Is.EqualTo(DateTimeOffset.Parse("2020-01-01T00:00:00Z")));
            Assert.That(actual.Value[1].LastUpdateTime, Is.EqualTo(DateTimeOffset.Parse("2020-01-01T00:00:00Z")));
        }
    }

}
