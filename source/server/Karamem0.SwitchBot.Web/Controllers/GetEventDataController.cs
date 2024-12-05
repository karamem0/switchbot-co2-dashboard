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
[Route("api/getEventData")]
public class GetEventDataController(
    IBlobsService blobsService,
    IMapper mapper,
    ILogger<GetEventDataController> logger
) : Controller
{

    private readonly IBlobsService blobsService = blobsService;

    private readonly IMapper mapper = mapper;

    private readonly ILogger logger = logger;

    public async Task<IActionResult> PostAsync([FromBody] GetEventDataRequest request)
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
            var response = new GetEventDataResponse()
            {
                Value = await Task.WhenAll(
                    await this
                        .blobsService
                        .GetDeviceDataAsync()
                        .ToListAsync()
                        .AsTask()
                        .ContinueWith(task => task
                            .Result
                            .Select(this.mapper.Map<GetEventDataResponseValue>)
                            .Select(async value => this.mapper.Map(
                                    await this
                                        .blobsService
                                        .GetEventDataAsync(value.DeviceMac)
                                        .ConfigureAwait(false),
                                    value
                                )
                            )
                        )
                        .ConfigureAwait(false)
                )
            };
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
            _ = config.NewConfig<DeviceData, GetEventDataResponseValue>();
            _ = config
                .NewConfig<EventData, GetEventDataResponseValue>()
                .Map(d => d.LastUpdateTime, s => s.TimeOfSample);
        }

    }

}
