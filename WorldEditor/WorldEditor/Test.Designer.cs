namespace WorldEditor
{
	partial class Test
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.listview = new GlacialComponents.Controls.GlacialList();
			this.SuspendLayout();
			// 
			// listview
			// 
			this.listview.AllowColumnResize = true;
			this.listview.AllowMultiselect = false;
			this.listview.AlternateBackground = System.Drawing.Color.DarkGreen;
			this.listview.AlternatingColors = false;
			this.listview.AutoHeight = false;
			this.listview.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.listview.BackgroundStretchToFit = true;
			this.listview.ControlStyle = GlacialComponents.Controls.GLControlStyles.Normal;
			this.listview.FullRowSelect = true;
			this.listview.GridColor = System.Drawing.Color.LightGray;
			this.listview.GridLines = GlacialComponents.Controls.GLGridLines.gridBoth;
			this.listview.GridLineStyle = GlacialComponents.Controls.GLGridLineStyles.gridSolid;
			this.listview.GridTypes = GlacialComponents.Controls.GLGridTypes.gridOnExists;
			this.listview.HeaderHeight = 22;
			this.listview.HeaderVisible = true;
			this.listview.HeaderWordWrap = false;
			this.listview.HotColumnTracking = false;
			this.listview.HotItemTracking = false;
			this.listview.HotTrackingColor = System.Drawing.Color.LightGray;
			this.listview.HoverEvents = false;
			this.listview.HoverTime = 1;
			this.listview.ImageList = null;
			this.listview.ItemHeight = 25;
			this.listview.ItemWordWrap = false;
			this.listview.Location = new System.Drawing.Point(12, 12);
			this.listview.Name = "listview";
			this.listview.Selectable = true;
			this.listview.SelectedTextColor = System.Drawing.Color.White;
			this.listview.SelectionColor = System.Drawing.Color.CornflowerBlue;
			this.listview.ShowBorder = true;
			this.listview.ShowFocusRect = false;
			this.listview.Size = new System.Drawing.Size(651, 341);
			this.listview.SortType = GlacialComponents.Controls.SortTypes.InsertionSort;
			this.listview.SuperFlatHeaderColor = System.Drawing.Color.White;
			this.listview.TabIndex = 0;
			this.listview.Text = "listview";
			// 
			// Test
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(748, 406);
			this.Controls.Add(this.listview);
			this.Name = "Test";
			this.Text = "Test";
			this.ResumeLayout(false);

		}

		#endregion

		private GlacialComponents.Controls.GlacialList listview;


	}
}