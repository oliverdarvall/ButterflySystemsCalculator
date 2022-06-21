using Server.Interfaces;

namespace Server.Operations
{
	/// <summary>
	/// Subtract model performs subtraction on two provided values
	/// </summary>
	public class Subtract : IOperation
	{
		public IOperationResult Perform(double value1, double value2)
		{
			double value = value1 - value2;

			return new SuccessResult($"{value1} - {value2} = {value}", value);
		}
	}
}
