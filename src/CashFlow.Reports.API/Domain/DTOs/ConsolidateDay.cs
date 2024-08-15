using CashFlow.Reports.API.Domain.DTOs.Base;

namespace CashFlow.Reports.API.Domain.DTOs
{
    public class ConsolidateDay : EntityBase
    {
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
    }
}
