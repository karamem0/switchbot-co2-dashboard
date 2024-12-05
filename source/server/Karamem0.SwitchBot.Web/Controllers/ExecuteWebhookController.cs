//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

using Karamem0.SwitchBot.Logging;
using Karamem0.SwitchBot.Models;
using Karamem0.SwitchBot.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Karamem0.SwitchBot.Controllers;

public class ExecuteWebhookController
{

    public static async Task<IResult> PostAsync(
        [FromServices()] IExecuteWebhookService service,
        [FromServices()] ILogger<ExecuteWebhookController> logger,
        [FromBody()] ExecuteWebhookRequest request
    )
    {
        try
        {
            logger.MethodExecuting();
            logger.MethodRequestData(data: request);
            var response = await service
                .InvokeAsync(request)
                .ConfigureAwait(false);
            logger.MethodResponseData(data: response);
            return Results.Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            logger.MethodFailed(exception: ex);
            return Results.BadRequest(ex);
        }
        catch (Exception ex)
        {
            logger.UnhandledErrorOccurred(exception: ex);
            return Results.InternalServerError(ex);
        }
        finally
        {
            logger.MethodExecuted();
        }
    }

}
