﻿using HBCDM.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBCDM.Entities.Dto
{
	public class JobMasterListDto
	{
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<BuilderJobMaster> Data { get; set; }
    }
	
}
