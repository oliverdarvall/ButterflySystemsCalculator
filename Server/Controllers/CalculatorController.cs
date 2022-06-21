using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;
using Server.Models;

namespace Server.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class CalculatorController : ControllerBase
	{
		private readonly ICalculator _calculator;
		private readonly ILogger<CalculatorController> _logger;

		public CalculatorController(ILogger<CalculatorController> logger, ICalculator calculator)
		{
			_logger = logger;
			_calculator = calculator;
		}

		// Accept all combinations of query values in request
		[Route("")]
		[Route("{operation}")]
		[Route("{operation}/{value1}")]
		[Route("{operation}/{value1}/{value2}")]
		[HttpGet]
		public IOperationResult Get(string? operation, string? value1, string? value2)
		{
			_logger.Log(LogLevel.Information, "{controller}::Get - operation={operation} , value1={value1} , value2={value2}", nameof(CalculatorController), operation, value1, value2);

			try
			{
				return _calculator.Perform(operation, value1, value2);
			}
			catch (Exception ex)
			{
				_logger.Log(LogLevel.Critical, "{controller}::Exception - {exception}", nameof(CalculatorController), ex.Message);
			}

			return Calculator.InvalidResult;
		}
	}
}