using System.ComponentModel.DataAnnotations;
using Models;
using Seido.Utilities.SeedGenerator;

namespace DbModels;

sealed public class AttractionDbM : Attraction, ISeed<AttractionDbM>
{
    [Key]
    public override Guid AttractionId { get; set; }

    public AttractionDbM() : base() { }

    public new AttractionDbM Seed(SeedGenerator seeder)
    {
        base.Seed(seeder);
        return this;
    }

}