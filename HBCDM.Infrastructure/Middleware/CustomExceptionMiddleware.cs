
using HBCD.Service.Handler;
using HBCDM.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace HBCDM.Infrastructure.Middleware
{
	public class CustomExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private ILogHandler? _logger;
		public CustomExceptionMiddleware(RequestDelegate next, ILogHandler logger)
		{
			_logger = logger;
			if (_logger != null)
				_logger.SystemLocation = "HBCDM.Infrastructure.Middleware.CustomExceptionMiddleware";
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next.Invoke(context);
			}
			catch (Exception exceptionObj)
			{
				await HandleExceptionAsync(context, exceptionObj);
			}
		}

		private Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			int code;
			string exceptionType;
			string result = "None";

			switch (exception)
			{
				case ValidationException validationException:
					code = (int)HttpStatusCode.BadRequest;
					result = JsonConvert.SerializeObject(validationException.Failures);
					exceptionType = "ValidationException";
					break;
				case BadRequestException badRequestException:
					code = (int)HttpStatusCode.BadRequest;
					result = badRequestException.Message;
					exceptionType = "BadRequestException";
					break;
				case DeleteFailureException deleteFailureException:
					code = (int)HttpStatusCode.BadRequest;
					result = deleteFailureException.Message;
					exceptionType = "DeleteFailureException";
					break;
				case NotFoundException _:
					code = (int)HttpStatusCode.NotFound;
					exceptionType = "NotFoundException";
					break;
				default:
					code = (int)HttpStatusCode.InternalServerError;
					exceptionType = "InternalServerError";
					break;
			}

			_logger?.WriteException($"Exception Type : {exceptionType}, JSON :@{result}@", exception);
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = code;
			return context.Response.WriteAsync(JsonConvert.SerializeObject(new { StatusCode = code, ErrorMessage = exception.Message }));
		}
	}
}
