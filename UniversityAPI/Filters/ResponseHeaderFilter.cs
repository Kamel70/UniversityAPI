using Microsoft.AspNetCore.Mvc.Filters;

namespace UniversityAPI.Filters
{
    public class ResponseHeaderFilter : IResultFilter
    {
        private readonly ILogger<ResponseHeaderFilter> _logger;

        public ResponseHeaderFilter(ILogger<ResponseHeaderFilter> logger)
        {
            _logger = logger;
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers["Custom-Header"] = "anyThing in the header";
            _logger.LogInformation("Added custom response header.");
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            _logger.LogInformation("Response header modification completed.");
        }
    }
    public static class FilterExtensions
    {
        public static IServiceCollection AddResponseHeaderFilter(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<ResponseHeaderFilter>();
            });
            return services;
        }
    }
}
