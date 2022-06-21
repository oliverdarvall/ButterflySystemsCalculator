using System.Diagnostics;
using System.Text;

namespace Server.Domain
{
	/// <summary>
	/// TextHelper provides functions for commonly performed text related operations
	/// </summary>
	public static class TextHelper
	{
		// Converts the received text string to Base64 encoded string
		public static string ToBase64(this string text)
		{
			try
			{
				var bytes = Encoding.UTF8.GetBytes(text);

				return System.Convert.ToBase64String(bytes);
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
			}

			return "";
		}
	}
}
