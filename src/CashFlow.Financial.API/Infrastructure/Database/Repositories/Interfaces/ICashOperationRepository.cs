using CashFlow.Financial.API.Domain.Entities;
using CashFlow.Financial.API.Infrastructure.Database.Repositories.Base.Interfaces;

namespace CashFlow.Financial.API.Infrastructure.Database.Repositories.Interfaces
{
    public interface ICashOperationRepository : IRepository<CashOperationEntity>
    {
    }
}
