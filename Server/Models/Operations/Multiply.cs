using Server.Interfaces;

namespace Server.Operations
{
	/// <summary>
	/// Multiply model performs multiplication on two provided values
	/// </summary>
	public class Multiply : IOperation
	{
		public IOperationResult Perform(double value1, double value2)
		{
			double value = value1 * value2;

			return new SuccessResult($"{value1} * {value2} = {value}", value);
		}
	}
}
