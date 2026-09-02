namespace Models;

public interface IAttraction
{
    public Guid AttractionId { get; set; }
    public string AttractionName { get; set; }
    public List<IReview> Reviews { get; set; }
    public List<ICategory> Categories { get; set; }
    public List<IUser> Users { get; set; }
    public IAddress Address { get; set; }
}

