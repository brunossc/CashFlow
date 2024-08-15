using CashFlow.Financial.API.Domain.Entities;

namespace CashFlow.Financial.API.Domain.Services.Interfaces
{
    public interface ICashOperationService
    {
        Task<CashOperationEntity> AddCredit(CashOperationEntity cashOperation);
        Task<CashOperationEntity> AddDebit(CashOperationEntity cashOperation);
        Task<IEnumerable<CashOperationEntity>> GetAll();
    }
}