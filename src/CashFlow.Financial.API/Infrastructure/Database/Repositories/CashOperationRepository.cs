using CashFlow.Financial.API.Domain.Entities;
using CashFlow.Financial.API.Infrastructure.Database.Repositories.Base;
using CashFlow.Financial.API.Infrastructure.Database.Repositories.Interfaces;

namespace CashFlow.Financial.API.Infrastructure.Database.Repositories
{
    public class CashOperationRepository : Repository<CashOperationEntity>, ICashOperationRepository
    {
        public CashOperationRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
