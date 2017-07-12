using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RnvPendler.Model;

namespace RnvPendler
{
	public static class StationsCache
	{
		private static readonly string cachePath;

		public static ICollection<Station> Stations { get; private set; }

		static StationsCache()
		{
			string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			StationsCache.cachePath = Path.Combine(documents, "..", "Library", "Caches", "Stations.json");
		}

		public static async Task PopulateStationsCache()
		{
			if (StationsCache.Stations != null)
			{
				return;
			}

			if (File.Exists(StationsCache.cachePath))
			{
				IList<Station> stationsFromFile = FetchStationsFromFile();
				StationsCache.Stations = stationsFromFile;
			}

			if (StationsCache.Stations != null)
			{
				Console.WriteLine("Populated stations cache from file system.");
				return;
			}

			IList<Station> stationsFromServer = await FetchStationsFromServer();
			StationsCache.Stations = new Collection<Station>(stationsFromServer);
			Console.WriteLine("Populated stations cache from web service.");

			// TODO: Write file cache asynchronously?
			StationsCache.WriteFileCache(stationsFromServer);
		}

		public static void ClearStationsCache()
		{
			StationsCache.Stations = null;
		}

		private static async Task<IList<Station>> FetchStationsFromServer()
		{
			RnvRequest request = new RnvRequest();
			IList<Station> stations = await request.FetchStations();
			return stations;
		}

		private static IList<Station> FetchStationsFromFile()
		{
			// TODO: Check timestamp and decide whether the station list has to be refreshed.
			string stationsJson = File.ReadAllText(StationsCache.cachePath);
			StationsData stationsData = null;
			try
			{
				stationsData = JsonConvert.DeserializeObject<StationsData>(stationsJson);
			}
			catch (Exception)
			{
				Console.WriteLine("Unable to deserialize file cache.");
				File.Delete(StationsCache.cachePath);
			}

			return stationsData?.Stations;
		}

		private static void WriteFileCache(IList<Station> stations)
		{
			StationsData data = new StationsData()
			{
				Timestamp = DateTime.Now,
				Stations = stations
			};
			string stationsJson = JsonConvert.SerializeObject(data);
			File.WriteAllText(StationsCache.cachePath, stationsJson);
		}
	}
}
