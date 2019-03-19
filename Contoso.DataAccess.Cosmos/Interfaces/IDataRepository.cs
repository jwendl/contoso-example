using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contoso.DataAccess.Cosmos.Interfaces
{
    public interface IDataRepository<TModel, TKey>
    {
        Task<IEnumerable<TModel>> FetchAllAsync(TKey partitionKey);

        Task<TModel> CreateItemAsync(TKey partitionKey, TModel model);
    }
}
