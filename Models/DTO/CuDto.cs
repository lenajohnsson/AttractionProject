using System.Text.RegularExpressions;

namespace Models.DTO;

public class UserCuDto
{
    public virtual Guid? UserId { get; set; }
    public virtual string FirstName { get; set; }
    public virtual string LastName { get; set; }
    public virtual string Email { get; set; }

    public UserCuDto() { }
    public UserCuDto(IUser org)
    {
        UserId = org.UserId;
        FirstName = org.FirstName;
        LastName = org.LastName;
        Email = org.Email;
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

public class ReviewCuDto
{
    public virtual Guid? ReviewId { get; set; }
    public virtual string Comment { get; set; }
    public virtual int ReviewGrade { get; set; }
    public virtual DateTime? Date { get; set; }
    public virtual Guid? AttractionId { get; set; }
    public virtual Guid? UserId { get; set; }

    public ReviewCuDto() { }
    public ReviewCuDto(IReview org)
    {
        Comment = org.Comment;
        ReviewGrade = org.ReviewGrade;
        Date = org.Date;

        AttractionId = org?.Attraction?.AttractionId;
        UserId = org?.User?.UserId;
    }
    public void EnsureValidity()
    {
        if (!string.IsNullOrEmpty(Comment) && !Regex.IsMatch(Comment, @"[A-Öa-ö\s\.,]"))
            throw new ArgumentException("Comment can only contain letters (a-ö) and characters (. ,)");
        if (ReviewGrade < 1 || ReviewGrade > 5)
            throw new ArgumentException("ReviewGrade can only be between 1-5");
        if (Date.HasValue)
        {
            var dateString = Date.Value.ToString("yyyy-MM-dd");
            var parsedDate = DateTime.Parse(dateString);
            if (parsedDate != DateTime.Today)
                throw new ArgumentException("Date of review must be todays date or null");
        }
    }
}