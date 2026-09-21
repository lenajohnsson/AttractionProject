namespace Models.DTO;

public class GuestUserInfoDto
{
    public int NrSeededAttractions { get; set; } = 0;
    public int NrUnSeededAttractions { get; set; } = 0;
    public int NrAttractionsWithReviews { get; set; } = 0;

    public int NrSeededUsers { get; set; } = 0;
    public int NrUnseededUsers { get; set; } = 0;

    public int NrSeededAddresses { get; set; } = 0;
    public int NrUnseededAddresses { get; set; } = 0;

    public int NrSeededReviews { get; set; } = 0;
    public int NrUnseededReviews { get; set; } = 0;

    public int NrSeededCategories { get; set; } = 0;
    public int NrUnseededCategories { get; set; } = 0;
}

public class GuestUserInfoOverwiewDto
{
    public int NrSeededAttractions { get; set; } = 0;
    public int NrSeededUsers { get; set; } = 0;
    public int NrSeededCities { get; set; } = 0;
}

public class GuestUserInfoAddressDto
{
    public string Country { get; set; } = null;
    public int NrOfAddresses { get; set; } = 0;
}

public class GuestUserInfoUserDto
{
    public string Country { get; set; } = null;
    public int NrOfUsers { get; set; } = 0;
}

public class GuestUserInfoAttractionDto
{
    public string AttractionName { get; set; } = null;
    public int NrOfAttractions { get; set; } = 0;
}

public class GuestUserInfoReviewDto
{
    public string Comment { get; set; } = null;
    public int NrOfReviews { get; set; } = 0;
}

public class GuestUserInfoCategoryDto
{
    public string CategoryType { get; set; } = null;
    public int NrOfCategories { get; set; } = 0;
}

public class GuestUserInfoAllDto
{
    public GuestUserInfoDto Db { get; set; } = null;
    public GuestUserInfoOverwiewDto Overwiew { get; set; } = null;
    public List<GuestUserInfoAddressDto> Addresses { get; set; } = null;
    public List<GuestUserInfoUserDto> Users { get; set; } = null;
    public List<GuestUserInfoAttractionDto> Attractions { get; set; } = null;
    public List<GuestUserInfoReviewDto> Reviews { get; set; } = null;
    public List<GuestUserInfoCategoryDto> Categories { get; set; } = null;
}