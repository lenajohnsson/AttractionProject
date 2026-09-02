namespace Models;

public class User : IUser
{
    public virtual Guid UserId { get; set; }
    public virtual string FirstName { get; set; }
    public virtual string LastName { get; set; }
    public virtual string Email { get; set; }
    public virtual List<IReview> Reviews { get; set; }
    public virtual List<IAttraction> Attractions { get; set; }
    public virtual IAddress Address { get; set; }

    public User() { }

    public User(User org)
    {
        this.UserId = org.UserId;
        this.FirstName = org.FirstName;
        this.LastName = org.LastName;
        this.Email = org.Email;
        this.Address = (org.Address != null) ? new Address((Address)org.Address) : null;
        // if original object of user has an address, 
        // then create a new address object by copying its values
        // and assign it to the new user object.
        // This is so that the referens aren´t copied.
    }
}