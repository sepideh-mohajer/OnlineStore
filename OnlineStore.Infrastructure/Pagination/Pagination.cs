using Newtonsoft.Json;

namespace OnlineStore.Infrastructure.Pagination;

public class Pagination
{
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int? TotalItemCount { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int? PageNumber { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int? PageSize { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int? PageCount { get; set; }
}