using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace Contoso.DataAccess.Cosmos.Interfaces
{
    public interface ICosmosClientWrapper
    {
        Task CreateCollectionIfNotExists(string databaseId, string collectionId, string partitionKeyPath);

        CosmosItems Items { get; }
    }
}
