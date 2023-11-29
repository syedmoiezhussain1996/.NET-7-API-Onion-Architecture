

using HBCD.Service.Handler;
using HBCDM.Domain.Context;

namespace HBCDM.Service.GenericRepository
{
	public class UnitOfWork : IUnitOfWork
	{
		private HBCDMContext _context;
		private Dictionary<Type, object> repositories;
		private ILogHandler _logger;

		public UnitOfWork(
			HBCDMContext context,
			ILogHandler logger
			)
		{
			DatabaseName = string.Empty;
			_context = context;



			_logger = logger;
			if (_logger != null)
				_logger.SystemLocation = "HBCDM.Service.UnitOfWork";
		}
		public string DatabaseName { get; set; }

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(obj: this);
		}

		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_context != null)
				{
					_context.Dispose();
					_context = null;
				}
			}
		}

		public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
		{
			if (repositories == null)
			{
				repositories = new Dictionary<Type, object>();
			}

			var type = typeof(TEntity);
			if (!repositories.ContainsKey(type))
			{
				repositories[type] = new Repository<TEntity>(_context, null);
			}

			return (IRepository<TEntity>)repositories[type];
		}

		public bool IsDbContextNull()
		{
			return _context == null ? true : false;
		}
	}
}
