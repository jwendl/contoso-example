using System;

namespace Contoso.DataAccess.Cosmos.Models
{
    public class CosmosOptions
    {
        public string Database { get; set; }

        public Uri EndpointUri { get; set; }

        public string PrimaryKey { get; set; }
    }
}
