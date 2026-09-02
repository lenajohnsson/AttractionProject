namespace Models;

public interface IAddress
{
    public Guid AddressId { get; set; }
    public string StreetAddress { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public List<IAttraction> Attractions { get; set; }
    public List<IUser> Users { get; set; }
}