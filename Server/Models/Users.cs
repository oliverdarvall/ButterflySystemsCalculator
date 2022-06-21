using Server.Interfaces;

namespace Server.Models
{
	/// <summary>
	/// InvalidUser is return by Authentication when authentication failed
	/// </summary>
	public class InvalidUser : IUser
	{
		public int ID { get; set; } = 0;
		public string FullName { get; set; } = "";
	}

	/// <summary>
	/// User 
	/// </summary>
	public class User : IUser
	{
		public int ID { get; set; }
		public string FullName { get; set; } = "";
	}
}
