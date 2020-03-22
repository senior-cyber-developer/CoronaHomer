using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CoronaHomer.Models.Entities
{
	public class User : IdentityUser
	{
		public int SolidarityScore { get; set; }

		public ICollection<UserQuest> CompletedQuests { get; set; }
	}
}