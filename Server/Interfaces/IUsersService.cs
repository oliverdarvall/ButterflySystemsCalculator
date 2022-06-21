using Microsoft.AspNetCore.Authentication;

namespace Server.Interfaces
{
	public interface IUsersService
	{
		Task<IUser> ValidateAsync(string username, string password);
	}
}
