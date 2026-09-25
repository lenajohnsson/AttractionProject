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

public class GuestUserInfoOverviewDto
{
    public int NrSeededAttractions { get; set; } = 0;
    public int NrSeededUsers { get; set; } = 0;
    public int NrSeededCities { get; set; } = 0;
}

public class GuestUserInfoAllDto
{
    public GuestUserInfoDto Db { get; set; } = null;
    public GuestUserInfoOverviewDto Overview { get; set; } = null;
}