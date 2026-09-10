using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;
using Seido.Utilities.SeedGenerator;

namespace DbModels;

sealed public class UserDbM : User, ISeed<UserDbM>
{
    [Key]
    public override Guid UserId { get; set; }

    [Required]
    public override string FirstName { get; set; }

    [Required]
    public override string LastName { get; set; }
    public override string Email { get; set; }

    [NotMapped]
    public override List<IReview> Reviews { get => ReviewsDbM?.ToList<IReview>(); set => throw new NotImplementedException(); }
    public List<ReviewDbM> ReviewsDbM { get; set; }

    [NotMapped]
    public override List<IAttraction> Attractions { get => AttractionsDbM?.ToList<IAttraction>(); set => throw new NotImplementedException(); }
    public List<AttractionDbM> AttractionsDbM { get; set; }

    [NotMapped]
    public override IAddress Address { get => AddressDbM; set => throw new NotImplementedException(); }
    public AddressDbM AddressDbM { get; set; }

    public UserDbM() { }

    public override UserDbM Seed(SeedGenerator seeding)
    {
        base.Seed(seeding);
        return this;
    }
}