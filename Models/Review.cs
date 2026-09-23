using Seido.Utilities.SeedGenerator;

namespace Models;

public class Review : IReview, ISeed<Review>
{
    public virtual Guid ReviewId { get; set; }
    public virtual string Comment { get; set; }
    public virtual int ReviewGrade { get; set; }
    public virtual DateTime? Date { get; set; }
    public virtual IAttraction Attraction { get; set; }
    public virtual IUser User { get; set; }
    public bool Seeded { get; set; } = false;

    public Review() { }
    public Review(Review org)
    {
        this.Seeded = org.Seeded;
        this.ReviewId = org.ReviewId;
        this.Comment = org.Comment;
        this.ReviewGrade = org.ReviewGrade;
        this.Date = org.Date;
    }

    public virtual Review Seed(SeedGenerator seeding)
    {
        Seeded = true;
        ReviewId = Guid.NewGuid();
        Comment = seeding.Comment;
        ReviewGrade = seeding.Next(1, 6);
        Date = seeding.DateAndTime(1985, 2026).Date;

        return this;
    }
}