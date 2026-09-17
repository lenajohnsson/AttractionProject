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

