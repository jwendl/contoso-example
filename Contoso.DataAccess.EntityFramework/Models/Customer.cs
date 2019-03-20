using System;

namespace Contoso.DataAccess.EntityFramework.Models
{
    public class Customer
        : BaseModel<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
