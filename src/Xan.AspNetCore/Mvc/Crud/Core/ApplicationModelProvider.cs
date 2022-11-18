﻿/*
The MIT License (MIT)

Copyright (c) .NET Foundation and Contributors

All rights reserved.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using System.Reflection;

namespace Xan.AspNetCore.Mvc.Crud.Core;

internal sealed class ApplicationModelProvider
{
    public static ControllerModel CreateAndConfigureControllerModel(Type controllerType, string name, bool authorize)
    {
        ArgumentNullException.ThrowIfNull(controllerType);
        ArgumentNullException.ThrowIfNull(name);

        TypeInfo controllerTypeInfo = controllerType.GetTypeInfo();

        var controllerModel = CreateControllerModel(controllerTypeInfo, name, authorize);

        foreach (var methodInfo in controllerType.GetMethods())
        {
            var actionModel = CreateActionModel(controllerTypeInfo, methodInfo);
            if (actionModel == null)
            {
                continue;
            }

            actionModel.Controller = controllerModel;
            controllerModel.Actions.Add(actionModel);

            foreach (var parameterInfo in actionModel.ActionMethod.GetParameters())
            {
                var parameterModel = CreateParameterModel(parameterInfo);
                if (parameterModel != null)
                {
                    parameterModel.Action = actionModel;
                    actionModel.Parameters.Add(parameterModel);
                }
            }
        }

        return controllerModel;
    }


    public static ControllerModel CreateControllerModel(TypeInfo typeInfo, string name, bool authorize)
    {
        ArgumentNullException.ThrowIfNull(typeInfo);
        ArgumentNullException.ThrowIfNull(name);

        // For attribute routes on a controller, we want to support 'overriding' routes on a derived
        // class. So we need to walk up the hierarchy looking for the first class to define routes.
        //
        // Then we want to 'filter' the set of attributes, so that only the effective routes apply.
        var currentTypeInfo = typeInfo;
        var objectTypeInfo = typeof(object).GetTypeInfo();

        IRouteTemplateProvider[] routeAttributes;

        do
        {
            routeAttributes = currentTypeInfo
                .GetCustomAttributes(inherit: false)
                .OfType<IRouteTemplateProvider>()
                .ToArray();

            if (routeAttributes.Length > 0)
            {
                // Found 1 or more route attributes.
                break;
            }

            currentTypeInfo = currentTypeInfo.BaseType!.GetTypeInfo();
        }
        while (currentTypeInfo != objectTypeInfo);

        var attributes = typeInfo.GetCustomAttributes(inherit: true);

        // This is fairly complicated so that we maintain referential equality between items in
        // ControllerModel.Attributes and ControllerModel.Attributes[*].Attribute.
        var filteredAttributes = new List<object>();
        foreach (var attribute in attributes)
        {
            if (attribute is IRouteTemplateProvider)
            {
                // This attribute is a route-attribute, leave it out.
            }
            else
            {
                filteredAttributes.Add(attribute);
            }
        }

        filteredAttributes.AddRange(routeAttributes);

        if (authorize)
        {
            filteredAttributes.Add(new AuthorizeAttribute());
        }

        attributes = filteredAttributes.ToArray();

        var controllerModel = new ControllerModel(typeInfo, attributes);

        AddRange(controllerModel.Selectors, CreateSelectors(attributes));

        controllerModel.ControllerName = name;

        AddRange(controllerModel.Filters, attributes.OfType<IFilterMetadata>());

        foreach (var routeValueProvider in attributes.OfType<IRouteValueProvider>())
        {
            controllerModel.RouteValues.Add(routeValueProvider.RouteKey, routeValueProvider.RouteValue);
        }

        var apiVisibility = attributes.OfType<IApiDescriptionVisibilityProvider>().FirstOrDefault();
        if (apiVisibility != null)
        {
            controllerModel.ApiExplorer.IsVisible = !apiVisibility.IgnoreApi;
        }

        var apiGroupName = attributes.OfType<IApiDescriptionGroupNameProvider>().FirstOrDefault();
        if (apiGroupName != null)
        {
            controllerModel.ApiExplorer.GroupName = apiGroupName.GroupName;
        }

        return controllerModel;
    }

    public static ParameterModel CreateParameterModel(ParameterInfo parameterInfo)
    {
        ArgumentNullException.ThrowIfNull(parameterInfo);

        var attributes = parameterInfo.GetCustomAttributes(inherit: true);

        BindingInfo? bindingInfo;
        //if (_modelMetadataProvider is ModelMetadataProvider modelMetadataProviderBase)
        //{
        //    var modelMetadata = modelMetadataProviderBase.GetMetadataForParameter(parameterInfo);
        //    bindingInfo = BindingInfo.GetBindingInfo(attributes, modelMetadata);
        //}
        //else
        {
            // GetMetadataForParameter should only be used if the user has opted in to the 2.1 behavior.
            bindingInfo = BindingInfo.GetBindingInfo(attributes);
        }

        var parameterModel = new ParameterModel(parameterInfo, attributes)
        {
            ParameterName = parameterInfo.Name!,
            BindingInfo = bindingInfo,
        };

        return parameterModel;
    }

    public static ActionModel? CreateActionModel(TypeInfo typeInfo, MethodInfo methodInfo)
    {
        ArgumentNullException.ThrowIfNull(typeInfo);
        ArgumentNullException.ThrowIfNull(methodInfo);

        if (!IsAction(typeInfo, methodInfo))
        {
            return null;
        }

        var attributes = methodInfo.GetCustomAttributes(inherit: true);

        var actionModel = new ActionModel(methodInfo, attributes);

        var actionName = attributes.OfType<ActionNameAttribute>().FirstOrDefault();
        if (actionName?.Name != null)
        {
            actionModel.ActionName = actionName.Name;
        }
        else
        {
            actionModel.ActionName = CanonicalizeActionName(methodInfo.Name);
        }

        var apiVisibility = attributes.OfType<IApiDescriptionVisibilityProvider>().FirstOrDefault();
        if (apiVisibility != null)
        {
            actionModel.ApiExplorer.IsVisible = !apiVisibility.IgnoreApi;
        }

        var apiGroupName = attributes.OfType<IApiDescriptionGroupNameProvider>().FirstOrDefault();
        if (apiGroupName != null)
        {
            actionModel.ApiExplorer.GroupName = apiGroupName.GroupName;
        }

        foreach (var routeValueProvider in attributes.OfType<IRouteValueProvider>())
        {
            actionModel.RouteValues.Add(routeValueProvider.RouteKey, routeValueProvider.RouteValue);
        }

        // Now we need to determine the action selection info (cross-section of routes and constraints)
        //
        // For attribute routes on a action, we want to support 'overriding' routes on a
        // virtual method, but allow 'overriding'. So we need to walk up the hierarchy looking
        // for the first definition to define routes.
        //
        // Then we want to 'filter' the set of attributes, so that only the effective routes apply.
        var currentMethodInfo = methodInfo;

        IRouteTemplateProvider[] routeAttributes;

        while (true)
        {
            routeAttributes = currentMethodInfo
                .GetCustomAttributes(inherit: false)
                .OfType<IRouteTemplateProvider>()
                .ToArray();

            if (routeAttributes.Length > 0)
            {
                // Found 1 or more route attributes.
                break;
            }

            // GetBaseDefinition returns 'this' when it gets to the bottom of the chain.
            var nextMethodInfo = currentMethodInfo.GetBaseDefinition();
            if (currentMethodInfo == nextMethodInfo)
            {
                break;
            }

            currentMethodInfo = nextMethodInfo;
        }

        // This is fairly complicated so that we maintain referential equality between items in
        // ActionModel.Attributes and ActionModel.Attributes[*].Attribute.
        var applicableAttributes = new List<object>(routeAttributes.Length);
        foreach (var attribute in attributes)
        {
            if (attribute is IRouteTemplateProvider)
            {
                // This attribute is a route-attribute, leave it out.
            }
            else
            {
                applicableAttributes.Add(attribute);
            }
        }

        applicableAttributes.AddRange(routeAttributes);
        AddRange(actionModel.Selectors, CreateSelectors(applicableAttributes));

        return actionModel;
    }


    internal static bool IsAction(TypeInfo typeInfo, MethodInfo methodInfo)
    {
        ArgumentNullException.ThrowIfNull(typeInfo);
        ArgumentNullException.ThrowIfNull(methodInfo);

        // The SpecialName bit is set to flag members that are treated in a special way by some compilers
        // (such as property accessors and operator overloading methods).
        if (methodInfo.IsSpecialName)
        {
            return false;
        }

        if (methodInfo.IsDefined(typeof(NonActionAttribute)))
        {
            return false;
        }

        // Overridden methods from Object class, e.g. Equals(Object), GetHashCode(), etc., are not valid.
        if (methodInfo.GetBaseDefinition().DeclaringType == typeof(object))
        {
            return false;
        }

        // Dispose method implemented from IDisposable is not valid
        if (IsIDisposableMethod(methodInfo))
        {
            return false;
        }

        if (methodInfo.IsStatic)
        {
            return false;
        }

        if (methodInfo.IsAbstract)
        {
            return false;
        }

        if (methodInfo.IsConstructor)
        {
            return false;
        }

        if (methodInfo.IsGenericMethod)
        {
            return false;
        }

        return methodInfo.IsPublic;
    }

    internal static bool IsIDisposableMethod(MethodInfo methodInfo)
    {
        ArgumentNullException.ThrowIfNull(methodInfo);

        // Ideally we do not want Dispose method to be exposed as an action. However there are some scenarios where a user
        // might want to expose a method with name "Dispose" (even though they might not be really disposing resources)
        // Example: A controller deriving from MVC's Controller type might wish to have a method with name Dispose,
        // in which case they can use the "new" keyword to hide the base controller's declaration.

        // Find where the method was originally declared
        var baseMethodInfo = methodInfo.GetBaseDefinition();
        var declaringType = baseMethodInfo.DeclaringType;

        return
            typeof(IDisposable).IsAssignableFrom(declaringType) &&
             declaringType.GetInterfaceMap(typeof(IDisposable)).TargetMethods[0] == baseMethodInfo;
    }

    internal static string CanonicalizeActionName(string actionName)
    {
        ArgumentNullException.ThrowIfNull(actionName);

        const string Suffix = "Async";

        if (actionName.EndsWith(Suffix, StringComparison.Ordinal))
        {
            actionName = actionName.Substring(0, actionName.Length - Suffix.Length);
        }

        return actionName;
    }

    private static void AddRange<T>(IList<T> list, IEnumerable<T> items)
    {
        ArgumentNullException.ThrowIfNull(list);
        ArgumentNullException.ThrowIfNull(items);

        foreach (var item in items)
        {
            list.Add(item);
        }
    }

    private static IList<SelectorModel> CreateSelectors(IList<object> attributes)
    {
        ArgumentNullException.ThrowIfNull(attributes);

        // Route attributes create multiple selector models, we want to split the set of
        // attributes based on these so each selector only has the attributes that affect it.
        //
        // The set of route attributes are split into those that 'define' a route versus those that are
        // 'silent'.
        //
        // We need to define a selector for each attribute that 'defines' a route, and a single selector
        // for all of the ones that don't (if any exist).
        //
        // If the attribute that 'defines' a route is NOT an IActionHttpMethodProvider, then we'll include with
        // it, any IActionHttpMethodProvider that are 'silent' IRouteTemplateProviders. In this case the 'extra'
        // action for silent route providers isn't needed.
        //
        // Ex:
        // [HttpGet]
        // [AcceptVerbs("POST", "PUT")]
        // [HttpPost("Api/Things")]
        // public void DoThing()
        //
        // This will generate 2 selectors:
        // 1. [HttpPost("Api/Things")]
        // 2. [HttpGet], [AcceptVerbs("POST", "PUT")]
        //
        // Another example of this situation is:
        //
        // [Route("api/Products")]
        // [AcceptVerbs("GET", "HEAD")]
        // [HttpPost("api/Products/new")]
        //
        // This will generate 2 selectors:
        // 1. [AcceptVerbs("GET", "HEAD")]
        // 2. [HttpPost]
        //
        // Note that having a route attribute that doesn't define a route template _might_ be an error. We
        // don't have enough context to really know at this point so we just pass it on.
        var routeProviders = new List<IRouteTemplateProvider>();

        var createSelectorForSilentRouteProviders = false;
        foreach (var attribute in attributes)
        {
            if (attribute is IRouteTemplateProvider routeTemplateProvider)
            {
                if (IsSilentRouteAttribute(routeTemplateProvider))
                {
                    createSelectorForSilentRouteProviders = true;
                }
                else
                {
                    routeProviders.Add(routeTemplateProvider);
                }
            }
        }

        foreach (var routeProvider in routeProviders)
        {
            // If we see an attribute like
            // [Route(...)]
            //
            // Then we want to group any attributes like [HttpGet] with it.
            //
            // Basically...
            //
            // [HttpGet]
            // [HttpPost("Products")]
            // public void Foo() { }
            //
            // Is two selectors. And...
            //
            // [HttpGet]
            // [Route("Products")]
            // public void Foo() { }
            //
            // Is one selector.
            if (!(routeProvider is IActionHttpMethodProvider))
            {
                createSelectorForSilentRouteProviders = false;
                break;
            }
        }

        var selectorModels = new List<SelectorModel>();
        if (routeProviders.Count == 0 && !createSelectorForSilentRouteProviders)
        {
            // Simple case, all attributes apply
            selectorModels.Add(CreateSelectorModel(route: null, attributes: attributes));
        }
        else
        {
            // Each of these routeProviders are the ones that actually have routing information on them
            // something like [HttpGet] won't show up here, but [HttpGet("Products")] will.
            foreach (var routeProvider in routeProviders)
            {
                var filteredAttributes = new List<object>();
                foreach (var attribute in attributes)
                {
                    if (ReferenceEquals(attribute, routeProvider))
                    {
                        filteredAttributes.Add(attribute);
                    }
                    else if (InRouteProviders(routeProviders, attribute))
                    {
                        // Exclude other route template providers
                        // Example:
                        // [HttpGet("template")]
                        // [Route("template/{id}")]
                    }
                    else if (
                        routeProvider is IActionHttpMethodProvider &&
                        attribute is IActionHttpMethodProvider)
                    {
                        // Example:
                        // [HttpGet("template")]
                        // [AcceptVerbs("GET", "POST")]
                        //
                        // Exclude other http method providers if this route is an
                        // http method provider.
                    }
                    else
                    {
                        filteredAttributes.Add(attribute);
                    }
                }

                selectorModels.Add(CreateSelectorModel(routeProvider, filteredAttributes));
            }

            if (createSelectorForSilentRouteProviders)
            {
                var filteredAttributes = new List<object>();
                foreach (var attribute in attributes)
                {
                    if (!InRouteProviders(routeProviders, attribute))
                    {
                        filteredAttributes.Add(attribute);
                    }
                }

                selectorModels.Add(CreateSelectorModel(route: null, attributes: filteredAttributes));
            }
        }

        return selectorModels;
    }

    private static bool IsSilentRouteAttribute(IRouteTemplateProvider routeTemplateProvider)
    {
        return
            routeTemplateProvider.Template == null &&
            routeTemplateProvider.Order == null &&
            routeTemplateProvider.Name == null;
    }

    private static bool InRouteProviders(List<IRouteTemplateProvider> routeProviders, object attribute)
    {
        foreach (var rp in routeProviders)
        {
            if (ReferenceEquals(rp, attribute))
            {
                return true;
            }
        }

        return false;
    }

    private static SelectorModel CreateSelectorModel(IRouteTemplateProvider? route, IList<object> attributes)
    {
        var selectorModel = new SelectorModel();
        if (route != null)
        {
            selectorModel.AttributeRouteModel = new AttributeRouteModel(route);
        }

        AddRange(selectorModel.ActionConstraints, attributes.OfType<IActionConstraintMetadata>());
        AddRange(selectorModel.EndpointMetadata, attributes);

        // Simple case, all HTTP method attributes apply
        var httpMethods = attributes
            .OfType<IActionHttpMethodProvider>()
            .SelectMany(a => a.HttpMethods)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();

        if (httpMethods.Length > 0)
        {
            selectorModel.ActionConstraints.Add(new HttpMethodActionConstraint(httpMethods));
            selectorModel.EndpointMetadata.Add(new HttpMethodMetadata(httpMethods));
        }

        return selectorModel;
    }
}
