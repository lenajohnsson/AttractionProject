using Seido.Utilities.SeedGenerator;

namespace Models;

public class Attraction : IAttraction, ISeed<Attraction>
{
    public virtual Guid AttractionId { get; set; }
    public virtual string AttractionName { get; set; }
    public virtual List<IReview> Reviews { get; set; }
    public virtual List<ICategory> Categories { get; set; }
    public virtual List<IUser> Users { get; set; }
    public virtual IAddress Address { get; set; }

    public bool Seeded { get; set; } = false;

    public Attraction() { }

    public Attraction Seed(SeedGenerator seeder) //för test, få lite data
    {
        Seeded = true;
        AttractionId = Guid.NewGuid();

        AttractionName = seeder.PetName;
        return this;
    }

    public Attraction(Attraction org)
    {
        this.AttractionId = org.AttractionId;
        this.AttractionName = org.AttractionName;
    }

}