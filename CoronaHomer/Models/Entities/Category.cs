using System.Collections.Generic;

namespace CoronaHomer.Models.Entities
{
	public class Category
	{
		public int Id { get; set; }

		public ICollection<Quest> Quests { get; set; }

		public string Description { get; set; }
	}
}