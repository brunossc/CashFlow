using CashFlow.Reports.API.Domain.Entities.Base;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace CashFlow.Reports.API.Infrastructure.Database.Repositories.Base.Interfaces
{
    public interface IRepository<TDocument> where TDocument : EntityBase
    {
        IQueryable<TDocument> Get();
        Task<TDocument> GetAsync(string id);
        Task<int> InsertAsync(TDocument document);
        Task<int> UpdateAsync(string id, TDocument sampleEntity);
        Task<int> UpdateByIdAsync(string id, string key, object value);
        Task<int> UpdateChildListByIdAsync(string id, string parentKey, string childKey, object child);
        Task<int> DeleteAsync(string id);
    }
}
