using Server.Interfaces;

namespace Server.Operations
{
	/// <summary>
	/// InvalidResult is returned upon any failure during calculation
	/// </summary>
	public class InvalidResult : IOperationResult
	{
		public string Display { get; set; } = "Invalid operation or values !";
		public bool Success { get; set; } = false;
		public double Value { get; set; } = 0;
	}

	/// <summary>
	/// SuccessResult is returned upon successful calculation
	/// </summary>
	public class SuccessResult : IOperationResult
	{
		public SuccessResult(string display, double value)
		{
			Display = display;
			Value = value;
		}

		public string Display { get; set; } = "";
		public bool Success { get; set; } = true;
		public double Value { get; set; } = 0;
	}
}
