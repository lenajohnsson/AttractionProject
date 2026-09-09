using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;
using Seido.Utilities.SeedGenerator;

namespace DbModels;

sealed public class CategoryDbM : Category, ISeed<CategoryDbM>
{
    [Key]
    public override Guid CategoryId { get; set; }

    [Required]
    public override string CategoryType { get; set; }

    // [NotMapped]
    // public override List<IAttraction> Attractions { get => AttractionsDbM?.ToList<IAttraction>(); set => throw new NotImplementedException(); }
    // public List<AttractionDbM> AttractionsDbM { get; set; }

    public CategoryDbM() { }

    public override CategoryDbM Seed(SeedGenerator seeding)
    {
        base.Seed(seeding);
        return this;
    }
}