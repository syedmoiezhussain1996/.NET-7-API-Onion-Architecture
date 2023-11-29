
using HBCDM.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBCDM.Services.Interfaces
{
	public interface IJobMasterService
	{
		public Task<JobMasterListDto> GetAllJobMasterAsync(int count, int pageNo = 1);
		//public Task<IEnumerable<JobMasterListDto>> GetAllJobMasterAsync();
	}
}
