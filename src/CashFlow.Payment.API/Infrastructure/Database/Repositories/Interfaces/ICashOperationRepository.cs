using CashFlow.Payment.API.Domain.Entities;
using CashFlow.Payment.API.Infrastructure.Database.Repositories.Base.Interfaces;

namespace CashFlow.Payment.API.Infrastructure.Database.Repositories.Interfaces
{
    public interface ICashOperationRepository : IRepository<CashOperationEntity>
    {
    }
}
