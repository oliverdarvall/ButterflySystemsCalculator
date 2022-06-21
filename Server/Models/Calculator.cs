using System.Diagnostics;
using Server.Interfaces;
using Server.Operations;

namespace Server.Models
{
	/// <summary>
	/// Calculator class parses the query parameters and passes it along to the relevant Operation
	/// </summary>
	public class Calculator : ICalculator
	{
		static public IOperationResult InvalidResult { get; set; } = new InvalidResult();

		public IOperationResult Perform(string? operation, double value1, double value2)
		{
			if (string.IsNullOrWhiteSpace(operation))
				return InvalidResult;

			// In "normal" circumstances I would just return the mathematical result from the switch
			// However, the below would be more in line with what I would implement if the 
			// operations to be performed were lengthy and/or complex
			IOperation? op = operation.ToLower() switch
			{
				"add" => new Add(),
				"subtract" => new Subtract(),
				"multiply" => new Multiply(),
				"divide" => new Divide(),
				_ => null
			};

			return op?.Perform(value1, value2) ?? new InvalidResult();
		}

		public IOperationResult Perform(string? operation, string? value1, string? value2)
		{
			if (string.IsNullOrWhiteSpace(value1) || string.IsNullOrWhiteSpace(value2))
				return InvalidResult;

			if (double.TryParse(value1, out double d1) && double.TryParse(value2, out double d2))
				return Perform(operation, d1, d2);

			return InvalidResult;
		}
	}
}
