using System;
using System.Collections.Generic;

namespace RnvPendler.Model
{
	/// <summary>
	/// Is used to serialize/deserialize the stations cache.
	/// </summary>
	public class StationsData
	{
		public DateTime Timestamp { get; set; }

		public IList<Station> Stations { get; set; }
	}
}
