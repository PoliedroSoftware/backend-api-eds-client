﻿using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;

namespace Poliedro.Billing.Api.Common.Extensions;

public static class ResultExtension
{
    public static IResult Match<TValue, TError>(
        this Result<TValue, TError> result,
        Func<TValue, IResult> onSuccess)
        where TError : Error
    {
        return result.IsSuccess ? onSuccess(result.Value!) : result.ToErrorResult();
    }

    public static IResult Match<TValue, TError>(
        this Result<TValue, TError> result,
        Func<TValue, IResult> onSuccess,
        Func<object, object> value)
        where TError : Error
    {
        return result.IsSuccess ? onSuccess(result.Value!) : result.ToErrorResult();
    }

    private static IResult ToErrorResult<TValue, TError>(this Result<TValue, TError> result)
        where TError : Error
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException("Result is successful. No error to convert.");
        }

        return CreateErrorResult(result.Error!);
    }

    private static IResult CreateErrorResult(Error error)
    {
          return new ErrorResult(error.Code, error.Description, error.HttpStatusCode);
    }
}
