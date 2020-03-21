namespace CoronaHomer.Models.Entities
{
	public class Quest
	{
		public int Id { get; set; }

		public int CategoryId { get; set; }
		public Category Category { get; set; }

		public string Description { get; set; }
		public int Score { get; set; }
	}
}