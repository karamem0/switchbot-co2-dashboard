//
// Copyright (c) 2024-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot/blob/main/LICENSE
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Karamem0.SwitchBot.Services.Models;

public record DeviceData
{

    [JsonPropertyName("deviceName")]
    public required string DeviceName { get; set; }

    [JsonPropertyName("deviceMac")]
    public required string DeviceMac { get; set; }

}
