using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBCDM.Service.GenericRepository
{
	public interface IUnitOfWork : IDisposable
	{
		public string DatabaseName { get; set; }
		IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
		bool IsDbContextNull();
	}
}
