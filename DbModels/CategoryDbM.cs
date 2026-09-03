using System.ComponentModel.DataAnnotations;
using Models;

namespace DbModels;

sealed public class CategoryDbM : Category
{
    [Key]
    public override Guid CategoryId { get; set; }
    public override string CategoryType { get; set; }

    public override List<IAttraction> Attractions { get; set; }

    public CategoryDbM() { }
}