using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerTests
{
	internal class CalculatorTests
	{
		Calculator _calculator;

		[SetUp]
		public void Setup()
		{
			_calculator = new Calculator();
		}

		[Test]
		public void Add()
		{
			double value1 = 10, value2 = 2;

			var result = _calculator.Perform(nameof(Add), value1, value2);

			Assert.That(result.Success, Is.True);
			Assert.That(result.Value, Is.EqualTo(value1 + value2));

			result = _calculator.Perform(nameof(Add), value1.ToString(), value2.ToString());

			Assert.That(result.Success, Is.True);
			Assert.That(result.Value, Is.EqualTo(value1 + value2));
		}

		[Test]
		public void Subtract()
		{
			double value1 = 10, value2 = 2;

			var result = _calculator.Perform(nameof(Subtract), value1, value2);

			Assert.That(result.Success, Is.True);
			Assert.That(result.Value, Is.EqualTo(value1 - value2));

			result = _calculator.Perform(nameof(Subtract), value1.ToString(), value2.ToString());

			Assert.That(result.Success, Is.True);
			Assert.That(result.Value, Is.EqualTo(value1 - value2));
		}

		[Test]
		public void Multiply()
		{
			double value1 = 10, value2 = 2;

			var result = _calculator.Perform(nameof(Multiply), value1, value2);

			Assert.That(result.Success, Is.True);
			Assert.That(result.Value, Is.EqualTo(value1 * value2));

			result = _calculator.Perform(nameof(Multiply), value1.ToString(), value2.ToString());

			Assert.That(result.Success, Is.True);
			Assert.That(result.Value, Is.EqualTo(value1 * value2));
		}

		[Test]
		public void Divide()
		{
			double value1 = 10, value2 = 2;

			var result = _calculator.Perform(nameof(Divide), value1, value2);

			Assert.That(result.Success, Is.True);
			Assert.That(result.Value, Is.EqualTo(value1 / value2));

			result = _calculator.Perform(nameof(Divide), value1.ToString(), value2.ToString());

			Assert.That(result.Success, Is.True);
			Assert.That(result.Value, Is.EqualTo(value1 / value2));
		}

		[Test]
		public void Invalid()
		{
			var result = _calculator.Perform(null, null, null);
			Assert.That(result.Success, Is.False);

			result = _calculator.Perform(null, null, "value2");
			Assert.That(result.Success, Is.False);

			result = _calculator.Perform(null, "value1", "value2");
			Assert.That(result.Success, Is.False);

			result = _calculator.Perform("whatever", "1", "2");
			Assert.That(result.Success, Is.False);
		}
	}
}
