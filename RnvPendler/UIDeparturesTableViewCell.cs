using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace RnvPendler
{
	public class UIDeparturesTableViewCell : UITableViewCell
	{
		private readonly UILabel lineIdLabel;

		private readonly UILabel directionLabel;

		private readonly UILabel timeLabel;

		public UIDeparturesTableViewCell(string cellId)
			: base(UITableViewCellStyle.Default, cellId)
		{
			Console.WriteLine("Create new UIDeparturesTableViewCell");
			this.lineIdLabel = new UILabel();
			this.directionLabel = new UILabel();
			this.timeLabel = new UILabel();

			this.ContentView.AddSubviews(new UIView[] { this.lineIdLabel, this.directionLabel, this.timeLabel });
		}

		public void UpdateCell(string lineId, string direction, string time)
		{
			this.lineIdLabel.Text = lineId;
			this.directionLabel.Text = direction;
			this.timeLabel.Text = String.Equals(time, "0", StringComparison.Ordinal) ? "sofort" : $"in {time} min";
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
			Console.WriteLine($"LayoutSubviews: Width: {ContentView.Bounds.Width}");
			nfloat cellHeight = ContentView.Bounds.Height - 10;
			nfloat lineLabelWidth = 40;
			nfloat timeLabelWidth = 80;
			nfloat margin = 5;

			this.lineIdLabel.Frame = new CGRect(margin, 5, lineLabelWidth, cellHeight);
			this.directionLabel.Frame = new CGRect(lineLabelWidth, 5, ContentView.Bounds.Width - lineLabelWidth - timeLabelWidth, cellHeight);
			this.timeLabel.Frame = new CGRect(ContentView.Bounds.Width - timeLabelWidth + margin, 5, timeLabelWidth, cellHeight);
		}
	}
}
