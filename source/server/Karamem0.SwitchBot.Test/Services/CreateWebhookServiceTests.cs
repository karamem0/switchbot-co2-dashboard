//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

using Karamem0.SwitchBot.Models;
using Karamem0.SwitchBot.Repositories;
using NSubstitute;
using NUnit.Framework;

namespace Karamem0.SwitchBot.Services.Test;

public class CreateWebhookServiceTests
{

    [Test()]
    public async Task InvokeAsync_Success()
    {
        // Setup
        var switchBotRepository = Substitute.For<ISwitchBotRepository>();
        _ = switchBotRepository
            .CreateWebhookAsync(
                Arg.Any<Uri>(),
                Arg.Any<string>(),
                default
            )
            .Returns(Task.CompletedTask);
        // Execute
        var target = new CreateWebhookService(switchBotRepository);
        var request = new CreateWebhookRequest()
        {
            Url = new Uri("https://example.com/Webhook1"),
            DeviceList = "ALL"
        };
        var actual = await target.InvokeAsync(request);
        // Asset
        using (Assert.EnterMultipleScope())
        {
            Assert.That(actual, Is.Not.Null);
        }
    }

}
