using Seido.Utilities.SeedGenerator;

namespace Models;

public class Address : IAddress, ISeed<Address>, IEquatable<Address>
{
    public virtual Guid AddressId { get; set; }
    public virtual string StreetAddress { get; set; }
    public virtual int ZipCode { get; set; }
    public virtual string City { get; set; }
    public virtual string Country { get; set; }
    public virtual List<IAttraction> Attractions { get; set; }
    public virtual List<IUser> Users { get; set; }
    public bool Seeded { get; set; } = false;

    public Address() { }

    public Address(Address org)
    {
        this.AddressId = org.AddressId;
        this.StreetAddress = org.StreetAddress;
        this.ZipCode = org.ZipCode;
        this.City = org.City;
        this.Country = org.Country;
    }

    public virtual Address Seed(SeedGenerator seeder)
    {
        Seeded = true;
        AddressId = Guid.NewGuid();
        StreetAddress = seeder.StreetAddress(Country);
        ZipCode = seeder.ZipCode;
        City = seeder.City(Country);
        Country = seeder.Country;

        return this;
    }


    public bool Equals(Address other)
    {
        return (other != null) &&
        ((this.StreetAddress, this.ZipCode, this.City, this.Country) ==
        (other.StreetAddress, other.ZipCode, other.City, other.Country));
    }
    public override bool Equals(object obj)
    {
        return Equals(obj as Address);
    }
    public override int GetHashCode()
    {
        return (StreetAddress, ZipCode, City, Country).GetHashCode();
    }

}