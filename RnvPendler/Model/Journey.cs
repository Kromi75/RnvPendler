using System;
using System.Collections.Generic;

namespace RnvPendler.Model
{
	public class Journey
	{
		public string Time { get; set; }

		public string ProjectedTime { get; set; }

		public string Label { get; set; }

		public string Icon { get; set; }

		public string Color { get; set; }

		public string PastRequestText { get; set; }

		public string Ticker { get; set; }

		public IList<Departure> ListOfDepartures { get; set; }

		public string LineLabel { get; set; }

		public string Direction { get; set; }

		//public string Time { get; set; }

		public string DifferenceTime { get; set; }

		public string TourId { get; set; }

		public string KindOfTour { get; set; }

		public string LineId { get; set; }

		public string Transportation { get; set; }

		public string Platform { get; set; }

		public string Status { get; set; }

		public string StatusNote { get; set; }
	}
}
