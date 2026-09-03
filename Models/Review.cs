namespace Models;

public class Review : IReview
{
    public virtual Guid ReviewId { get; set; }
    public virtual string Comment { get; set; }
    public virtual int ReviewGrade { get; set; }
    public virtual DateTime Date { get; set; }
    public virtual IAttraction Attraction { get; set; }

    public Review() { }

    public Review(Review org)
    {
        this.ReviewId = org.ReviewId;
        this.Comment = org.Comment;
        this.ReviewGrade = org.ReviewGrade;
        this.Date = org.Date;
    }
}