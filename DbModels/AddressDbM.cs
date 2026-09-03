using System.ComponentModel.DataAnnotations;
using Models;

namespace DbModels;

sealed public class AddressDbM : Address, IEquatable<AddressDbM>
{
    [Key]
    public override Guid AddressId { get; set; }
    public override string StreetAddress { get; set; }
    public override string ZipCode { get; set; }
    public override string City { get; set; }
    public override string Country { get; set; }

    public override List<IAttraction> Attractions { get; set; }
    public override List<IUser> Users { get; set; }

    public AddressDbM() { }

    public bool Equals(AddressDbM other)
    {
        return (other != null) &&
        ((this.StreetAddress, this.ZipCode, this.City, this.Country) ==
        (other.StreetAddress, other.ZipCode, other.City, other.Country));
    }
    public override bool Equals(object obj)
    {
        return Equals(obj as AddressDbM);
    }
    public override int GetHashCode()
    {
        return (StreetAddress, ZipCode, City, Country).GetHashCode();
    }
}