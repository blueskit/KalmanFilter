using System;
using System.Linq;

namespace FusionFiltering
{
	public class ProgramTest
	{
		/// <summary>
		/// kalman滤波测试1
		/// </summary>
		[System.Diagnostics.Conditional("DEBUG")]
		public static void TestKalmanFilter1()
		{
			Console.WriteLine("FilterKalman Usage");

			FilterKalman test = new FilterKalman(0.008, 0.1);
			double[] testData = { 66, 64, 63, 63, 63, 66, 65, 67, 58 };
			foreach (var x in testData) {
				Console.WriteLine("Input data: {0:#,##0.00}, Filtered data:{1:#,##0.000}", x, test.filter(x));
			}
		}

		/// <summary>
		/// Example Usage with controlled input
		/// </summary>
		[System.Diagnostics.Conditional("DEBUG")]
		public static void TestKalmanFilterWithControlled()
		{
			Console.WriteLine("FilterKalman Usage with controlled input");

			FilterKalman test = new FilterKalman(0.008, 0.1, 1, 1, 1);
			double[] testData = { 66, 64, 63, 63, 63, 66, 65, 67, 58 };
			double u = 0.2;
			foreach (var x in testData) {
				Console.WriteLine("Input data: {0:#,##0.00}, Filtered data:{1:#,##0.000}", x, test.filter(x, u));
			}

		}
	}
}
