namespace GPSMath
{
	public class LatLng
	{
		public double Latitude { get; set; }
		public double Longitude { get; set; }

		public LatLng(double lat, double lon)
		{
			this.Latitude = lat;
			this.Longitude = lon;
		}

		public LatLng()
		{
		}
	}
}
