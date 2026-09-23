using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;
using Models.DTO;
using Newtonsoft.Json;
using Seido.Utilities.SeedGenerator;

namespace DbModels;

sealed public class UserDbM : User, ISeed<UserDbM>
{
    [Key]
    public override Guid UserId { get; set; }
    [Required]
    public override string FirstName { get; set; }
    [Required]
    public override string LastName { get; set; }
    public override string Email { get; set; }

    // Mapping relations
    // One user can have many reviews
    [NotMapped]
    public override List<IReview> Reviews { get => ReviewsDbM?.ToList<IReview>(); set => throw new NotImplementedException(); }
    [JsonIgnore]
    public List<ReviewDbM> ReviewsDbM { get; set; }

    // Update from DTO
    public UserDbM UpdateFromDto(UserCuDto org)
    {
        FirstName = org.FirstName;
        LastName = org.LastName;
        Email = org.Email;
        return this;
    }
    // Constructor
    public UserDbM() { }
    public UserDbM(UserCuDto org)
    {
        UserId = Guid.NewGuid();
        UpdateFromDto(org);
    }

    // Seed
    public override UserDbM Seed(SeedGenerator seeding)
    {
        base.Seed(seeding);
        return this;
    }
}