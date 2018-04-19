using System.Collections.Generic;

namespace GracefulTear.Web.Infrastructure
{
	/// <summary>
	/// 分页
	/// </summary>
	public interface IPagedList<T> : IList<T>
	{
		/// <summary>
		/// 页数，即第几页，从1开始
		/// </summary>
		int Page { get; }
		/// <summary>
		/// 每页显示行数
		/// </summary>
		int PageSize { get; }
		/// <summary>
		/// 总行数
		/// </summary>
		int TotalCount { get; }
		/// <summary>
		/// 总页数
		/// </summary>
		int PageCount { get; }

		bool HasPreviousPage { get; }
		bool HasNextPage { get; }
	}
}
