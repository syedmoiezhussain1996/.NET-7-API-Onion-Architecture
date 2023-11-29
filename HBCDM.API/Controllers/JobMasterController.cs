
using HBCDM.Domain.Models;
using HBCDM.Entities.Dto;
using HBCDM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.ComponentModel.DataAnnotations;

namespace DataServiceAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class JobMasterController : ControllerBase
	{
		private IJobMasterService _jobMasterService;
		public JobMasterController(IJobMasterService jobMasterService)
		{
			_jobMasterService = jobMasterService;
		}

		
		[HttpGet("JobMasterAll")]
		public async Task<ActionResult<JobMasterListDto>> JobMasterAllAsync(
			[Required(ErrorMessage = "The 'count' parameter is required.")] int? count = 5000,
			[Required(ErrorMessage = "The 'pageNo' parameter is required.")] int? pageNo = 1)
		{
			return Ok(await _jobMasterService.GetAllJobMasterAsync(count.Value, pageNo.Value));
		}

		[HttpGet("JobMasterByBuilderId")]
		public ActionResult JobMasterByBuilderId(
			   [Required(ErrorMessage = "The 'builderID' parameter is required.")] string? builderID)
		{
			return Ok("I am working fine");
		}
		
	}
}
