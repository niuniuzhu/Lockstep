namespace WorldEditor.Forms
{
	partial class ValueEditor
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
			this.nameLabel = new System.Windows.Forms.Label();
			this.textContainer = new System.Windows.Forms.Panel();
			this.confirm = new System.Windows.Forms.Button();
			this.cancel = new System.Windows.Forms.Button();
			this.btnContainer = new System.Windows.Forms.Panel();
			this.descLable = new System.Windows.Forms.Label();
			this.textContainer.SuspendLayout();
			this.btnContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// nameLabel
			// 
			this.nameLabel.AutoSize = true;
			this.nameLabel.Location = new System.Drawing.Point(0, 6);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(17, 12);
			this.nameLabel.TabIndex = 2;
			this.nameLabel.Text = "N:";
			// 
			// textContainer
			// 
			this.textContainer.AutoSize = true;
			this.textContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.textContainer.Controls.Add(this.nameLabel);
			this.textContainer.Location = new System.Drawing.Point(11, 11);
			this.textContainer.MinimumSize = new System.Drawing.Size(180, 25);
			this.textContainer.Name = "textContainer";
			this.textContainer.Size = new System.Drawing.Size(180, 25);
			this.textContainer.TabIndex = 5;
			// 
			// confirm
			// 
			this.confirm.Location = new System.Drawing.Point(0, 0);
			this.confirm.Margin = new System.Windows.Forms.Padding(0);
			this.confirm.Name = "confirm";
			this.confirm.Size = new System.Drawing.Size(44, 23);
			this.confirm.TabIndex = 0;
			this.confirm.Text = "确定";
			this.confirm.UseVisualStyleBackColor = true;
			this.confirm.Click += new System.EventHandler(this.confirm_Click);
			// 
			// cancel
			// 
			this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancel.Location = new System.Drawing.Point(50, 0);
			this.cancel.Margin = new System.Windows.Forms.Padding(0);
			this.cancel.Name = "cancel";
			this.cancel.Size = new System.Drawing.Size(44, 23);
			this.cancel.TabIndex = 1;
			this.cancel.Text = "取消";
			this.cancel.UseVisualStyleBackColor = true;
			this.cancel.Click += new System.EventHandler(this.cancel_Click);
			// 
			// btnContainer
			// 
			this.btnContainer.AutoSize = true;
			this.btnContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnContainer.Controls.Add(this.confirm);
			this.btnContainer.Controls.Add(this.cancel);
			this.btnContainer.Location = new System.Drawing.Point(49, 72);
			this.btnContainer.Name = "btnContainer";
			this.btnContainer.Size = new System.Drawing.Size(94, 23);
			this.btnContainer.TabIndex = 4;
			// 
			// descLable
			// 
			this.descLable.AutoSize = true;
			this.descLable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.descLable.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.descLable.Location = new System.Drawing.Point(11, 44);
			this.descLable.Margin = new System.Windows.Forms.Padding(0);
			this.descLable.Name = "descLable";
			this.descLable.Padding = new System.Windows.Forms.Padding(2);
			this.descLable.Size = new System.Drawing.Size(6, 18);
			this.descLable.TabIndex = 6;
			// 
			// ValueEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancel;
			this.ClientSize = new System.Drawing.Size(203, 114);
			this.ControlBox = false;
			this.Controls.Add(this.descLable);
			this.Controls.Add(this.textContainer);
			this.Controls.Add(this.btnContainer);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "ValueEditor";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "ValueEditor";
			this.textContainer.ResumeLayout(false);
			this.textContainer.PerformLayout();
			this.btnContainer.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}


		protected System.Windows.Forms.Panel textContainer;
		protected System.Windows.Forms.Label nameLabel;
		protected System.Windows.Forms.Button confirm;
		protected System.Windows.Forms.Button cancel;
		protected System.Windows.Forms.Panel btnContainer;

		#endregion
		private System.Windows.Forms.Label descLable;
	}
}