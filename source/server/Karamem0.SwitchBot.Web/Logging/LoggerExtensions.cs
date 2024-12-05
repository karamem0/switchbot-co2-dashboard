//
// Copyright (c) 2024-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Karamem0.SwitchBot.Logging;

public static class LoggerExtensions
{

    private static readonly Action<ILogger, string?, Exception?> methodExecuting = LoggerMessage.Define<string?>(
        LogLevel.Information,
        new EventId(1001),
        "[{MethodName}] メソッドを実行しています。"
    );

    public static void MethodExecuting(
        this ILogger logger,
        [CallerMemberName()] string? methodName = null,
        Exception? exception = null
    )
    {
        methodExecuting.Invoke(
            logger,
            methodName,
            exception
        );
    }

    private static readonly Action<ILogger, string?, Exception?> methodExecuted = LoggerMessage.Define<string?>(
        LogLevel.Information,
        new EventId(1002),
        "[{MethodName}] メソッドを実行しました。"
    );

    public static void MethodExecuted(
        this ILogger logger,
        [CallerMemberName()] string? methodName = null,
        Exception? exception = null
    )
    {
        methodExecuted.Invoke(
            logger,
            methodName,
            exception
        );
    }

    private static readonly Action<ILogger, string?, string?, Exception?> methodRequestData = LoggerMessage.Define<string?, object?>(
        LogLevel.Debug,
        new EventId(2001),
        "[{MethodName}] 要求データ: {RequestData}"
    );

    public static void MethodRequestData(
        this ILogger logger,
        [CallerMemberName()] string? methodName = null,
        object? data = null,
        Exception? exception = null
    )
    {
        methodRequestData.Invoke(
            logger,
            methodName,
            JsonSerializer
                .Serialize(data)
                .Replace("\r", "")
                .Replace("\n", ""),
            exception
        );
    }

    private static readonly Action<ILogger, string?, string?, Exception?> methodResponseData = LoggerMessage.Define<string?, object?>(
        LogLevel.Debug,
        new EventId(2002),
        "[{MethodName}] 応答データ: {ResponseData}"
    );

    public static void MethodResponseData(
        this ILogger logger,
        [CallerMemberName()] string? methodName = null,
        object? data = null,
        Exception? exception = null
    )
    {
        methodResponseData.Invoke(
            logger,
            methodName,
            JsonSerializer
                .Serialize(data)
                .Replace("\r", "")
                .Replace("\n", ""),
            exception
        );
    }

    private static readonly Action<ILogger, string?, Exception?> methodFailed = LoggerMessage.Define<string?>(
        LogLevel.Error,
        new EventId(8001),
        "[{MethodName}] メソッドの実行に失敗しました。"
    );

    public static void MethodFailed(
        this ILogger logger,
        [CallerMemberName()] string? methodName = null,
        Exception? exception = null
    )
    {
        methodFailed.Invoke(
            logger,
            methodName,
            exception
        );
    }

    private static readonly Action<ILogger, string?, Exception?> unhandledErrorOccurred = LoggerMessage.Define<string?>(
        LogLevel.Error,
        new EventId(9001),
        "[{MethodName}] 予期しない問題が発生しました。"
    );

    public static void UnhandledErrorOccurred(
        this ILogger logger,
        [CallerMemberName()] string? methodName = null,
        Exception? exception = null
    )
    {
        unhandledErrorOccurred.Invoke(
            logger,
            methodName,
            exception
        );
    }

}
