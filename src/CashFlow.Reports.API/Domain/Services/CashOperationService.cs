using Amazon.Auth.AccessControlPolicy;
using CashFlow.Payment.API.Domain.Services.Interfaces;
using CashFlow.Reports.API.Domain.DTOs;
using CashFlow.Reports.API.Infrastructure.Database.Repositories.Interfaces;
using MongoDB.Driver.Linq;

namespace CashFlow.Payment.API.Domain.Services
{
    public class CashOperationService : ICashOperationService
    {
        private readonly ICashOperationRepository _cashOperationRepository;
        private readonly ILogger<CashOperationService> _logger;

        public CashOperationService(ICashOperationRepository repo, ILogger<CashOperationService> logger)
        {
            _cashOperationRepository = repo;
            _logger = logger;
            _logger = logger;
        }

        public IList<ConsolidateDay> GetConsolidateByDay()
        {
            var result = _cashOperationRepository.Get().ToList();
            return result;
        }

        public async Task<int> SetOperation(CashOperation operation)
        {
            var result = _cashOperationRepository.Get().Where(x => x.Date.Date == operation.CreatedAt.Date).ToList();

            if (result.Count() > 0)
            {
                var dayOperations = result.First();

                switch (operation.Type)
                {
                    case 0:
                        dayOperations.Value -= operation.Value;
                        break;

                    case 1:
                        dayOperations.Value += operation.Value;
                        break;
                }

                return await _cashOperationRepository.UpdateAsync(dayOperations.Id, dayOperations);
            }
            else
            {
                var dayOperation = new ConsolidateDay()
                {
                    Id = operation.Id,
                    Date = operation.CreatedAt,
                    Value = operation.Value
                };

                return await _cashOperationRepository.InsertAsync(dayOperation);

            }

            return 0;
        }

    }
}
