namespace Server.Interfaces
{
	public interface IOperationResult
	{
		string Display { get; set; }
		bool Success { get; set; }
		double Value { get; set; }
	}
}
