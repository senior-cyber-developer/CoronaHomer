namespace CoronaHomer.Models.Entities
{
	public class UserQuest
	{
		public string UserId { get; set; }
		public User User { get; set; }

		public int QuestId { get; set; }
		public Quest Quest { get; set; }
	}
}