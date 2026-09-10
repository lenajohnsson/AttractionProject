using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;
using Seido.Utilities.SeedGenerator;

namespace DbModels;

sealed public class AttractionDbM : Attraction, ISeed<AttractionDbM>
{
    [Key]
    public override Guid AttractionId { get; set; }

    [Required]
    public override string AttractionName { get; set; }
    public override string Description { get; set; }

    [NotMapped]
    public override List<IReview> Reviews { get => ReviewsDbM?.ToList<IReview>(); set => throw new NotImplementedException(); }
    public List<ReviewDbM> ReviewsDbM { get; set; }

    [NotMapped]
    public override List<ICategory> Categories { get => CategoriesDbM?.ToList<ICategory>(); set => throw new NotImplementedException(); }
    public List<CategoryDbM> CategoriesDbM { get; set; }

    [NotMapped]
    public override List<IUser> Users { get => UsersDbM?.ToList<IUser>(); set => throw new NotImplementedException(); }
    public List<UserDbM> UsersDbM { get; set; }

    [NotMapped]
    public override IAddress Address { get => AddressDbM; set => throw new NotImplementedException(); }
    public AddressDbM AddressDbM { get; set; }

    public AttractionDbM() { }

    public override AttractionDbM Seed(SeedGenerator seeder)
    {
        base.Seed(seeder);
        return this;
    }

}