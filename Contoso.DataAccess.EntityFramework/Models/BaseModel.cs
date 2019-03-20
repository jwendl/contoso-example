namespace Contoso.DataAccess.EntityFramework.Models
{
    public abstract class BaseModel<TKey>
    {
        public TKey Id { get; set; }
    }
}
