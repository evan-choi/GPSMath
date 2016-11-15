using System;

namespace GPSMath
{
	//https://github.com/SteaI/GPSMath
	public static class GPSMath
	{
		const double Degree = 180d / Math.PI;
		const double Radian = Math.PI / 180d;

		public const double E = 0.016710219d;
		public const double EquatorRadius = 6378137d;
		public const double PoleRadius = EquatorRadius - EquatorRadius * E;
		public const double Round = 40077000d;

		public const decimal Micro = 360m / 60m / 60m / 1000m;
		const double SecAngle = 360d / 60d / 60d;

		// * Longtitude per 1°
		//Cos(Latitude) * 2 * PI * Radius / 360

		// * Latitude per 1°
		//EarthRound / 360

		public static double Bearing(this LatLng ll1, LatLng ll2)
		{
			double fi1 = ll1.Latitude.ToRadian();
			double fi2 = ll2.Latitude.ToRadian();
			double longDiff = (ll2.Longitude - ll1.Longitude).ToRadian();
			double y = Math.Sin(longDiff) * Math.Cos(fi2);
			double x = Math.Cos(fi1) * Math.Sin(fi2) - Math.Sin(fi1) * Math.Cos(fi2) * Math.Cos(longDiff);

			return ((Math.Atan2(y, x)).ToDegree() + 360) % 360;
		}

		public static LatLngBounds Bound(this LatLng ll1, LatLng ll2)
		{
			double minLat = Math.Min(ll1.Latitude, ll2.Latitude);
			double minLon = Math.Min(ll1.Longitude, ll2.Longitude);
			double maxLat = Math.Max(ll1.Latitude, ll2.Latitude);
			double maxLon = Math.Max(ll1.Longitude, ll2.Longitude);

			return new LatLngBounds(new LatLng(minLat, minLon), new LatLng(maxLat, maxLon));
		}

		public static LatLng ToIndex(this LatLng ll)
		{
			return new LatLng(
				(double)Math.Floor((decimal)ll.Latitude / Micro),
				(double)Math.Floor((decimal)ll.Longitude / Micro));
		}

		public static double Radius(this LatLng p1)
		{
			double fi = p1.Latitude.ToRadian();

			double a1 = Math.Pow(Math.Pow(EquatorRadius, 2) * Math.Cos(fi), 2);
			double a2 = Math.Pow(Math.Pow(PoleRadius, 2) * Math.Sin(fi), 2);

			double b1 = Math.Pow(EquatorRadius * Math.Cos(fi), 2);
			double b2 = Math.Pow(PoleRadius * Math.Sin(fi), 2);

			return Math.Sqrt((a1 + a2) / (b1 + b2));
		}

		public static double DistanceCeiling(this LatLng ll1, LatLng ll2, int mDistance)
		{
			double distance = ll1.Distance(ll2);

			return Math.Ceiling(distance / mDistance) * mDistance;
		}

		public static string ToDistanceString(this double mValue)
		{
			if (mValue < 1)
				return $"{mValue * 100:0.##}cm";

			if (mValue < 1000)
				return $"{mValue:0.##}m";

			return $"{mValue / 1000:0.##}km";
		}

		// millimeter
		public static double Distance(this LatLng ll1, LatLng ll2)
		{
			double radius = ll1.MidPoint(ll2).Radius();

			double p1 = ll1.Latitude.ToRadian();
			double l1 = ll1.Longitude.ToRadian();

			double p2 = ll2.Latitude.ToRadian();
			double l2 = ll2.Longitude.ToRadian();

			double pDelta = p2 - p1;
			double lDelta = l2 - l1;

			double a = Math.Pow(Math.Sin(pDelta / 2), 2)
						   + Math.Cos(p1) * Math.Cos(p2)
						   * Math.Pow(Math.Sin(lDelta / 2), 2);
			double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

			return radius * c;
		}

		public static LatLng MidPoint(this LatLng ll1, LatLng ll2)
		{
			double p1 = ll1.Latitude.ToRadian();
			double l1 = ll1.Longitude.ToRadian();

			double p2 = ll2.Latitude.ToRadian();
			double lDelta = (ll2.Longitude - ll1.Longitude).ToRadian();

			double Bx = Math.Cos(p2) * Math.Cos(lDelta);
			double By = Math.Cos(p2) * Math.Sin(lDelta);

			double x = Math.Sqrt((Math.Cos(p1) + Bx) * (Math.Cos(p1) + Bx) + Math.Pow(By, 2));
			double y = Math.Sin(p1) + Math.Sin(p2);

			double p3 = Math.Atan2(y, x);
			double l3 = l1 + Math.Atan2(By, Math.Cos(p1) + Bx);

			return new LatLng(
				p3.ToDegree(),
				(l3.ToDegree() + 540) % 360 - 180);
		}

		public static double ToDegree(this double value)
		{
			return value * Degree;
		}

		public static double ToRadian(this double value)
		{
			return value * Radian;
		}
	}
}
