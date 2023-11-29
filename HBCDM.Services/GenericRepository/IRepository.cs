
using System.Linq.Expressions;

namespace HBCDM.Service.GenericRepository
{
	public interface IRepository<T> where T : class
	{
		public void LoadCollection(IEnumerable<T> data, string collection);
		#region Db Gets
		public Task<T> GetByIdAsync<Tkey>(Tkey id);
		public Task<T> GetAsync(Expression<Func<T, bool>> predict);
		public Task<T> GetAsync(Expression<Func<T, bool>> predict, string[] navigationProperties, bool withTracking);
		public Task<IEnumerable<T>> GetAllAsync();
		public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predict, bool withTracking = false);
		public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predict, string[] navigationProperties, bool withTracking);
		public IQueryable<T> GetAll(Expression<Func<T, bool>> predict , string[] navigationProperties = null, bool withTracking = false);
		public IQueryable<T> GetAll(Expression<Func<T, bool>> predict , bool withTracking = false);

		#endregion
		public Task<bool> CreateRecordsAsync(IEnumerable<T> records, bool commitContext, Guid? subsidiaryId, Guid userId, bool storeInAudit = true);
		public Task<bool> CreateRecordAsync(T record, bool commitContext, Guid userId, bool storeInAudit = true);
		public Task<bool> DeleteRecordAsync(T record, bool commitContext, Guid userId, bool storeInAudit = true);
		public Task<bool> DeleteRecordsAsync(IEnumerable<T> records, bool commitContext, Guid? subsidiaryId, Guid userId, bool storeInAudit = true);
		public Task<bool> UpdateRecordsAsync(IEnumerable<T> records, bool commitContext, Guid? subsidiaryId, Guid userId, bool storeInAudit = true);
		public Task<bool> UpdateRecordAsync(T record, bool attachRecord, bool commitContext, Guid userId, bool storeInAudit = true);
		public Task<bool> SaveChangesAsync(bool storeInAudit = true);
	}
}
