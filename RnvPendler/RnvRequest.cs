namespace RnvPendler
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Net.Http;
	using System.Net.Http.Headers;
	using System.Threading.Tasks;
	using Newtonsoft.Json;
	using RnvPendler.Model;

	public class RnvRequest
	{
		private const string Server = "http://rnv.the-agent-factory.de:8080";
		private const string Path = "/easygo2/api/regions/rnv/modules/";
		private const string ModuleStations = "stations/packages/1";
		private const string ModuleStationMonitor = "stationmonitor/element";

		private static readonly HttpClient Client = new HttpClient();

		static RnvRequest()
		{
			RnvRequest.InitializeClient();
		}

		public async Task<IList<Station>> FetchStations()
		{
			HttpResponseMessage response = await RnvRequest.Client.GetAsync($"{Server}{Path}{ModuleStations}");

			// Get the response content.
			HttpContent responseContent = response.Content;

			// Get the stream of the content.
			StationsResult result = null;
			using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
			{
				string resultString = await reader.ReadToEndAsync();
				Console.WriteLine(resultString);

				result = JsonConvert.DeserializeObject<StationsResult>(resultString);
			}

			return result?.Stations?.ToList();
		}

		public async Task<Journey> FetchDeparturesForStation(string hafasId)
		{
			HttpResponseMessage response = await RnvRequest.Client.GetAsync($"{Server}{Path}{ModuleStationMonitor}?hafasID={hafasId}&time=null");

			// Get the response content.
			HttpContent responseContent = response.Content;

			// Get the stream of the content.
			Journey result = null;
			using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
			{
				string resultString = await reader.ReadToEndAsync();
				Console.WriteLine(resultString);

				result = JsonConvert.DeserializeObject<Journey>(resultString);
			}

			return result;
		}

		private static void InitializeClient()
		{
			RnvRequest.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			RnvRequest.Client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("de"));
			RnvRequest.Client.DefaultRequestHeaders.Add("RNV_API_TOKEN", new[] { "kssbmqnbqb5krf4o23rgkg4uut" });
		}

		//static async void TestCall()
		//{
		//	curl 'http://rnv.the-agent-factory.de:8080/easygo2/rest/regions/rnv/modules/lines?hafasID=116&lineID=4&stopIndex=0&tourType=454AUS&tourID=3101812137&time=20%3A04' \
		//-H 'User-Agent: easy.GO Client Android v1.2.1 (Mozilla/5.0 (Linux; Android 4.4.4; Nexus 4 Build/KTU84Q) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/33.0.0.0 Mobile Safari/537.36)' \
		//-H 'Accept-Language: de' \
		//-H 'Accept: application/json'

		//HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, URI);

		//request.Headers.UserAgent.Add(new ProductInfoHeaderValue("Mozilla", "5.0"));

		//// Create the HttpContent for the form to be posted.
		//KeyValuePair<string, string>[] keyValuePair = new[] 
		//{
		//	new KeyValuePair<string, string>("hafasID", "116"),
		//	new KeyValuePair<string, string>("lineID", "4"),
		//	new KeyValuePair<string, string>("stopIndex", "0"),
		//	new KeyValuePair<string, string>("tourType", "0"),
		//	new KeyValuePair<string, string>("tourID", "3101812137"),
		//	new KeyValuePair<string, string>("time", "20%3A04")
		//};
		//var requestContent = new FormUrlEncodedContent(keyValuePair);

		// Get the response.
		//}
	}
}
