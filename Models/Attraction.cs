using Seido.Utilities.SeedGenerator;

namespace Models;

public class Attraction : IAttraction, ISeed<Attraction>, IEquatable<Attraction>
{
    public virtual Guid AttractionId { get; set; }
    public virtual string AttractionName { get; set; }
    public virtual string Description { get; set; }
    public virtual List<IReview> Reviews { get; set; }
    public virtual List<ICategory> Categories { get; set; }
    public virtual IAddress Address { get; set; }

    public bool Seeded { get; set; } = false;

    public Attraction() { }

    public virtual Attraction Seed(SeedGenerator seeder)
    {
        Seeded = true;
        AttractionId = Guid.NewGuid();
        AttractionName = seeder.Attraction;
        Description = seeder.Description;
        return this;
    }

    public Attraction(Attraction org)
    {
        this.AttractionId = org.AttractionId;
        this.AttractionName = org.AttractionName;
    }

    public bool Equals(Attraction other)
    {
        return (other != null) &&
        (this.AttractionName == other.AttractionName);
    }
    public override bool Equals(object obj)
    {
        return Equals(obj as Attraction);
    }
    public override int GetHashCode()
    {
        return AttractionName.GetHashCode();
    }

}