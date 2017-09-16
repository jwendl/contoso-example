﻿namespace ContosoExample.Data.Models
{
    public class Location
        : BaseModel
    {
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }
    }
}
