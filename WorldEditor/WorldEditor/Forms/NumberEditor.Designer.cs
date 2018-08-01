namespace WorldEditor.Forms
{
	partial class NumberEditor
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
			this.input = new System.Windows.Forms.NumericUpDown();
			this.textContainer.SuspendLayout();
			this.btnContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.input)).BeginInit();
			this.SuspendLayout();
			// 
			// textContainer
			// 
			this.textContainer.Controls.Add(this.input);
			this.textContainer.Controls.SetChildIndex(this.nameLabel, 0);
			this.textContainer.Controls.SetChildIndex(this.input, 0);
			// 
			// input
			// 
			this.input.Location = new System.Drawing.Point(24, 0);
			this.input.Name = "input";
			this.input.Size = new System.Drawing.Size(120, 21);
			this.input.TabIndex = 3;
			// 
			// NumberEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(193, 81);
			this.Name = "NumberEditor";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Number Editor";
			this.textContainer.ResumeLayout(false);
			this.textContainer.PerformLayout();
			this.btnContainer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.input)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown input;
	}
}