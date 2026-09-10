using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Models;
using Seido.Utilities.SeedGenerator;

namespace DbModels;

[Index(nameof(City))]
sealed public class AddressDbM : Address, ISeed<AddressDbM>
{
    [Key]
    public override Guid AddressId { get; set; }
    public override string StreetAddress { get; set; }
    public override int ZipCode { get; set; }
    public override string City { get; set; }

    [Required]
    public override string Country { get; set; }

    [NotMapped]
    public override List<IAttraction> Attractions { get => AttractionsDbM?.ToList<IAttraction>(); set => throw new NotImplementedException(); }
    public List<AttractionDbM> AttractionsDbM { get; set; }

    [NotMapped]
    public override List<IUser> Users { get => UsersDbM?.ToList<IUser>(); set => throw new NotImplementedException(); }
    public List<UserDbM> UsersDbM { get; set; }

    public AddressDbM() { }

    public override AddressDbM Seed(SeedGenerator seeding)
    {
        base.Seed(seeding);
        return this;
    }



    // public bool Equals(AddressDbM other)
    // {
    //     return (other != null) &&
    //     ((this.StreetAddress, this.ZipCode, this.City, this.Country) ==
    //     (other.StreetAddress, other.ZipCode, other.City, other.Country));
    // }
    // public override bool Equals(object obj)
    // {
    //     return Equals(obj as AddressDbM);
    // }
    // public override int GetHashCode()
    // {
    //     return (StreetAddress, ZipCode, City, Country).GetHashCode();
    // }
}