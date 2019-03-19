using Contoso.DataAccess.Cosmos.Interfaces;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace Contoso.DataAccess.Cosmos
{
    public class CosmosClientWrapper
        : ICosmosClientWrapper
    {
        private readonly CosmosClient cosmosClient;

        public CosmosClientWrapper(string accountEndPoint, string accountKey)
        {
            cosmosClient = new CosmosClient(accountEndPoint, accountKey);
        }

        public async Task CreateCollectionIfNotExists(string databaseId, string collectionId, string partitionKeyPath)
        {
            CosmosDatabase = await cosmosClient.Databases.CreateDatabaseIfNotExistsAsync(databaseId);

            var cosmosContainerSettings = new CosmosContainerSettings("id", partitionKeyPath);
            CosmosContainer = await CosmosDatabase.Containers.CreateContainerIfNotExistsAsync(cosmosContainerSettings);
        }

        public CosmosItems Items
        {
            get
            {
                return CosmosContainer.Items;
            }
        }

        public CosmosDatabase CosmosDatabase { get; set; }
        public CosmosContainer CosmosContainer { get; set; }
    }
}
