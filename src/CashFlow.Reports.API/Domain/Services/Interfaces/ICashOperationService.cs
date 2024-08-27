using CashFlow.Reports.API.Domain.Entities;
using MongoDB.Driver.Linq;

namespace CashFlow.Financial.API.Domain.Services.Interfaces
{
    public interface ICashOperationService
    {
        IList<ConsolidateDay> GetConsolidateByDay();
        Task<int> SetOperation(CashOperation operation);
    }
}