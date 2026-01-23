#nullable enable

using CQRS.Application.ApplicationInsights;
using CQRS.Application.Exceptions;
using CQRS.Common.Constants;
using CQRS.Domain.Models.ApplicationInsights;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Api.ExceptionHandlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly IInsertApplicationInsightsService _insertApplicationInsightsService;

        private readonly ILogger<GlobalExceptionHandler> _logger;

        private readonly IWebHostEnvironment _environment;
        public GlobalExceptionHandler(
                ILogger<GlobalExceptionHandler> logger,
                IWebHostEnvironment environment,
                IInsertApplicationInsightsService insertApplicationInsightsService)
        {
            _logger = logger;
            _environment = environment;
            _insertApplicationInsightsService = insertApplicationInsightsService;
        }
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception,
                "Exception occurred: {Message} | RequestId: {RequestId} | Path: {Path} | Method: {Method}",
                exception.Message,
                httpContext.TraceIdentifier,
                httpContext.Request.Path.Value,
                httpContext.Request.Method);

            ExceptionDetail exceptionDetail = MapException(exception);
            var apiResponse = CreateApiResponse(httpContext, exception, exceptionDetail);
            httpContext.Response.StatusCode = exceptionDetail.Status;


            await httpContext.Response.WriteAsJsonAsync(
                apiResponse,
                cancellationToken
            );



            var metric = new InsertApplicationInsightsModel(
                ApplicationInsightsConstants.METRIC_TYPE_ERROR,
                exception.Message, exception.Message.ToString()

                );

            _insertApplicationInsightsService.Execute(metric);


            return true;
            
        }
  
        private static ExceptionDetail MapException(Exception exception)
        {
            return exception switch
            {
                ValidationException validationException => new ExceptionDetail(
                    StatusCodes.Status400BadRequest,
                    "ValidationFailure",
                    "Validacion de Error",
                    "Han ocurrido errores de validacion",
                    validationException.Errors
                ),
                _ => new ExceptionDetail(
                    StatusCodes.Status500InternalServerError,
                    "ServerError",
                    "Error de servidor",
                    "Ocurrio un error inesperado en la aplicacion",
                    null
                )
            };
        }

        internal record ExceptionDetail
        (
            int Status,
            string Type,
            string Title,
            string Detail,
            IEnumerable<object>? Errors
        );


        internal record ApiResponse
        (
            object? Value,
            bool IsSuccess,
            bool IsFailure,
            string Detail,
            IEnumerable<object>? Errors,
            ApiObjectError error,
            ProblemDetails? ProblemDetails 
        );
        public record ApiObjectError(string code, string message);


        private ApiResponse CreateApiResponse(HttpContext httpContext,Exception exception,ExceptionDetail exceptionDetail)
        {
            object? problemDetailsApi = null;
            var apiError = new ApiObjectError(code: exceptionDetail.Type, message: exceptionDetail.Title);

            if (_environment.IsDevelopment() && exception is not ValidationException)
            {

                var problemDetails = new ProblemDetails
                {
                    Status = exceptionDetail.Status,
                    Type = exceptionDetail.Type,
                    Title = exceptionDetail.Title,
                    Detail = _environment.IsDevelopment() ? exception.Message : exceptionDetail.Detail,
                    Instance = httpContext.Request.Path
                };

                problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;
                problemDetails.Extensions["timestamp"] = DateTime.UtcNow;
                problemDetails.Extensions["exceptionType"] = exception.GetType().Name;
                problemDetails.Extensions["stackTrace"] = exception.StackTrace;
                problemDetails.Extensions["machine"] = Environment.MachineName;
 
                if (httpContext.Request.Headers.TryGetValue("X-Correlation-ID", out var correlationId))
                {
                    problemDetails.Extensions["correlationId"] = correlationId.ToString();
                }

                problemDetailsApi = problemDetails;
            }

            return new ApiResponse(
                Value: null,
                IsSuccess: false,
                IsFailure: true,
                Detail: exceptionDetail.Detail, 
                Errors: exceptionDetail.Errors,
                error: apiError,
                ProblemDetails: problemDetailsApi as ProblemDetails
            );
        }

    }
}
