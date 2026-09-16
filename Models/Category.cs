using Seido.Utilities.SeedGenerator;

namespace Models;

public class Category : ICategory, ISeed<Category>
{
    public virtual Guid CategoryId { get; set; }
    public virtual string CategoryType { get; set; }
    public virtual List<IAttraction> Attractions { get; set; }
    public bool Seeded { get; set; } = false;

    public Category() { }
    public Category(Category org)
    {
        this.Seeded = org.Seeded;
        this.CategoryId = org.CategoryId;
        this.CategoryType = org.CategoryType;
    }

    public virtual Category Seed(SeedGenerator seeding)
    {
        Seeded = true;
        CategoryId = Guid.NewGuid();
        CategoryType = seeding.Category;

        return this;
    }
}