﻿using Contoso.DataAccess.Cosmos.Interfaces;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.DataAccess.Cosmos
{
    public class DataRepository<TModel, TKey>
        : IDataRepository<TModel, TKey>
    {
        private readonly ICosmosClientWrapper cosmosClientWrapper;

        public DataRepository(ICosmosClientWrapper cosmosClientWrapper)
        {
            this.cosmosClientWrapper = cosmosClientWrapper;
        }

        public async Task<IEnumerable<TModel>> FetchAllAsync()
        {
            var cosmosSqlQueryDefinition = new CosmosSqlQueryDefinition($"select * from {typeof(TModel).Name}");
            var queryResultSetIterator = cosmosClientWrapper.Items.CreateItemQuery<TModel>(cosmosSqlQueryDefinition, 10);

            var items = new List<TModel>();
            while (queryResultSetIterator.HasMoreResults)
            {
                var currentResultSet = await queryResultSetIterator.FetchNextSetAsync();
                foreach (var item in currentResultSet)
                {
                    items.Add(item);
                }
            }

            return items.AsEnumerable();
        }

        public async Task<TModel> CreateItemAsync(TKey partitionKey, TModel model)
        {
            Argument.IsNotNull(() => partitionKey);
            Argument.IsNotNull(() => model);
            var cosmosItemResponse = await cosmosClientWrapper.Items.CreateItemAsync(partitionKey, model);
            return cosmosItemResponse.Resource;
        }
    }
}
