using CashFlow.Financial.API.Application.CashOperation.Commands.Responses.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CashFlow.Financial.API.Application.CashOperation.Commands.Requests
{
    public class DebitCashOperationCommandsRequest : IRequest<CashOperationCommandResponse>
    {
        [Range(0.01, 1000000000, ErrorMessage = "Valor deve ser maior que 0 e menor que 1.000.000.000,00(Hum bilhão)")]
        public decimal Value { get; set; }
        [StringLength(200, ErrorMessage = "A descrição só pode ter até 200 caracteres")]
        public string Description { get; set; }
    }
}
