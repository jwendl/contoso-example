using System;
using System.Collections.Generic;
using System.Text;

namespace ContosoExample.Data.Models
{
    public class Customer
        : BaseModel
    {
        public string UserAlias { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }
    }
}
