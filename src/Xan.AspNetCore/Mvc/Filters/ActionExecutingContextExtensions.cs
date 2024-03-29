﻿using Microsoft.AspNetCore.Mvc.Filters;

namespace Xan.AspNetCore.Mvc.Filters;

public static class ActionExecutingContextExtensions
{
    public static T? FirstOrDefaultArgument<T>(this ActionExecutingContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        foreach (object? parameter in context.ActionArguments.Values)
        {
            if (parameter is T castedParameter)
            {
                return castedParameter;
            }
        }

        return default;
    }
}
