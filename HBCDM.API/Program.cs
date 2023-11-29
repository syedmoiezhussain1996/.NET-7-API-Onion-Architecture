using HBCDM.Domain.Context;
using HBCDM.Domain.Models;
using HBCDM.Infrastructure.Extension;

using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Batch;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddControllers()
	.AddOData(option =>
	{
		option.Select().Filter().Count().OrderBy().Expand().SetMaxTop(1000).Count();
		//option.AddRouteComponents("api", GetEdmModel());
	});



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices();
builder.Services.AddRepository();
builder.Services.AddApiCallRateLimiter();	


builder.Services.AddDbContext<HBCDMContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
	sqlServerOptionsAction: sqlOption =>
	{
		sqlOption.EnableRetryOnFailure(
			maxRetryCount: 10,
			maxRetryDelay: System.TimeSpan.FromSeconds(5),
			errorNumbersToAdd: null);
	}
	));

var app = builder.Build();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//	app.UseSwagger();
//	app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

//static IEdmModel GetEdmModel()
//{
//	ODataConventionModelBuilder builder = new();
//	builder.EntitySet<BuilderJobMaster>("BuilderJobMaster");
//	return builder.GetEdmModel();
//}
