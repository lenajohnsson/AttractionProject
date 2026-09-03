using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;

namespace DbModels;

sealed public class ReviewDbM : Review
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

    public ReviewDbM() { }
}