namespace MyAppMoreToMore.Models
{
	public class ItemClient		//ten model bedzie sie laczyc z modelami item i client w celu wykonania relacji many to many
	{
		public int ItemId { get; set; }
		public Item Item { get; set; }

		public int ClientId { get; set; }
		public Client Client { get; set; }
	}
}
