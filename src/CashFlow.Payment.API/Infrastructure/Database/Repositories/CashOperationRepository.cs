using CashFlow.Payment.API.Domain.Entities;
using CashFlow.Payment.API.Infrastructure.Database.Repositories.Base;
using CashFlow.Payment.API.Infrastructure.Database.Repositories.Interfaces;

namespace CashFlow.Payment.API.Infrastructure.Database.Repositories
{
    public class CashOperationRepository : Repository<CashOperationEntity>, ICashOperationRepository
    {
        public CashOperationRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
