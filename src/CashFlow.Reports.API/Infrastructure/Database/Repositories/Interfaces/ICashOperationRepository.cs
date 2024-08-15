using CashFlow.Reports.API.Domain.DTOs;
using CashFlow.Reports.API.Infrastructure.Database.Repositories.Base.Interfaces;

namespace CashFlow.Reports.API.Infrastructure.Database.Repositories.Interfaces
{
    public interface ICashOperationRepository : IRepository<ConsolidateDay>
    {
    }
}
