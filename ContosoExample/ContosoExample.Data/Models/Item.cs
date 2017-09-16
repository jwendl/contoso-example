namespace ContosoExample.Data.Models
{
    public class Item
        : BaseModel
    {
        public string ItemNumber { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
