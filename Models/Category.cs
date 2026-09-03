namespace Models;

public class Category : ICategory
{
    public virtual Guid CategoryId { get; set; }
    public virtual string CategoryType { get; set; }
    public virtual List<IAttraction> Attractions { get; set; }

    public Category() { }
    public Category(Category org)
    {
        this.CategoryId = org.CategoryId;
        this.CategoryType = org.CategoryType;
    }
}