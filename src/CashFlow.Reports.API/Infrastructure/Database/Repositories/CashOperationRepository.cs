using CashFlow.Reports.API.Domain.DTOs;
using CashFlow.Reports.API.Infrastructure.Database.Enums;
using CashFlow.Reports.API.Infrastructure.Database.Repositories.Base;
using CashFlow.Reports.API.Infrastructure.Database.Repositories.Interfaces;
using MongoDB.Driver;

namespace CashFlow.Reports.API.Infrastructure.Database.Repositories
{
    public class CashOperationRepository : Repository<ConsolidateDay>, ICashOperationRepository
    {
        public CashOperationRepository(IConfiguration context, IMongoClient mongoClient, ILogger<CashOperationRepository> logger)
            : base(context, mongoClient, logger, DataBaseEnum.ConsolidateDay.ToString())
        { }
    }
}
