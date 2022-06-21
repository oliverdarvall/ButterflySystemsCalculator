using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Server.Interfaces;

namespace Server.Models
{
	/// <summary>
	/// UsersService 
	/// </summary>
	public class UsersService : IUsersService
	{
		public static IUser InvalidUser { get; set; } = new InvalidUser();

		public async Task<IUser> ValidateAsync(string username, string passwordHash)
		{
			// Get the user model from a persistent store, generate the passwordHash and compare
			// But for now just compare the hard-coded credentials

			return await Task<IUser>.Run(() =>
			{
				if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(passwordHash))
				{
					if (
						string.Equals(username, "butterfly", StringComparison.CurrentCultureIgnoreCase) &&
						string.Equals(passwordHash, "systems", StringComparison.CurrentCultureIgnoreCase)
						)
						return new User() { ID = 1, FullName = "Butterfly Systems" };
				}

				return InvalidUser;
			});
		}
	}
}
