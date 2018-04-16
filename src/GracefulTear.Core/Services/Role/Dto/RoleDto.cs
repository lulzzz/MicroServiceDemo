using System;
using System.Collections.Generic;
using System.Text;

namespace GracefulTear.Core.Services.Role.Dto
{
	public class RoleDto
	{
		//
		// Summary:
		//     Gets or sets the primary key for this role.
		public virtual string Id { get; set; }
		//
		// Summary:
		//     Gets or sets the name for this role.
		public virtual string Name { get; set; }
		//
		// Summary:
		//     Gets or sets the normalized name for this role.
		public virtual string NormalizedName { get; set; }
		//
		// Summary:
		//     A random value that should change whenever a role is persisted to the store
		public virtual string ConcurrencyStamp { get; set; }

		/// <summary>
		/// Last modification date of this entity.
		/// </summary>
		public virtual DateTimeOffset? LastModificationTime { get; set; }

		/// <summary>
		/// Last modifier user of this entity.
		/// </summary>
		public virtual string LastModifierUserId { get; set; }

		/// <summary>
		/// Creation time of this entity.
		/// </summary>
		public virtual DateTimeOffset? CreationTime { get; set; }

		/// <summary>
		/// Creator of this entity.
		/// </summary>
		public virtual string CreatorUserId { get; set; }

		public virtual ICollection<RoleDto> ChildRoles { get; set; }
	}
}
