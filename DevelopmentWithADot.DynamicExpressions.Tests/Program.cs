using System;
using System.Linq;

namespace DevelopmentWithADot.DynamicExpressions.Tests
{
	class Program
	{
		static void Main(String[] args)
		{
			var expression = DateTime.Now.ParseExpression<DateTime, Int32>("Day");
			var day = expression(DateTime.Now);
			var result = DateTime.Now.EvaluateExpression<DateTime, Int32>("Day");
			var month = DateTime.Now.EvaluateExpression("Month");
			var monthString = DateTime.Now.EvaluateExpression("Year.ToString()");
			var filtered = Enumerable.Range(0, 10).FilterByExpression("true").ToList();
		}
	}
}
