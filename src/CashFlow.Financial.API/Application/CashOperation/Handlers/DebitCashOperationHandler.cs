using CashFlow.Financial.API.Application.CashOperation.Commands.Requests;
using CashFlow.Financial.API.Application.CashOperation.Commands.Responses.Common;
using CashFlow.Financial.API.Domain.Entities;
using CashFlow.Financial.API.Domain.Services.Interfaces;
using MediatR;

namespace CashFlow.Financial.API.Application.CashOperation.Handlers
{
    public class DebitCashOperationHandler : IRequestHandler<DebitCashOperationCommandsRequest, CashOperationCommandResponse>
    {

        private readonly ICashOperationService _cashOperationService;

        public DebitCashOperationHandler(ICashOperationService service)
        {
            _cashOperationService = service;
        }

        public async Task<CashOperationCommandResponse> Handle(DebitCashOperationCommandsRequest request, CancellationToken cancellationToken)
        {
            var entity = new CashOperationEntity()
            {
                Description = request.Description,
                Value = request.Value
            };

            await _cashOperationService.AddDebit(entity);

            var response = new CashOperationCommandResponse()
            {
                Type = entity.Type,
                Description = entity.Description,
                Value = entity.Value
            };

            return response;
        }
    }
}
