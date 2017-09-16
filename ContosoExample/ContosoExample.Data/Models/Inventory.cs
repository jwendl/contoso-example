namespace ContosoExample.Data.Models
{
    public class Inventory
        : BaseModel
    {
        public int ItemId { get; set; }
        public Item Item { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }

        public int Quantity { get; set; }
    }
}
