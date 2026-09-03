using System.ComponentModel.DataAnnotations;
using Models;

namespace DbModels;

sealed public class ReviewDbM : Review
{
    [Key]
    public override Guid ReviewId { get; set; }
    public override string Comment { get; set; }
    public override int ReviewGrade { get; set; }
    public override DateTime Date { get; set; }

    public override IAttraction Attraction { get; set; }

    public ReviewDbM() { }
}