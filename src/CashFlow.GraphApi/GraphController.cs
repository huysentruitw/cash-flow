using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Conventions;
using GraphQL.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CashFlow.GraphApi
{
    [ApiController]
    [Route("api/graph")]
    public sealed class GraphController : ControllerBase
    {
        private readonly GraphQLEngine _engine;
        private readonly IDependencyInjector _injector;
        private readonly ILogger<GraphController> _logger;
        private readonly IValidationRule[] _validationRules;

        public GraphController(
            GraphQLEngine engine,
            IDependencyInjector injector,
            ILogger<GraphController> logger)
        {
            _engine = engine;
            _injector = injector;
            _logger = logger;
#if !DEBUG
            _validationRules = new IValidationRule[] { new ValidationRules.DisableIntrospectionRule() };
#else
            _validationRules = null;
#endif
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var requestBody = await ReadRequestBody();

            ExecutionResult result = await _engine
                .NewExecutor()
                .WithValidationRules(_validationRules)
                .WithDependencyInjector(_injector)
                .WithRequest(requestBody)
                .ExecuteAsync();

            var responseBody = await _engine.SerializeResultAsync(result);

            HttpStatusCode statusCode = HttpStatusCode.OK;

            if (result.Errors?.Any() ?? false)
            {
                statusCode = HttpStatusCode.InternalServerError;
                if (result.Errors.Any(x => x.Code == "VALIDATION_ERROR"))
                    statusCode = HttpStatusCode.BadRequest;
                else if (result.Errors.Any(x => x.Code == "UNAUTHORIZED_ACCESS"))
                    statusCode = HttpStatusCode.Forbidden;

                var eventId = $"{Guid.NewGuid():N}";
                foreach (ExecutionError error in result.Errors)
                {
                    error.Data.Add("__eventid", eventId);
                    Exception exception = error.InnerException?.InnerException
                        ?? error.InnerException
                        ?? error;

                    _logger.LogError(exception, "[{eventId}] Graph API execution failed. Body = {body}", eventId, requestBody);
                }
            }
            else if (result.Perf?.Any() ?? false)
            {
                _logger.LogInformation($"Graph API with '{result.Perf[0].Category} {result.Perf[0].Subject}' finished in {result.Perf[0].Duration} ms");
            }
            else if (result.Operation != null)
            {
                _logger.LogInformation($"Graph API with '{result.Operation.Name}'");
            }

            return new ContentResult
            {
                Content = responseBody,
                ContentType = "application/json; charset=utf-8",
                StatusCode = (int)statusCode
            };
        }

        private Task<string> ReadRequestBody()
        {
            using (StreamReader reader = new StreamReader(Request.Body))
                return reader.ReadToEndAsync();
        }

#if DEBUG
        [HttpGet]
        [Route("schema")]
        public IActionResult Schema()
        {
            return Ok(_engine.Describe());
        }
#endif
    }
}
