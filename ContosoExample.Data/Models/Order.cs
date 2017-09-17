using System;

namespace ContosoExample.Data.Models
{
    public class Order
        : BaseModel
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public string OrderNumber { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
