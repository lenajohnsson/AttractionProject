using System.ComponentModel.DataAnnotations;
using Models;
using Seido.Utilities.SeedGenerator;

namespace DbModels;

sealed public class AttractionDbM : Attraction, ISeed<AttractionDbM>
{
    [Key]
    public override Guid AttractionId { get; set; }
    public override string AttractionName { get; set; }

    public override List<IReview> Reviews { get; set; }
    public override List<ICategory> Categories { get; set; }
    public override List<IUser> Users { get; set; }
    public override IAddress Address { get; set; }

    public AttractionDbM() { }

    public new AttractionDbM Seed(SeedGenerator seeder)
    {
        base.Seed(seeder);
        return this;
    }

}