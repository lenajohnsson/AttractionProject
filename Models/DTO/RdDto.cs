namespace Models.DTO;

public class AttractionReadListDto
{
    public Guid AttractionId { get; set; }
    public string AttractionName { get; set; }
    public string Description { get; set; }

    public string City { get; set; }
    public string Country { get; set; }

    public List<string> Categories { get; set; }
}

public class AttractionNoCommentDto
{
    public Guid AttractionId { get; set; }
    public string AttractionName { get; set; }
    public string Description { get; set; }
    public string Country { get; set; }
    public List<string> Reviews { get; set; }
}

public class AttractionReadItemDto
{
    public Guid AttractionId { get; set; }
    public string AttractionName { get; set; }
    public string Description { get; set; }
    public List<string> Categories { get; set; }
    public List<ReviewReadDto> Reviews { get; set; }

}

public class UserReadDto
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public List<ReviewReadDto> Reviews { get; set; }
}

public class ReviewReadDto
{
    public Guid ReviewId { get; set; }
    public string Comment { get; set; }
    public int ReviewGrade { get; set; }
    public DateTime? Date { get; set; }
}