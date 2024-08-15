namespace CashFlow.Financial.API.Application.CashOperation.Commands.Responses.Common
{
    public class CashOperationCommandResponse
    {
        public int Type { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
    }
}
