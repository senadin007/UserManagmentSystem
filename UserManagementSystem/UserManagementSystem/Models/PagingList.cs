using Microsoft.EntityFrameworkCore;

namespace UserManagementSystem.Models
{
    public class PagingList<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public PagingList(List<T> items, int count, int pageIndex, int pageSize, int nextPage, int previousPage)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }
        public bool PreviousPage
        {
            get { return (PageIndex > 1); }
        }
        public bool NextPage
        {
            get { return (PageIndex < TotalPages); }
        }
        public static async Task<PagingList<T>> CreateAsync(IQueryable<T> source, 
            int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var lastPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(count) / pageSize));

            var nextPage = pageIndex >= 1 && pageIndex < lastPage ? pageIndex + 1 : 0;
            var previousPage = pageIndex > 1 ? pageIndex - 1 : 1;

            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagingList<T>(items, count, pageIndex, pageSize, nextPage, previousPage);
        }
    }
}
