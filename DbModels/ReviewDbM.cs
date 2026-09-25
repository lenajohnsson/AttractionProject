using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;
using Models.DTO;
using Newtonsoft.Json;
using Seido.Utilities.SeedGenerator;

namespace DbModels;

sealed public class ReviewDbM : Review, ISeed<ReviewDbM>
{
    [Key]
    public override Guid ReviewId { get; set; }
    [Required]
    public override string Comment { get; set; }
    public override int ReviewGrade { get; set; }
    public override DateTime? Date { get; set; }

    // Foreign Key variables
    public Guid AttractionId { get; set; }
    public Guid UserId { get; set; }

    // Mapping relations
    // Many reviews can have one attraction
    [NotMapped]
    public override IAttraction Attraction { get => AttractionDbM; set => throw new NotImplementedException(); }
    [JsonIgnore]
    [ForeignKey("AttractionId")]
    public AttractionDbM AttractionDbM { get; set; }

    // Many reviews can have one user
    [NotMapped]
    public override IUser User { get => UserDbM; set => throw new NotImplementedException(); }
    [JsonIgnore]
    [ForeignKey("UserId")]
    public UserDbM UserDbM { get; set; }

    // Update from DTO
    public ReviewDbM UpdateFromDto(ReviewCuDto org)
    {
        Comment = org.Comment;
        ReviewGrade = org.ReviewGrade;
        Date = org.Date;
        return this;
    }

    // Constructor
    public ReviewDbM() { }
    public ReviewDbM(ReviewCuDto org)
    {
        ReviewId = Guid.NewGuid();
        UpdateFromDto(org);
    }

    // Seed
    public override ReviewDbM Seed(SeedGenerator seeding)
    {
        base.Seed(seeding);
        return this;
    }
}