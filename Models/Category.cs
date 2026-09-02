namespace Models;

public class Category : ICategory
{
    public virtual Guid CategoryId { get; set; }
    public virtual string CategoryType { get; set; }
    public List<IAttraction> Attractions { get; set; }
}