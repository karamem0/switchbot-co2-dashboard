//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot/blob/main/LICENSE
//

using Karamem0.SwitchBot.Controllers.Models;
using Karamem0.SwitchBot.Services;
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

public class CreateWebhookControllerTests
{

    [Test()]
    public async Task PostAsync_Success()
    {
        // Setup
        var switchBotService = Substitute.For<ISwitchBotService>();
        _ = switchBotService
            .CreateWebhookAsync(
                Arg.Any<Uri>(),
                Arg.Any<string>(),
                default
            )
            .Returns(Task.CompletedTask);
        var logger = Substitute.For<ILogger<CreateWebhookController>>();
        // Execute
        var target = new CreateWebhookController(switchBotService, logger);
        var request = new CreateWebhookRequest()
        {
            Url = new Uri("https://example.com/Webhook1"),
            DeviceList = "ALL"
        };
        var actual = await target.PostAsync(request);
        // Asset
        using (Assert.EnterMultipleScope())
        {
            Assert.That(actual, Is.TypeOf<OkObjectResult>());
            var result = actual as OkObjectResult;
            var response = result?.Value as CreateWebhookResponse;
            Assert.That(response, Is.Not.Null);
        }
    }

}
