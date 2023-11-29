using Microsoft.AspNetCore.Builder;
using HBCDM.Infrastructure.Middleware;

namespace HBCDM.Infrastructure.Extension
{
	public static class ConfigureContainer
	{
		public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
		{
			app.UseMiddleware<CustomExceptionMiddleware>();
		}

		//public static void UseSwaggerApp(this IApplicationBuilder app)
		//{

		//	app.UseSwagger();
		//	app.UseSwaggerUI(options =>
		//	{
		//		options.SwaggerEndpoint("/swagger/v1/swagger.json", "Onion Architecture API");
		//	});
		//}

		//public static void ConfigureSwagger(this IApplicationBuilder app)
		//{
		//	app.UseAuthentication();
		//}

		//public static void UseHealthCheck(this IApplicationBuilder app)
		//{
		//	app.UseHealthChecks("/healthz", new HealthCheckOptions
		//	{
		//		Predicate = _ => true,
		//		ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
		//		ResultStatusCodes =
		//		{
		//			[HealthStatus.Healthy] = StatusCodes.Status200OK,
		//			[HealthStatus.Degraded] = StatusCodes.Status500InternalServerError,
		//			[HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
		//		},
		//	}).UseHealthChecksUI(setup =>
		//	{
		//		setup.ApiPath = "/healthcheck";
		//		setup.UIPath = "/healthcheck-ui";
		//	});
		//}

		//public static AuthorizationPolicyBuilder UserRequireCustomClaim(this AuthorizationPolicyBuilder builder, string claimType)
		//{
		//	builder.AddRequirements(new CustomUserRequireClaim(claimType));
		//	return builder;
		//}

	}
}
