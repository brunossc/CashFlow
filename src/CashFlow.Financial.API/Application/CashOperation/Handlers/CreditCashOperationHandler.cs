using CashFlow.Financial.API.Application.CashOperation.Commands.Requests;
using CashFlow.Financial.API.Application.CashOperation.Commands.Responses.Common;
using CashFlow.Financial.API.Domain.Entities;
using CashFlow.Financial.API.Domain.Services.Interfaces;
using MediatR;

namespace CashFlow.Financial.API.Application.CashOperation.Handlers
{
    public class CreditCashOperationHandler : IRequestHandler<CreditCashOperationCommandsRequest, CashOperationCommandResponse>
    {

        private readonly ICashOperationService _cashOperationService;

        public CreditCashOperationHandler(ICashOperationService service)
        {
            _cashOperationService = service;
        }

        public async Task<CashOperationCommandResponse> Handle(CreditCashOperationCommandsRequest request, CancellationToken cancellationToken)
        {
            var entity = new CashOperationEntity()
            {
                Description = request.Description,
                Value = request.Value
            };

            var result = await _cashOperationService.AddCredit(entity);

            var response = new CashOperationCommandResponse()
            {
                Type = result.Type,
                Description = result.Description,
                Value = result.Value
            };

            return response;
        }
    }
}
