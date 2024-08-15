using CashFlow.Payment.API.Application.CashOperation.Commands.Responses.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CashFlow.Payment.API.Application.CashOperation.Commands.Requests
{
    public class DebitCashOperationCommandsRequest : IRequest<CashOperationCommandResponse>
    {
        [Range(0.01, 1000000000, ErrorMessage = "Valor deve ser maior que 0")]
        public decimal Value { get; set; }
        [StringLength(200, ErrorMessage = "A descrição só pode ter até 200 caracteres")]
        public string Description { get; set; }
    }
}
