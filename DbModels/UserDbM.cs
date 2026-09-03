using System.ComponentModel.DataAnnotations;
using Models;

namespace DbModels;

sealed public class UserDbM : User
{
    [Key]
    public override Guid UserId { get; set; }
    public override string FirstName { get; set; }
    public override string LastName { get; set; }
    public override string Email { get; set; }

    public override List<IReview> Reviews { get; set; }
    public override List<IAttraction> Attractions { get; set; }
    public override IAddress Address { get; set; }

    public UserDbM() { }
}