using CashFlow.Reports.API.Domain.DTOs.Base;
using CashFlow.Reports.API.Infrastructure.Database.Repositories.Base.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Newtonsoft.Json;


namespace CashFlow.Reports.API.Infrastructure.Database.Repositories.Base
{
    public abstract class Repository<TDocument> : IRepository<TDocument> where TDocument : EntityBase
    {
        protected readonly IMongoCollection<TDocument> _deliverDatabaseSettings;
        //private readonly IClientSessionHandle _session;
        private readonly ILogger _logger;

        public Repository(
           IConfiguration configuration,
           IMongoClient mongoClient,
           ILogger logger,
           string collectionName)
        {
            //_session = mongoClient.StartSession();
            var mongoDatabase = mongoClient.GetDatabase("CashFlow");
            _deliverDatabaseSettings = mongoDatabase.GetCollection<TDocument>(collectionName);
            _logger = logger;
        }

        public IQueryable<TDocument> Get() =>
            _deliverDatabaseSettings.AsQueryable();

        public async Task<TDocument> GetAsync(string id) =>
         await _deliverDatabaseSettings.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<int> InsertAsync(TDocument document)
        {
            try
            {
                ///_session.StartTransaction();
                await _deliverDatabaseSettings.InsertOneAsync(document);
                //_session.CommitTransaction();
                return 1;
            }
            catch (Exception ex)
            {
                //await _session.AbortTransactionAsync();
                _logger.LogError(ex, ex.Message, document);
                return 0;
            }
        }

        public async Task<int> UpdateAsync(string id, TDocument document)
        {
            try
            {
                //_session.StartTransaction();
                await _deliverDatabaseSettings.ReplaceOneAsync(x => x.Id == id, document);
                //_session.CommitTransaction();
                return 1;
            }
            catch (Exception ex)
            {
                //await _session.AbortTransactionAsync();
                _logger.LogError(ex, ex.Message, document);
                return 0;
            }
        }

        public async Task<int> UpdateByIdAsync(string id, string key, object value)
        {
            try
            {
                //_session.StartTransaction();

                var json = JsonConvert.SerializeObject(new Dictionary<string, object>() { { key, value } });
                var serializedObject = BsonDocument.Parse(json);

                await _deliverDatabaseSettings.FindOneAndUpdateAsync(
                    o => o.Id == id,
                    Builders<TDocument>.Update.Set(serializedObject.First().Name, serializedObject.First().Value)
                    );

                //_session.CommitTransaction();
                return 1;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                _logger.LogError(ex, ex.Message);
                //await _session.AbortTransactionAsync();
                return 0;
            }
        }

        public async Task<int> UpdateChildListByIdAsync(string id, string parentKey, string childKey, object child)
        {
            try
            {
                //_session.StartTransaction();

                var json = JsonConvert.SerializeObject(
                    new Dictionary<string, object>() { { childKey, child } });
                var serializedObject = BsonDocument.Parse(json);

                var dataName = parentKey + "." + childKey;

                await _deliverDatabaseSettings.FindOneAndUpdateAsync(
                    o => o.Id == id,
                    Builders<TDocument>.Update.Set(dataName, serializedObject.First().Value)
                    );

                //_session.CommitTransaction();
                return 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                //await _session.AbortTransactionAsync();
                return 0;
            }
        }

        public async Task<int> DeleteAsync(string id)
        {
            try
            {
                //_session.StartTransaction();
                await _deliverDatabaseSettings.DeleteOneAsync(x => x.Id == id);
                //_session.CommitTransaction();
                return 1;
            }
            catch (Exception ex)
            {
                //await _session.AbortTransactionAsync();
                _logger.LogError(ex, ex.Message);
                return 0;
            }

        }
    }
}
