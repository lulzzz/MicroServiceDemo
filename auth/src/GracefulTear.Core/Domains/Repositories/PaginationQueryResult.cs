using GracefulTear.Core.Applications.Dtos;
using GracefulTear.Core.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace GracefulTear.Core.Domains.Repositories
{
	public class PaginationQueryResult<TEntity> where TEntity : IDto
	{
		public virtual long TotalCount { get; set; }
		public virtual int Page { get; set; }
		public virtual int PageSize { get; set; }

		public IEnumerable<TEntity> Data { get; set; }
	}
}
