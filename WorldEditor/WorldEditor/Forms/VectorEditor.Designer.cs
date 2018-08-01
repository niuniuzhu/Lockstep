namespace WorldEditor.Forms
{
	partial class VectorEditor
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
			this.valueContainer = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.lenLabel = new System.Windows.Forms.Label();
			this.norBtn = new System.Windows.Forms.Button();
			this.xlabel = new System.Windows.Forms.Label();
			this.xvalue = new System.Windows.Forms.NumericUpDown();
			this.ylabel = new System.Windows.Forms.Label();
			this.yvalue = new System.Windows.Forms.NumericUpDown();
			this.zlabel = new System.Windows.Forms.Label();
			this.zvalue = new System.Windows.Forms.NumericUpDown();
			this.wlabel = new System.Windows.Forms.Label();
			this.wvalue = new System.Windows.Forms.NumericUpDown();
			this.textContainer.SuspendLayout();
			this.btnContainer.SuspendLayout();
			this.valueContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.xvalue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.yvalue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.zvalue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.wvalue)).BeginInit();
			this.SuspendLayout();
			// 
			// textContainer
			// 
			this.textContainer.Controls.Add(this.valueContainer);
			this.textContainer.Size = new System.Drawing.Size(295, 60);
			this.textContainer.Controls.SetChildIndex(this.nameLabel, 0);
			this.textContainer.Controls.SetChildIndex(this.valueContainer, 0);
			// 
			// valueContainer
			// 
			this.valueContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.valueContainer.Controls.Add(this.label1);
			this.valueContainer.Controls.Add(this.lenLabel);
			this.valueContainer.Controls.Add(this.norBtn);
			this.valueContainer.Controls.Add(this.xlabel);
			this.valueContainer.Controls.Add(this.xvalue);
			this.valueContainer.Controls.Add(this.ylabel);
			this.valueContainer.Controls.Add(this.yvalue);
			this.valueContainer.Controls.Add(this.zlabel);
			this.valueContainer.Controls.Add(this.zvalue);
			this.valueContainer.Controls.Add(this.wlabel);
			this.valueContainer.Controls.Add(this.wvalue);
			this.valueContainer.Location = new System.Drawing.Point(24, 0);
			this.valueContainer.Name = "valueContainer";
			this.valueContainer.Size = new System.Drawing.Size(268, 57);
			this.valueContainer.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(50, 37);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 12);
			this.label1.TabIndex = 9;
			this.label1.Text = "长度:";
			// 
			// lenLabel
			// 
			this.lenLabel.AutoSize = true;
			this.lenLabel.Location = new System.Drawing.Point(86, 37);
			this.lenLabel.Name = "lenLabel";
			this.lenLabel.Size = new System.Drawing.Size(0, 12);
			this.lenLabel.TabIndex = 8;
			// 
			// norBtn
			// 
			this.norBtn.Location = new System.Drawing.Point(0, 31);
			this.norBtn.Name = "norBtn";
			this.norBtn.Size = new System.Drawing.Size(43, 23);
			this.norBtn.TabIndex = 4;
			this.norBtn.Text = "归一";
			this.norBtn.UseVisualStyleBackColor = true;
			this.norBtn.Click += new System.EventHandler(this.norBtn_Click);
			// 
			// xlabel
			// 
			this.xlabel.AutoSize = true;
			this.xlabel.Location = new System.Drawing.Point(3, 6);
			this.xlabel.Name = "xlabel";
			this.xlabel.Size = new System.Drawing.Size(17, 12);
			this.xlabel.TabIndex = 0;
			this.xlabel.Text = "x:";
			// 
			// xvalue
			// 
			this.xvalue.Location = new System.Drawing.Point(20, 3);
			this.xvalue.Name = "xvalue";
			this.xvalue.Size = new System.Drawing.Size(44, 21);
			this.xvalue.TabIndex = 1;
			this.xvalue.ValueChanged += new System.EventHandler(this.ValueChanged);
			// 
			// ylabel
			// 
			this.ylabel.AutoSize = true;
			this.ylabel.Location = new System.Drawing.Point(70, 6);
			this.ylabel.Name = "ylabel";
			this.ylabel.Size = new System.Drawing.Size(17, 12);
			this.ylabel.TabIndex = 2;
			this.ylabel.Text = "y:";
			// 
			// yvalue
			// 
			this.yvalue.Location = new System.Drawing.Point(87, 3);
			this.yvalue.Name = "yvalue";
			this.yvalue.Size = new System.Drawing.Size(44, 21);
			this.yvalue.TabIndex = 3;
			this.yvalue.ValueChanged += new System.EventHandler(this.ValueChanged);
			// 
			// zlabel
			// 
			this.zlabel.AutoSize = true;
			this.zlabel.Location = new System.Drawing.Point(137, 6);
			this.zlabel.Name = "zlabel";
			this.zlabel.Size = new System.Drawing.Size(17, 12);
			this.zlabel.TabIndex = 4;
			this.zlabel.Text = "z:";
			// 
			// zvalue
			// 
			this.zvalue.Location = new System.Drawing.Point(154, 3);
			this.zvalue.Name = "zvalue";
			this.zvalue.Size = new System.Drawing.Size(44, 21);
			this.zvalue.TabIndex = 5;
			this.zvalue.ValueChanged += new System.EventHandler(this.ValueChanged);
			// 
			// wlabel
			// 
			this.wlabel.AutoSize = true;
			this.wlabel.Location = new System.Drawing.Point(204, 6);
			this.wlabel.Name = "wlabel";
			this.wlabel.Size = new System.Drawing.Size(17, 12);
			this.wlabel.TabIndex = 6;
			this.wlabel.Text = "w:";
			// 
			// wvalue
			// 
			this.wvalue.Location = new System.Drawing.Point(221, 3);
			this.wvalue.Name = "wvalue";
			this.wvalue.Size = new System.Drawing.Size(44, 21);
			this.wvalue.TabIndex = 7;
			this.wvalue.ValueChanged += new System.EventHandler(this.ValueChanged);
			// 
			// VectorEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(319, 114);
			this.Name = "VectorEditor";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Vector Editor";
			this.textContainer.ResumeLayout(false);
			this.textContainer.PerformLayout();
			this.btnContainer.ResumeLayout(false);
			this.valueContainer.ResumeLayout(false);
			this.valueContainer.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.xvalue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.yvalue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.zvalue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.wvalue)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel valueContainer;
		private System.Windows.Forms.Label xlabel;
		private System.Windows.Forms.NumericUpDown xvalue;
		private System.Windows.Forms.Label ylabel;
		private System.Windows.Forms.NumericUpDown yvalue;
		private System.Windows.Forms.Label zlabel;
		private System.Windows.Forms.NumericUpDown zvalue;
		private System.Windows.Forms.Label wlabel;
		private System.Windows.Forms.NumericUpDown wvalue;
		private System.Windows.Forms.Button norBtn;
		private System.Windows.Forms.Label lenLabel;
		private System.Windows.Forms.Label label1;
	}
}