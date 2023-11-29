using HBCD.Service.Handler;
using HBCDM.Domain.Models;
using HBCDM.Entities.Dto;
using HBCDM.Service.GenericRepository;
using HBCDM.Services.Interfaces;
using Microsoft.SqlServer.Server;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HBCDM.Services.Services
{
	public class JobMasterService : IJobMasterService
	{
		private IUnitOfWork? _unitOfWork;
		private ILogHandler? _logger;
		public JobMasterService(IUnitOfWork unitOfWork, ILogHandler logger)
		{
			_logger = logger;
			if (_logger != null)
				_logger.SystemLocation = "HBCDM.Api.Service.JobMasterService";

			_unitOfWork = unitOfWork;
		}
		public async Task<JobMasterListDto> GetAllJobMasterAsync(int count, int pageNo = 1)
		{
			if (_unitOfWork.IsDbContextNull())
			{
				var exception = new Exception("An error occurred due to DB context is null.");
				_logger?.WriteException(exception);
				throw exception;
			}

			//DateTime start = DateTime.ParseExact(startDate.ToString(), "yyyy-MM-dd", null);
			//DateTime end = DateTime.ParseExact(endDate.ToString(), "yyyy-MM-dd", null);
			var jobMasterRepo = _unitOfWork.GetRepository<BuilderJobMaster>();
			//var records = jobMasterRepo.GetAll(x=>x.StartDate == DateTime.ParseExact(startDate.ToString(), "yyyy-MM-dd", null) && x.EndDate == DateTime.ParseExact(endDate.ToString(), "yyyy-MM-dd", null), false).Take(500);
			var records = jobMasterRepo.GetAll(null, false);
			var recordsSize = records.Count();
			var totalPages = recordsSize / count;
			if (totalPages == 0)
				totalPages = 1;
			var data = records.Skip(pageNo * count).Take(count).ToList();
			JobMasterListDto response = new JobMasterListDto
			{
				Data = data,
				PageNo = pageNo,
				PageSize = data.Count(),
				TotalCount = recordsSize,
				TotalPages = totalPages,
			};
			return response;
		}

	}
}
