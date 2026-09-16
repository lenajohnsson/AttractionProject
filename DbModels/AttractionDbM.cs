using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;
using Newtonsoft.Json;
using Seido.Utilities.SeedGenerator;

namespace DbModels;

sealed public class AttractionDbM : Attraction, ISeed<AttractionDbM>, IEquatable<AttractionDbM>
{
    [Key]
    public override Guid AttractionId { get; set; }
    [Required]
    public override string AttractionName { get; set; }
    public override string Description { get; set; }

    // Foreign Key variable
    [JsonIgnore]
    public Guid? AddressId { get; set; }

    // Mapping relations
    [NotMapped]
    public override List<IReview> Reviews { get => ReviewsDbM?.ToList<IReview>(); set => throw new NotImplementedException(); }
    [JsonIgnore]
    public List<ReviewDbM> ReviewsDbM { get; set; }

    [NotMapped]
    public override List<ICategory> Categories { get => CategoriesDbM?.ToList<ICategory>(); set => throw new NotImplementedException(); }
    [JsonIgnore]
    [Required] // ??
    public List<CategoryDbM> CategoriesDbM { get; set; }

    [NotMapped]
    public override IAddress Address { get => AddressDbM; set => throw new NotImplementedException(); }
    [JsonIgnore]
    [ForeignKey("AddressId")]
    public AddressDbM AddressDbM { get; set; }

    // Constructors
    public AttractionDbM() { }
    public override AttractionDbM Seed(SeedGenerator seeder)
    {
        base.Seed(seeder);
        return this;
    }

    // IEquatable
    public bool Equals(AttractionDbM other)
    {
        return (other != null) &&
        (this.AttractionName == other.AttractionName);
    }
    public override bool Equals(object obj)
    {
        return Equals(obj as AttractionDbM);
    }
    public override int GetHashCode()
    {
        return AttractionName.GetHashCode();
    }
}