namespace Server.Interfaces
{
	public interface ICalculator
	{
		public IOperationResult Perform(string? operation, string? value1, string? value2);
		public IOperationResult Perform(string? operation, double value1, double value2);
	}
}
