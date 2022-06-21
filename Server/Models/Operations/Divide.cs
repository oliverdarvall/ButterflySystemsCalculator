using Server.Interfaces;

namespace Server.Operations
{
	/// <summary>
	/// Divide model performs division on two provided values
	/// </summary>
	public class Divide : IOperation
	{
		public IOperationResult Perform(double value1, double value2)
		{
			if (value2 == 0)
				return new InvalidResult()
				{
					Display = $"I am only a mortal calculator !",
				};

			double value = value1 / value2;

			return new SuccessResult($"{value1} / {value2} = {value}", value);
		}
	}
}
