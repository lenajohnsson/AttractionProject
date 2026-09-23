namespace Models;

public interface IReview
{
    public Guid ReviewId { get; set; }
    public string Comment { get; set; }
    public int ReviewGrade { get; set; }
    public DateTime? Date { get; set; }
    public IAttraction Attraction { get; set; }
    public IUser User { get; set; }
}