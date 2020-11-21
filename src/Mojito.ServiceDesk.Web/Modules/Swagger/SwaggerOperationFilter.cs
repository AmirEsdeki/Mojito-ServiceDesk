using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

//This class is used for adding custom headers or required options to Swagger UI
public class SwaggerOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        //    if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();

        //    var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;

        //if (descriptor != null
        //    && descriptor.ControllerName.StartsWith("Users"))
        //{
        //    operation.Parameters.Add(new OpenApiParameter()
        //    {
        //        Name = "AuthToken",
        //        In = ParameterLocation.Header,
        //        Description = "user's token",
        //        Required(ErrorMessage ="تمام فیلدهای اجباری باید دارای مقدار باشند.") = true
        //    });
        //}

        //if (descriptor != null
        //    && (!(descriptor.ControllerName.StartsWith("Check")
        //    || descriptor.ControllerName.StartsWith("GroupServiceProvider")
        //    || descriptor.ControllerName.StartsWith("MenuAccessServiceProvider")))
        //    )
        //{
        //    operation.Parameters.Add(new OpenApiParameter()
        //    {
        //        Name = "AuthToken",
        //        In = ParameterLocation.Header,
        //        Description = "user's token",
        //        Required(ErrorMessage ="تمام فیلدهای اجباری باید دارای مقدار باشند.") = true
        //    });
        //}

        //if (descriptor != null
        //    && (descriptor.ControllerName.StartsWith("Check")
        //    || descriptor.ControllerName.StartsWith("GroupServiceProvider")
        //    || descriptor.ControllerName.StartsWith("MenuAccessServiceProvider")
        //    ))
        //{
        //    operation.Parameters.Add(new OpenApiParameter()
        //    {
        //        Name = "ServiceSecret",
        //        In = ParameterLocation.Header,
        //        Description = "service token",
        //        Required(ErrorMessage ="تمام فیلدهای اجباری باید دارای مقدار باشند.") = false,
        //        Schema = new OpenApiSchema
        //        {
        //            Type = "String",
        //            Default = new OpenApiString(@";b@U{Hh#3;|[(%22-p?YF%<}2Ru(KHIZ#l,SwOAj*x3HKUnlm[s^7b,8}p:oo+l")
        //        }
        //    });
        //}

    }
}