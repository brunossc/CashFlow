using CashFlow.Payment.API.Domain.Entities;

namespace CashFlow.Payment.API.Domain.Services.Interfaces
{
    public interface ICashOperationService
    {
        Task<CashOperationEntity> AddCredit(CashOperationEntity cashOperation);
        Task<CashOperationEntity> AddDebit(CashOperationEntity cashOperation);
        Task<IEnumerable<CashOperationEntity>> GetAll();
    }
}