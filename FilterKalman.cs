namespace FusionFiltering
{
	/// <summary>
	/// Simple implementation of the Kalman Filter for 1D data.
	/// Originally written in JavaScript by Wouter Bulten
	/// 
	/// https://github.com/wouterbulten/kalmanjs/blob/master/contrib/java/KalmanFilter.java
	/// </summary>
	public class FilterKalman
	{
		private double A = 1;
		private double B = 0;
		private double C = 1;

		private double R;
		private double Q;

		private double cov = double.NaN;
		private double x = double.NaN;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="R">R Process noise</param>
		/// <param name="Q">Q Measurement noise</param>
		/// <param name="A">A State vector</param>
		/// <param name="B">B Control vector</param>
		/// <param name="C">C Measurement vector</param>
		public FilterKalman(double R, double Q, double A, double B, double C)
		{
			this.R = R;
			this.Q = Q;

			this.A = A;
			this.B = B;
			this.C = C;

			this.cov = double.NaN;
			this.x = double.NaN; // estimated signal without noise
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="R">R Process noise</param>
		/// <param name="Q">Q Measurement noise</param>
		public FilterKalman(double R, double Q)
		{
			this.R = R;
			this.Q = Q;
		}

		/// <summary>
		/// Filters a measurement
		/// </summary>
		/// <param name="measurement">The measurement value to be filtered</param>
		/// <param name="u">The controlled input value</param>
		/// <returns>The filtered value</returns>
		public double filter(double measurement, double u)
		{
			if (double.IsNaN(this.x)) {
				this.x = (1 / this.C) * measurement;
				this.cov = (1 / this.C) * this.Q * (1 / this.C);
			} else {
				double predX = (this.A * this.x) + (this.B * u);
				double predCov = ((this.A * this.cov) * this.A) + this.R;

				// Kalman gain
				double K = predCov * this.C * (1 / ((this.C * predCov * this.C) + this.Q));

				// Correction
				this.x = predX + K * (measurement - (this.C * predX));
				this.cov = predCov - (K * this.C * predCov);
			}
			return this.x;
		}

		/// <summary>
		/// Filters a measurement
		/// </summary>
		/// <param name="measurement">The measurement value to be filtered</param>
		/// <returns>The filtered value</returns>
		public double filter(double measurement)
		{
			double u = 0;
			if (double.IsNaN(this.x)) {
				this.x = (1 / this.C) * measurement;
				this.cov = (1 / this.C) * this.Q * (1 / this.C);
			} else {
				double predX = (this.A * this.x) + (this.B * u);
				double predCov = ((this.A * this.cov) * this.A) + this.R;

				// Kalman gain
				double K = predCov * this.C * (1 / ((this.C * predCov * this.C) + this.Q));

				// Correction
				this.x = predX + K * (measurement - (this.C * predX));
				this.cov = predCov - (K * this.C * predCov);
			}
			return this.x;
		}

		/// <summary>
		/// Set the last measurement.
		/// </summary>
		/// <returns>return The last measurement fed into the filter</returns>
		public double lastMeasurement()
		{
			return this.x;
		}

		/// <summary>
		/// Sets measurement noise
		/// </summary>
		/// <param name="noise">The new measurement noise</param>
		public void setMeasurementNoise(double noise)
		{
			this.Q = noise;
		}

		/// <summary>
		/// Sets process noise
		/// </summary>
		/// <param name="noise">The new process noise</param>
		public void setProcessNoise(double noise)
		{
			this.R = noise;
		}
	}
}
