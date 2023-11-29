
using HBCD.Service.Handler;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HBCDM.Service.GenericRepository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private ILogHandler _logger;
		private readonly DbContext _context;
		private readonly DbSet<T> _dbset;

		public Repository(DbContext context, ILogHandler logger)
		{
			_logger = logger;
			if (_logger != null)
				_logger.SystemLocation = "HBCDM.Api.Repository";

			_context = context;

			if (_context != null)
				_dbset = context.Set<T>();
		}

		#region Helpers
		public void LoadCollection(IEnumerable<T> data, string collection)
		{
			foreach (var record in data)
			{
				_context.Entry(record).Collection(collection).Load();
			}
		}
		#endregion

		#region Db Gets
		public async Task<T> GetByIdAsync<Tkey>(Tkey id)
		{
			return await _dbset.FindAsync(id);
		}
		public async Task<T> GetAsync(Expression<Func<T, bool>> predict)
		{
			return await _dbset.AsNoTracking().FirstOrDefaultAsync(predict).ConfigureAwait(false);
		}
		public async Task<T> GetAsync(Expression<Func<T, bool>> predict, string[] navigationProperties, bool withTracking)
		{
			var query = _dbset.AsQueryable();
			// query = predict.Aggregate(query, (q) => q.a));
			query = navigationProperties.Aggregate(query, (q, s) => q.Include(s));
			query = query.Where(predict);
			if (withTracking)
			{
				return await query.AsTracking().FirstOrDefaultAsync().ConfigureAwait(false);
			}
			else
			{
				return await query.AsNoTracking().FirstOrDefaultAsync().ConfigureAwait(false);
			}
		}
		public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predict, string[] navigationProperties, bool withTracking)
		{
			var query = _dbset.AsQueryable();

			// query = predict.Aggregate(query, (q) => q.a));

			query = navigationProperties.Aggregate(query, (q, s) => q.Include(s));
			if (predict != null)
				query = query.Where(predict);
			if (withTracking)
			{
				return await query.AsTracking().ToListAsync().ConfigureAwait(false);
			}
			else
			{
				return await query.AsNoTracking().ToListAsync().ConfigureAwait(false);
			}
		}
		public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predict, bool withTracking = false)
		{
			if (withTracking)
				return await _dbset.Where(predict).ToListAsync().ConfigureAwait(false);

			return await _dbset.Where(predict).AsNoTracking().ToListAsync().ConfigureAwait(false);
		}
		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbset.AsNoTracking().ToListAsync().ConfigureAwait(false);
		}
		public IQueryable<T> GetAll(Expression<Func<T, bool>> predict, string[] navigationProperties = null, bool withTracking = false)
		{
			IQueryable<T> query;
			if (withTracking)
			{
				query = _dbset.AsTracking().AsQueryable();
			}
			else
			{
				query = _dbset.AsNoTracking().AsQueryable();
			}
			if (navigationProperties != null)
				query = navigationProperties.Aggregate(query, (q, s) => q.Include(s));

			if (predict != null)
				query = query.Where(predict);

			return query;

		}
		public IQueryable<T> GetAll(Expression<Func<T, bool>> predict, bool withTracking = false)
		{
			IQueryable<T> query;
			if (withTracking)
			{
				query = _dbset.AsTracking().AsQueryable();
			}
			else
			{
				query = _dbset.AsNoTracking().AsQueryable();
			}			

			if (predict != null)
				query = query.Where(predict);

			return query;

		}
		#endregion

		#region Db Operations
		public async Task<bool> CreateRecordAsync(T record, bool commitContext, Guid userId, bool storeInAudit = true)
		{
			if (record == null)
				return false;

			try
			{
				await _dbset.AddAsync(record).ConfigureAwait(false);

				if (commitContext)
				{
					if (storeInAudit)
					{
						// krna ha sukoon sa audit ka kaam
						//var auditMaster = new AuditMasterModel();
						//var entityType =_context.Model.FindEntityType(typeof(T));
						//var schema = entityType.GetSchema();
						//var tableName = entityType.GetTableName();

						//auditMaster.TableName = tableName;
						//auditMaster.Id = Guid.NewGuid();
						//auditMaster.RecordId=re
						var count = await _context.SaveChangesAsync().ConfigureAwait(false);
						return count > 0 ? true : false;
					}
					else
					{
						var count = await _context.SaveChangesAsync().ConfigureAwait(false);
						return count > 0 ? true : false;
					}
				}

				return true;
			}
			catch (Exception ex)
			{
				_logger.WriteException($"An error occurred while creating a record. Record type: {record.GetType()}", ex);
				return false;
			}
		}

		public async Task<bool> UpdateRecordAsync(T record, bool attachRecord, bool commitContext, Guid userId, bool storeInAudit = true)
		{
			if (record == null)
				return false;

			try
			{
				if (attachRecord)
					_context.Attach(record);

				_context.Entry(record).State = EntityState.Modified;

				if (commitContext)
				{
					if (storeInAudit)
					{
						var count = await _context.SaveChangesAsync().ConfigureAwait(false);
						return count > 0 ? true : false;
					}
					else
					{
						var count = await _context.SaveChangesAsync().ConfigureAwait(false);
						return count > 0 ? true : false;
					}
				}

				return true;
			}
			catch (Exception ex)
			{
				_logger.WriteException($"An error occurred while updating a record. Record type: {record.GetType()}", ex);
				return false;
			}
		}

		public async Task<bool> DeleteRecordAsync(T record, bool commitContext, Guid userId, bool storeInAudit = true)
		{
			if (record == null)
				return false;

			try
			{
				_dbset.Remove(record);

				if (commitContext)
				{
					if (storeInAudit)
					{
						var count = await _context.SaveChangesAsync().ConfigureAwait(false);
						return count > 0 ? true : false;
					}
					else
					{
						var count = await _context.SaveChangesAsync().ConfigureAwait(false);
						return count > 0 ? true : false;
					}
				}

				return true;
			}
			catch (Exception ex)
			{
				_logger.WriteException($"An error occurred while deleting a record. Record type: {record.GetType()}", ex);
				return false;
			}
		}
		#endregion

		#region Db Bulk Operations
		public async Task<bool> CreateRecordsAsync(IEnumerable<T> records, bool commitContext, Guid? subsidiaryId, Guid userId, bool storeInAudit = true)
		{
			if (records == null)
				return false;

			try
			{
				await _dbset.AddRangeAsync(records).ConfigureAwait(false);

				if (commitContext)
				{
					if (storeInAudit)
					{
						var count = await _context.SaveChangesAsync().ConfigureAwait(false);
						return count > 0 ? true : false;
					}
					else
					{
						var count = await _context.SaveChangesAsync().ConfigureAwait(false);
						return count > 0 ? true : false;
					}
				}

				return true;
			}
			catch (Exception ex)
			{
				// _logger.WriteException("An error occurred while creating records.", ex);
				return false;
			}
		}

		public async Task<bool> UpdateRecordsAsync(IEnumerable<T> records, bool commitContext, Guid? subsidiaryId, Guid userId, bool storeInAudit = true)
		{
			if (records == null)
				return false;

			if (!records.Any())
				return true;

			try
			{
				foreach (var record in records)
				{
					_context.Entry(record).State = EntityState.Modified;
				}

				if (commitContext)
				{
					if (storeInAudit)
					{
						var count = await _context.SaveChangesAsync().ConfigureAwait(false);
						return count > 0 ? true : false;
					}
					else
					{
						var count = await _context.SaveChangesAsync().ConfigureAwait(false);
						return count > 0 ? true : false;
					}
				}

				return true;
			}
			catch (Exception ex)
			{
				_logger.WriteException("An error occurred while creating records.", ex);
				return false;
			}
		}

		public async Task<bool> DeleteRecordsAsync(IEnumerable<T> records, bool commitContext, Guid? subsidiaryId, Guid userId, bool storeInAudit = true)
		{
			if (records == null)
				return false;

			try
			{
				_dbset.RemoveRange(records);

				if (commitContext)
				{
					if (storeInAudit)
					{
						var count = await _context.SaveChangesAsync().ConfigureAwait(false);
						return count > 0 ? true : false;
					}
					else
					{
						var count = await _context.SaveChangesAsync().ConfigureAwait(false);
						return count > 0 ? true : false;
					}
				}

				return true;
			}
			catch (Exception ex)
			{
				_logger.WriteException("An error occurred while deleting records.", ex);
				return false;
			}
		}
		#endregion

		public async Task<bool> SaveChangesAsync(bool storeInAudit = true)
		{
			if (storeInAudit)
			{
				var count = await _context.SaveChangesAsync().ConfigureAwait(false);
				return count > 0 ? true : false;
			}
			else
			{
				var count = await _context.SaveChangesAsync().ConfigureAwait(false);
				return count > 0 ? true : false;
			}
		}
	}
}

