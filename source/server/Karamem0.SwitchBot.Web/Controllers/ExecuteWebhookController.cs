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
using Karamem0.SwitchBot.Services.Models;
using Mapster;
using MapsterMapper;
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
[Route("api/executeWebhook")]
public class ExecuteWebhookController(
    IBlobsService blobsService,
    IMapper mapper,
    ILogger<ExecuteWebhookController> logger
) : Controller
{

    private readonly IBlobsService blobsService = blobsService;

    private readonly IMapper mapper = mapper;

    private readonly ILogger logger = logger;

    public async Task<IActionResult> PostAsync([FromBody] ExecuteWebhookRequest request)
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
                .blobsService
                .CreateEventDataAsync(this.mapper.Map<EventData>(request.Context))
                .ConfigureAwait(false);
            await this
                .blobsService
                .CreateEventDataHistoryAsync(this.mapper.Map<EventData>(request.Context))
                .ConfigureAwait(false);
            var response = new ExecuteWebhookResponse();
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

    public class MapperConfiguration : IRegister
    {

        public void Register(TypeAdapterConfig config)
        {
            _ = config
                .NewConfig<ExecuteWebhookRequestContext, EventData>()
                .Map(d => d.TimeOfSample, s => DateTimeOffset.FromUnixTimeMilliseconds(s.TimeOfSample));
        }

    }

}
