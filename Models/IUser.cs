namespace Models;

public interface IUser
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public List<IReview> Reviews { get; set; }
    public List<IAttraction> Attractions { get; set; }
    public IAddress Address { get; set; }
}