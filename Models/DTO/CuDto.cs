using System.Text.RegularExpressions;

namespace Models.DTO;

public class UserCuDto
{
    public virtual Guid? UserId { get; set; }
    public virtual string FirstName { get; set; }
    public virtual string LastName { get; set; }
    public virtual string Email { get; set; }
    public virtual List<Guid> ReviewId { get; set; } = null;

    public UserCuDto() { }
    public UserCuDto(IUser org)
    {
        UserId = org.UserId;
        FirstName = org.FirstName;
        LastName = org.LastName;
        Email = org.Email;

        ReviewId = org.Reviews?.Select(r => r.ReviewId).ToList();
    }
    public void EnsureValidity()
    {
        if (!string.IsNullOrEmpty(FirstName) && !Regex.IsMatch(FirstName, @"([A-Öa-ö\s-'])\w+"))
            throw new ArgumentException("Firstname can only contains letters (a-ö) and characters (' -)");
        if (!string.IsNullOrEmpty(LastName) && !Regex.IsMatch(LastName, @"([A-Öa-ö\s-'])\w+"))
            throw new ArgumentException("Lastname can only contains letters (a-ö) and characters (' -)");
        if (!string.IsNullOrEmpty(Email) && !Regex.IsMatch(Email, @"[a-z0-9'-]+(?:\.[a-z0-9'-]+)*@((?:[a-z0-9-]*[a-z0-9])?\.)+(?:[a-z0-9-]*[a-z0-9])?"))
            throw new ArgumentException("Not an valid email address");
    }
}

public class AttractionCuDto
{
    public virtual Guid? AttractionId { get; set; }
    public virtual string AttractionName { get; set; }
    public virtual string Description { get; set; }
    public virtual List<Guid> ReviewId { get; set; } = null;

    public AttractionCuDto() { }
    public AttractionCuDto(IAttraction org)
    {
        AttractionId = org.AttractionId;
        AttractionName = org.AttractionName;
        Description = org.Description;

        ReviewId = org.Reviews?.Select(r => r.ReviewId).ToList();
    }
    public void EnsureValidity()
    {
        if (!string.IsNullOrEmpty(AttractionName) && !Regex.IsMatch(AttractionName, @"[A-Öa-ö\s'&-]"))
            throw new ArgumentException("AttractionName can only contains letters (a-ö) and characters (' & -)");
        if (!string.IsNullOrEmpty(Description) && !Regex.IsMatch(Description, @"[A-Öa-ö\s\.,]"))
            throw new ArgumentException("Description can only contain letters (a-ö) and characters (. ,)");
    }
}