
using Microsoft.Extensions.Logging;

namespace ServerTests
{
	internal class ControllerTests
	{

		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void CalculatorController()
		{
			var logger = new LoggerFactory().CreateLogger<CalculatorController>();
			var calculator = new Calculator();

			CalculatorController controller = new (logger, calculator);

			var result1 = controller.Get(null, null, null);
			Assert.That(result1.GetType(), Is.EqualTo(typeof(InvalidResult)));

			var result2 = controller.Get(null, null, "1");
			Assert.That(result2.GetType(), Is.EqualTo(typeof(InvalidResult)));

			var result3 = controller.Get(null, "2", "1");
			Assert.That(result3.GetType(), Is.EqualTo(typeof(InvalidResult)));

			var result4 = controller.Get("whatever", "2", "1");
			Assert.That(result4.GetType(), Is.EqualTo(typeof(InvalidResult)));

			var result5 = controller.Get(nameof(Add), "2", "1");
			Assert.That(result5.GetType(), !Is.EqualTo(typeof(InvalidResult)));

			var result6 = controller.Get(nameof(Subtract), "2", "1");
			Assert.That(result6.GetType(), !Is.EqualTo(typeof(InvalidResult)));

			var result7 = controller.Get(nameof(Multiply), "2", "1");
			Assert.That(result7.GetType(), !Is.EqualTo(typeof(InvalidResult)));

			var result8 = controller.Get(nameof(Divide), "2", "1");
			Assert.That(result8.GetType(), !Is.EqualTo(typeof(InvalidResult)));
		}
	}
}