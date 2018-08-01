namespace WorldEditor.Forms
{
	partial class StringEditor
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
			this.input = new System.Windows.Forms.TextBox();
			this.textContainer.SuspendLayout();
			this.btnContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// textContainer
			// 
			this.textContainer.Controls.Add(this.input);
			this.textContainer.Size = new System.Drawing.Size(162, 24);
			this.textContainer.Controls.SetChildIndex(this.input, 0);
			this.textContainer.Controls.SetChildIndex(this.nameLabel, 0);
			// 
			// input
			// 
			this.input.Location = new System.Drawing.Point(19, 0);
			this.input.Name = "input";
			this.input.Size = new System.Drawing.Size(140, 21);
			this.input.TabIndex = 3;
			this.input.Text = "value";
			// 
			// StringEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(199, 90);
			this.Name = "StringEditor";
			this.Padding = new System.Windows.Forms.Padding(8);
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "文本修改器";
			this.textContainer.ResumeLayout(false);
			this.textContainer.PerformLayout();
			this.btnContainer.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox input;
	}
}