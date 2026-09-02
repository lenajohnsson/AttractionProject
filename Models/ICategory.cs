namespace Models;

public interface ICategory
{
    public Guid CategoryId { get; set; }
    public string CategoryType { get; set; }

    public List<IAttraction> Attractions { get; set; }

}