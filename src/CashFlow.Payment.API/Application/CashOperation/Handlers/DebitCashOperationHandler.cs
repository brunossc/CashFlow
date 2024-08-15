using CashFlow.Payment.API.Application.CashOperation.Commands.Requests;
using CashFlow.Payment.API.Application.CashOperation.Commands.Responses.Common;
using CashFlow.Payment.API.Domain.Entities;
using CashFlow.Payment.API.Domain.Services.Interfaces;
using MediatR;

namespace CashFlow.Payment.API.Application.CashOperation.Handlers
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
