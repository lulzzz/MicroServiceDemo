using GracefulTear.Core.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace GracefulTear.Core.Domains
{
	public interface IAudited : IEntity<string>
	{
		/// <summary>
		/// Last modification date of this entity.
		/// </summary>
		DateTimeOffset? LastModificationTime { get; set; }

		/// <summary>
		/// Last modifier user of this entity.
		/// </summary>
		string LastModifierUserId { get; set; }

		/// <summary>
		/// Creation time of this entity.
		/// </summary>
		DateTimeOffset? CreationTime { get; set; }

		/// <summary>
		/// Creator of this entity.
		/// </summary>
		string CreatorUserId { get; set; }
	}
}
