using HBCD.Service.Handler;
using HBCDM.Service.GenericRepository;
using HBCDM.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RateLimiting;

namespace HBCDM.Infrastructure.Extension{
	
	public static class ConfigureServiceContainer
	{

		
		public static void AddServices(this IServiceCollection services)
		{
			services.Scan(scan =>
			{
				scan.FromAssemblyOf<IJobMasterService>()
					.AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
					.AsImplementedInterfaces()
					.WithScopedLifetime();
			});
			services.AddScoped(typeof(ILogHandler), typeof(LogHandler));
		}
		public static void AddRepository(this IServiceCollection services)
		{
			services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
		}

		public static void AddApiCallRateLimiter(this IServiceCollection services)
		{
			services.AddRateLimiter(_ => _.AddFixedWindowLimiter("fixed Window", options =>
			{
				options.Window = TimeSpan.FromSeconds(10);
				options.PermitLimit = 1;
				options.QueueLimit = 1;
				options.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
			}));
		}

		//public static void AddJWTAuthentication(this IServiceCollection services, IConfiguration Configuration)
		//{
		//	services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		//.AddJwtBearer(options =>
		//{
		//	options.SaveToken = true;
		//	options.RequireHttpsMetadata = false;
		//	options.TokenValidationParameters = new TokenValidationParameters
		//	{
		//		ValidateIssuer = true,
		//		ValidateAudience = true,
		//		ValidateLifetime = true,
		//		ValidateIssuerSigningKey = true,
		//		ClockSkew = TimeSpan.Zero,
		//		ValidIssuer = Configuration["JWT:ValidIssuer"],
		//		ValidAudience = Configuration["JWT:ValidAudience"],
		//		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
		//	};
		//});
		//}
	}
}
