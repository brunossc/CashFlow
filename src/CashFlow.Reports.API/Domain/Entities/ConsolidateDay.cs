using CashFlow.Reports.API.Domain.Entities.Base;

namespace CashFlow.Reports.API.Domain.Entities
{
    public class ConsolidateDay : EntityBase
    {
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
    }
}
