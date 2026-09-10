using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;
using Seido.Utilities.SeedGenerator;

namespace DbModels;

sealed public class ReviewDbM : Review, ISeed<ReviewDbM>
{
    [Key]
    public override Guid ReviewId { get; set; }

    [Required]
    public override string Comment { get; set; }
    public override int ReviewGrade { get; set; }
    public override DateTime Date { get; set; }

    [NotMapped]
    public override IAttraction Attraction { get => AttractionDbM; set => throw new NotImplementedException(); }
    public AttractionDbM AttractionDbM { get; set; }

    [NotMapped]
    public override IUser User { get => UserDbM; set => throw new NotImplementedException(); }
    public UserDbM UserDbM { get; set; }

    public ReviewDbM() { }
    public override ReviewDbM Seed(SeedGenerator seeding)
    {
        base.Seed(seeding);
        return this;
    }
}