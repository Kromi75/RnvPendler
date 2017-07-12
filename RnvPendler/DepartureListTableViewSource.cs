using System;
using System.Collections.Generic;
using Foundation;
using RnvPendler.Model;
using UIKit;

namespace RnvPendler
{
	internal class DepartureListTableViewSource : UITableViewSource
	{
		private readonly IList<Departure> tableItems;
		private const string nameCellIdentifier = "TableCell";

		public DepartureListTableViewSource(IList<Departure> departures)
		{
			this.tableItems = departures;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			UIDeparturesTableViewCell cell = tableView.DequeueReusableCell(nameCellIdentifier) as UIDeparturesTableViewCell;
			if (cell == null)
			{
				cell = new UIDeparturesTableViewCell(nameCellIdentifier);
			}

			Departure departure = this.tableItems[indexPath.Row];
			cell.UpdateCell(departure.LineLabel, departure.Direction, departure.DifferenceTime);

			return cell;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return this.tableItems.Count;
		}
	}
}