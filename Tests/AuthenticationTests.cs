
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Server.Domain;
using Server.Interfaces;

namespace ServerTests
{
	internal class AuthenticationTests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void Authentication()
		{
			UsersService usersService = new ();
			IDictionary<string, StringValues>  headers = new Dictionary<string, StringValues>();

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
			AuthenticateResult ar = BasicAuthentication.AuthenticateAsync(null, null, null).Result;
			Assert.That(ar.Succeeded, Is.False);

			ar = BasicAuthentication.AuthenticateAsync("whatever", null, null).Result;
			Assert.That(ar.Succeeded, Is.False);

			ar = BasicAuthentication.AuthenticateAsync("whatever", headers, null).Result;
			Assert.That(ar.Succeeded, Is.False);
#pragma warning restore CS8625

			ar = BasicAuthentication.AuthenticateAsync("whatever", headers, usersService).Result;
			Assert.That(ar.Succeeded, Is.False);

			var schemeName = Constants.BasicAuthentication;

			ar = BasicAuthentication.AuthenticateAsync(schemeName, headers, usersService).Result;
			Assert.That(ar.Succeeded, Is.False);

			headers.Add(Constants.AuthorizationHeader, new StringValues(schemeName));

			ar = BasicAuthentication.AuthenticateAsync(schemeName, headers, usersService).Result;
			Assert.That(ar.Succeeded, Is.False);

			headers.Clear();
			var credentials = "wrong:credentials";
			headers.Add(Constants.AuthorizationHeader, new StringValues(credentials));

			ar = BasicAuthentication.AuthenticateAsync(schemeName, headers, usersService).Result;
			Assert.That(ar.Succeeded, Is.False);

			headers.Clear();
			credentials = "wrong:credentials".ToBase64();
			headers.Add(Constants.AuthorizationHeader, new StringValues($"{Constants.BasicAuthentication} {credentials}"));

			ar = BasicAuthentication.AuthenticateAsync(schemeName, headers, usersService).Result;
			Assert.That(ar.Succeeded, Is.False);

			headers.Clear();
			credentials = "butterfly:systems".ToBase64();
			headers.Add(Constants.AuthorizationHeader, new StringValues($"{Constants.BasicAuthentication} {credentials}"));

			ar = BasicAuthentication.AuthenticateAsync(schemeName, headers, usersService).Result;
			Assert.That(ar.Succeeded, Is.True);
		}

		[Test]
		public void UsersService()
		{
			UsersService usersService = new ();

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
			IUser user = usersService.ValidateAsync(null, null).Result;
			Assert.That(user.ID, Is.EqualTo(0));

			user = usersService.ValidateAsync("username", null).Result;
			Assert.That(user.ID, Is.EqualTo(0));
#pragma warning restore CS8625

			user = usersService.ValidateAsync("", "").Result;
			Assert.That(user.ID, Is.EqualTo(0));

			user = usersService.ValidateAsync("butterfly", "").Result;
			Assert.That(user.ID, Is.EqualTo(0));

			user = usersService.ValidateAsync("", "systems").Result;
			Assert.That(user.ID, Is.EqualTo(0));

			user = usersService.ValidateAsync("butterfly", "systems").Result;
			Assert.That(user.ID, Is.GreaterThan(0));
		}
	}
}