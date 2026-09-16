using Seido.Utilities.SeedGenerator;

namespace Models;

public class User : IUser, ISeed<User>
{
    public virtual Guid UserId { get; set; }
    public virtual string FirstName { get; set; }
    public virtual string LastName { get; set; }
    public virtual string Email { get; set; }
    public virtual List<IReview> Reviews { get; set; }
    public bool Seeded { get; set; } = false;

    public User() { }

    public User(User org)
    {
        this.UserId = org.UserId;
        this.FirstName = org.FirstName;
        this.LastName = org.LastName;
        this.Email = org.Email;

    }

    public virtual User Seed(SeedGenerator seeder)
    {
        Seeded = true;
        UserId = Guid.NewGuid();
        FirstName = seeder.FirstName;
        LastName = seeder.LastName;
        Email = seeder.Email(FirstName, LastName);

        return this;
    }
}