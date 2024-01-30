namespace Movie.Api.Models
{
    public class LeaderboardRequestModel
    {
        public string LeaderboardName { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
