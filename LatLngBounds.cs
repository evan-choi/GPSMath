namespace GPSMath
{
	public class LatLngBounds
	{
		public LatLng LatLng1 { get; set; }
		public LatLng LatLng2 { get; set; }

		public LatLngBounds(LatLng l1, LatLng l2)
		{
			this.LatLng1 = l1;
			this.LatLng2 = l2;
		}

		public LatLngBounds()
		{
		}
	}
}