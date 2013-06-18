using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.UI;

namespace DevelopmentWithADot.DynamicExpressions
{
	public static class Extensions
	{
		public static Func<TType, TResult> ParseExpression<TType, TResult>(this TType source, String expression, params Object [] values)
		{
			//public static Expression<Func<T, S>> DynamicExpression.ParseLambda<T, S>(string expression, params object[] values);
			Assembly asm = typeof(UpdatePanel).Assembly;
			Type dynamicExpressionType = asm.GetType("System.Web.Query.Dynamic.DynamicExpression");
			MethodInfo parseLambdaMethod = dynamicExpressionType.GetMethods(BindingFlags.Public | BindingFlags.Static).Where(m => (m.Name == "ParseLambda") && (m.GetParameters().Length == 2)).Single().MakeGenericMethod(typeof(TType), typeof(TResult));
			Expression<Func<TType, TResult>> accessor = parseLambdaMethod.Invoke(null, new Object[] { expression, values }) as Expression<Func<TType, TResult>>;
			
			return (accessor.Compile());
		}

		public static TResult EvaluateExpression<TType, TResult>(this TType source, String expression, params Object[] values)
		{
			return (ParseExpression<TType, TResult>(source, expression, values)(source));
		}

		public static Object EvaluateExpression<TType>(this TType source, String query, params Object[] values)
		{
			return (ParseExpression<TType, Object>(source, query, values)(source));
		}

		public static IEnumerable<TType> FilterByExpression<TType>(this IEnumerable<TType> source, String expression, params Object[] values)
		{
			Func<TType, Boolean> filter = ParseExpression<TType, Boolean>(default(TType), expression, values);

			return (source.Where(filter));
		}
	}
}
