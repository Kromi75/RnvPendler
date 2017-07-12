using System;

namespace RnvPendler.Model
{
	public class Station
	{
		public string LongName { get; set; }

		public string ShortName { get; set; }

		public double Longitude { get; set; }

		public double Latitude { get; set; }

		public string HafasId { get; set; }

		public int ElementId { get; set; }
	}
}
