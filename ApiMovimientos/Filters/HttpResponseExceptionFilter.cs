using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace ApiMovimientos.Filters
{
    public class HttpResponseExceptionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                if (context.Exception is SuccessException successException)
                {
                    context.Result = new ObjectResult(new { Messages = successException.Messages })
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                    };
                    context.ExceptionHandled = true;

                }
                if (context.Exception is UnAuthorizeException unAuthorizeException)
                {
                    context.Result = new ObjectResult(new { Messages = unAuthorizeException.Messages })
                    {
                        StatusCode = (int)HttpStatusCode.Unauthorized,
                    };
                    context.ExceptionHandled = true;
                }

                if (context.Exception is ModelValidationException _modelValidationException)
                {
                    context.Result = new ObjectResult(new ApiResponse<ValidationProblemDetails>()
                    {
                        Response = _modelValidationException.ProblemDetails,
                    })
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                    };
                    context.ExceptionHandled = true;
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //// Obtiene el valor del parámetro "type" de la acción
            //var type = context.ActionArguments["productId"] as string;

            //// Valida el valor del parámetro
            //if (string.IsNullOrEmpty(type))
            //{
            //    // Cancela la ejecución de la acción y devuelve un BadRequestResult
            //    context.Result = new BadRequestResult();
            //}

            // Crea un ModelStateDictionary para guardar los errores
            var modelState = new ModelStateDictionary();

            // Iterar sobre los argumentos de la acción
            //foreach (var argument in context.ActionArguments)
            //{
            //    // Obtener el nombre y el valor del argumento
            //    var name = argument.Key;
            //    var value = argument.Value;

            //    // Verificar si el valor es un array o una colección
            //    if (value is Array || value is IEnumerable)

            //    // Verificar si el nombre coincide con algún parámetro de la ruta
            //    if (context.RouteData.Values.ContainsKey(name))

            //    // Verifica si el valor tiene algún atributo de binding
            //    //var attributes = value.GetType().GetCustomAttributes(typeof(BindingAttribute), true);
            //    //if (attributes.Length > 0)

            //    // Validar el argumento según logica
            //    //if (!EsValido(argument.Value.ti))
            //    //    // Agrega un error al ModelState con el nombre y el mensaje del argumento
            //    //    modelState.AddModelError(argument.Key, "El argumento no es válido");
            //}

            // Verifica si hay errores en el ModelState
            if (!modelState.IsValid)
            {
                // Crea un ValidationProblemDetails con el ModelState y otros datos
                var problemDetails = new ValidationProblemDetails(modelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Title = "Uno o más errores de validación ocurrieron.",
                    Detail = "Por favor revise la propiedad errors para más detalles.",
                    Instance = context.HttpContext.Request.Path
                };

                // Establece el resultado del contexto con un BadRequestObjectResult que contiene el ValidationProblemDetails
                context.Result = new BadRequestObjectResult(new ApiResponse<ValidationProblemDetails> { Response = problemDetails});
            }
        }
    }

    public class ApiResponse<T>
    {
        public IList<string> Messages { get; set; } = new List<string> { "Success" };
        public T Response { get; set; }
    }

    public class SuccessException : Exception
    {
        public int StatusCode { get; set; }
        public IList<String> Messages { get; set; }
    }

    public class UnAuthorizeException : HttpRequestException
    {
        public int Status { get; set; }
        public IList<String> Messages { get; set; } = new List<string> { "UnAuthorized" };
    }

    public class ModelValidationException : Exception
    {
        public ValidationProblemDetails ProblemDetails { get; set; }
        public ModelValidationException(string key, string message)
        {
            var model = new ModelStateDictionary();
            model.AddModelError(key, message);

            this.ProblemDetails = new ValidationProblemDetails(model);
        }
        public ModelValidationException(IDictionary<string, string[]> messages)
        {
            this.ProblemDetails = new ValidationProblemDetails(messages);
        }
    }
}
