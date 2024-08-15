using CashFlow.Financial.API.Domain.Entities.Base;

namespace CashFlow.Financial.API.Domain.Entities
{
    public class CashOperationEntity : EntityBase
    {
        public int Type { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
    }
}
