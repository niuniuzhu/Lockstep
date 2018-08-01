namespace Controls
{
	partial class StringEditor
	{
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region 组件设计器生成的代码

		/// <summary> 
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.input = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// input
			// 
			this.input.Location = new System.Drawing.Point(0, 0);
			this.input.Margin = new System.Windows.Forms.Padding(0);
			this.input.Name = "input";
			this.input.Size = new System.Drawing.Size(140, 21);
			this.input.TabIndex = 0;
			this.input.Text = "value";
			// 
			// StringEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.input);
			this.Name = "StringEditor";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox input;
	}
}
