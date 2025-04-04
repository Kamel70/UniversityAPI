using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace UniversityAPI.Filters
{
    public class ValidateLocationAttribute : ActionFilterAttribute
    {
        private readonly string _parameterName;
        private readonly string[] _allowedValues;

        public ValidateLocationAttribute(string parameterName, params string[] allowedValues)
        {
            _parameterName = parameterName.ToLower();
            _allowedValues = allowedValues.Select(v => v.ToLower()).ToArray();
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var request = context.HttpContext.Request;

            if (request.ContentLength > 0 && request.Body.CanRead)
            {
                request.EnableBuffering(); // Allows multiple reads of the request body

                using (var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true))
                {
                    var bodyText = await reader.ReadToEndAsync();
                    request.Body.Position = 0; // Reset stream position for the controller to read

                    if (!string.IsNullOrEmpty(bodyText))
                    {
                        try
                        {
                            var jsonDoc = JsonDocument.Parse(bodyText);

                            if (jsonDoc.RootElement.TryGetProperty(_parameterName, out JsonElement parameterElement))
                            {
                                string parameterValue = parameterElement.GetString()?.Trim().ToLower();

                                if (!_allowedValues.Contains(parameterValue))
                                {
                                    context.Result = new BadRequestObjectResult($"Invalid {_parameterName}: '{parameterValue}'. Allowed values: {string.Join(", ", _allowedValues)}");
                                    return;
                                }
                            }
                            else
                            {
                                context.Result = new BadRequestObjectResult($"Missing required parameter: '{_parameterName}'.");
                                return;
                            }
                        }
                        catch (JsonException)
                        {
                            context.Result = new BadRequestObjectResult("Invalid JSON format.");
                            return;
                        }
                    }
                }
            }

            await next(); // Proceed to the action method if validation passes
        }
    }
}
