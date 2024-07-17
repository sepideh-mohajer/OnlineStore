using OnlineStore.Infrastructure.Pagination;

namespace OnlineStore.Models.Base
{
    public class SearchDto
    {
        public Dictionary<string, string> SearchCriteria { get; set; } = new Dictionary<string, string>();
        public string? SortBy { get; set; }
        public bool SortDescending { get; set; }
        public required PaginationRequestDto Pagination { get; set; }
    }
}
