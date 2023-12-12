using Microsoft.AspNetCore.Identity;

namespace AtreidesTeamProject1.Models
{
	public class UserRoles : IdentityUser
	{
		public int UserRoleID { get; set; }

		public string Role { get; set; }
	}
}
