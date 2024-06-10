using App.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq;
using System.Data.Entity;

namespace App.Common.Helpers
{
   public static class IQueryableExtensions
    {
        public async static Task<TResult> ToPageResultAsync<TModel,TResult>(this IQueryable<TModel> source, IPagedRequest request) where TResult : PagedResult<TModel>, new()
        {
            var totalCount = await source.CountAsync();

            var page = request.Page < 1 ? 1 : request.Page;
            var pageLength = request.Page < 100 ? 100 : request.Page;

            var items =  source.Skip((page - 1) * pageLength).Take(pageLength);
            var pageSize = items.Count();

            var totalPage = (int)Math.Ceiling((double)totalCount / request.PageLength);

            var result = new TResult
            {
                Page = page,
                PageLength = pageSize,
                TotalCount = totalCount,
                TotalPage = totalPage,
                Items = items.ToList()
            };

            return result;
        }
    }

    public class PagedResult<T>
    {
        public int Page { get;set; }

        public int PageLength { get; set; }

        public int TotalPage { get; set; }

        public int TotalCount { get; set; }

        public ICollection<T> Items { get; set; }
    }
}
