//
// Copyright (c) 2024-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot/blob/main/LICENSE
//

using Karamem0.SwitchBot.Controllers.Models;
using Karamem0.SwitchBot.Logging;
using Karamem0.SwitchBot.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Karamem0.SwitchBot.Controllers;

[ApiController()]
[Authorize()]
[Route("api/deleteWebhook")]
public class DeleteWebhookController(ISwitchBotService switchBotService, ILogger<DeleteWebhookController> logger) : Controller
{

    private readonly ISwitchBotService switchBotService = switchBotService;

    private readonly ILogger logger = logger;

    public async Task<IActionResult> PostAsync([FromBody] DeleteWebhookRequest request)
    {
        try
        {
            this.logger.FunctionExecuting();
            this.logger.FunctionRequestData(
                requestData: JsonSerializer
                    .Serialize(request)
                    .Replace("\r", "")
                    .Replace("\n", "")
            );
            await this
                .switchBotService
                .DeleteWebhookAsync(request.Url)
                .ConfigureAwait(false);
            var response = new DeleteWebhookResponse();
            this.logger.FunctionResponseData(
                responseData: JsonSerializer
                    .Serialize(response)
                    .Replace("\r", "")
                    .Replace("\n", "")
            );
            return new OkObjectResult(response);
        }
        catch (InvalidOperationException ex)
        {
            this.logger.FunctionFailed(exception: ex);
            return new StatusCodeResult(StatusCodes.Status400BadRequest);
        }
        catch (Exception ex)
        {
            this.logger.UnhandledErrorOccurred(exception: ex);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
        finally
        {
            this.logger.FunctionExecuted();
        }
    }

}
