using System;
using System.Collections.Generic;

namespace RnvPendler.Model
{
	public class StationsResult
	{
		public string Name { get; set; }

		public IEnumerable<Station> Stations { get; set; }
	}
}
