using System;
using System.Threading.Tasks;
using UIKit;
using RnvPendler.Model;

namespace RnvPendler
{
	public partial class ViewController : UIViewController
	{
		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public async override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Perform any additional setup after loading the view, typically from a nib.

			// LU Rathaus: 2096
			await this.ShowDepartures("2096");
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();

			// Release any cached data, images, etc that aren't in use.
			Console.WriteLine("DidReceiveMemoryWarning: Clearing stations cache.");
			StationsCache.ClearStationsCache();
		}

		private async Task ShowDepartures(string hafasId)
		{
			RnvRequest request = new RnvRequest();
			Journey journey = await request.FetchDeparturesForStation(hafasId);

			// Since data is fetched asynchronously, ReloadData() must be called after assigning the view source.
			// Otherwise, it's not going to be displayed.
			this.DepartureListTableView.Source = new DepartureListTableViewSource(journey.ListOfDepartures);
			this.DepartureListTableView.ReloadData();
		}
	}
}
