using CashFlow.Financial.API.Domain.Entities;
using CashFlow.Financial.API.Domain.Services.Interfaces;
using CashFlow.Financial.API.Infrastructure.Database.Repositories.Interfaces;
using CashFlow.Financial.API.Infrastructure.MQ.Publishers.Interfaces;
using Newtonsoft.Json;

namespace CashFlow.Financial.API.Domain.Services
{
    public class CashOperationService : ICashOperationService
    {
        private readonly ICashOperationRepository _cashOperationRepository;
        private readonly ILogger<CashOperationService> _logger;
        private readonly ICashOperationReportPublisher _publisher;

        public CashOperationService(ICashOperationRepository repo, ICashOperationReportPublisher publisher, ILogger<CashOperationService> logger)
        {
            _cashOperationRepository = repo;
            _logger = logger;
            _publisher = publisher;
            _logger = logger;

            publisher.EnsureCreated();
        }

        public async Task<CashOperationEntity> AddCredit(CashOperationEntity cashOperation)
        {
            try
            {
                cashOperation.Type = 1;
                return await this.Add(cashOperation);
            }
            catch (Exception ex)
            {
                var param = new object[] { cashOperation };
                _logger.LogError(ex, "Erro adicionando uma operação de credito", param);
                return null;
            }
        }

        public async Task<CashOperationEntity> AddDebit(CashOperationEntity cashOperation)
        {
            try
            {
                cashOperation.Type = 0;
                return await this.Add(cashOperation);
            }
            catch (Exception ex)
            {
                var param = new object[] { cashOperation };
                _logger.LogError(ex, "Erro adicionando uma operação de débito", param);
                return null;
            }
        }

        public async Task<IEnumerable<CashOperationEntity>> GetAll()
        {
            return await _cashOperationRepository.GetAllAsync();
        }

        private async Task<CashOperationEntity> Add(CashOperationEntity cashOperation)
        {
            cashOperation.Id = Guid.NewGuid();
            cashOperation.CreatedAt = DateTime.UtcNow;

            await _cashOperationRepository.AddAsync(cashOperation);
            _publisher.SendMessage(JsonConvert.SerializeObject(cashOperation));
            return cashOperation;
        }
    }
}
