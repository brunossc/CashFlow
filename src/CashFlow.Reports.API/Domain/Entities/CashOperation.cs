
using CashFlow.Reports.API.Domain.Entities.Base;

namespace CashFlow.Reports.API.Domain.Entities
{
    public class CashOperation : EntityBase
    {
        public int Type { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
