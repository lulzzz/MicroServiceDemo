using Auth.Core.Domains;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Auth.Core.Identity
{
	public class Role : IdentityRole, IAudited
	{
		public string ParentRoleId { get; set; }

		public RoleType RoleType { get; set; } = RoleType.Role;

		[ForeignKey("ParentRoleId")]
		public virtual ICollection<Role> ChildRoles { get; set; }

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
	}
}
