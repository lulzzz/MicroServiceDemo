using System.Collections.Generic;
using System.Linq;

namespace Auth.Web.Infrastructure
{
	public class PagedList<T> : List<T>, IPagedList<T>
	{
		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="source">source</param>
		/// <param name="pageIndex">Page index</param>
		/// <param name="pageSize">Page size</param>
		public PagedList(IQueryable<T> source, int pageIndex, int pageSize)
		{
			int total = source.Count();
			this.TotalCount = total;
			this.PageCount = total / pageSize;

			if (total % pageSize > 0)
				PageCount++;

			this.PageSize = pageSize;
			this.Page = pageIndex;
			this.AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());
		}

		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="source">source</param>
		/// <param name="pageIndex">Page index</param>
		/// <param name="pageSize">Page size</param>
		public PagedList(IList<T> source, int pageIndex, int pageSize)
		{
			TotalCount = source.Count();
			PageCount = TotalCount / pageSize;

			if (TotalCount % pageSize > 0)
				PageCount++;

			this.PageSize = pageSize;
			this.Page = pageIndex;
			// TODO EF Core 2.0 Pagination has some isseues
			this.AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());
		}

		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="source">source</param>
		/// <param name="pageIndex">Page index</param>
		/// <param name="pageSize">Page size</param>
		/// <param name="totalCount">Total count</param>
		public PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
		{
			TotalCount = totalCount;
			PageCount = TotalCount / pageSize;

			if (TotalCount % pageSize > 0)
				PageCount++;

			this.PageSize = pageSize;
			this.Page = pageIndex;
			this.AddRange(source);
		}

		public int Page  { get; private set; }
		public int PageSize { get; private set; }
		public int TotalCount { get; private set; }
		public int PageCount { get; private set; }

		public bool HasPreviousPage
		{
			get { return (Page > 0); }
		}

		public bool HasNextPage
		{
			get { return (Page + 1 < PageCount); }
		}
	}
}
