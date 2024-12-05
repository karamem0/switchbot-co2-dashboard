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

public class DeleteWebhookControllerTests
{

    [Test()]
    public async Task PostAsync_Success()
    {
        // Setup
        var switchBotService = Substitute.For<ISwitchBotService>();
        _ = switchBotService
            .DeleteWebhookAsync(Arg.Any<Uri>(), default)
            .Returns(Task.CompletedTask);
        var logger = Substitute.For<ILogger<DeleteWebhookController>>();
        // Execute
        var target = new DeleteWebhookController(switchBotService, logger);
        var request = new DeleteWebhookRequest()
        {
            Url = new Uri("https://example.com/Webhook1")
        };
        var actual = await target.PostAsync(request);
        // Asset
        using (Assert.EnterMultipleScope())
        {
            Assert.That(actual, Is.TypeOf<OkObjectResult>());
            var result = actual as OkObjectResult;
            var response = result?.Value as DeleteWebhookResponse;
            Assert.That(response, Is.Not.Null);
        }
    }

}
