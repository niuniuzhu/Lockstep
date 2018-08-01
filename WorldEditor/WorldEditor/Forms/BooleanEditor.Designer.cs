namespace WorldEditor.Forms
{
	partial class BooleanEditor
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
			this.checkBox = new System.Windows.Forms.CheckBox();
			this.textContainer.SuspendLayout();
			this.btnContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// textContainer
			// 
			this.textContainer.Controls.Add(this.checkBox);
			this.textContainer.Size = new System.Drawing.Size(34, 20);
			this.textContainer.Controls.SetChildIndex(this.checkBox, 0);
			this.textContainer.Controls.SetChildIndex(this.nameLabel, 0);
			// 
			// checkBox
			// 
			this.checkBox.AutoSize = true;
			this.checkBox.Location = new System.Drawing.Point(16, 3);
			this.checkBox.Name = "checkBox";
			this.checkBox.Size = new System.Drawing.Size(15, 14);
			this.checkBox.TabIndex = 4;
			this.checkBox.UseVisualStyleBackColor = true;
			// 
			// BooleanEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(160, 79);
			this.Name = "BooleanEditor";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "布尔值修改器";
			this.textContainer.ResumeLayout(false);
			this.textContainer.PerformLayout();
			this.btnContainer.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox checkBox;
	}
}