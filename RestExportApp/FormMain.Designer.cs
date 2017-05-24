namespace RestExportApp {
	partial class FormMain {
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent() {
			this.btnGet = new System.Windows.Forms.Button();
			this.group01 = new System.Windows.Forms.GroupBox();
			this.btnExport = new System.Windows.Forms.Button();
			this.rdo03 = new System.Windows.Forms.RadioButton();
			this.rdo02 = new System.Windows.Forms.RadioButton();
			this.rdo01 = new System.Windows.Forms.RadioButton();
			this.grid01 = new System.Windows.Forms.DataGridView();
			this.group01.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grid01)).BeginInit();
			this.SuspendLayout();
			// 
			// btnGet
			// 
			this.btnGet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnGet.Location = new System.Drawing.Point(329, 38);
			this.btnGet.Name = "btnGet";
			this.btnGet.Size = new System.Drawing.Size(75, 23);
			this.btnGet.TabIndex = 0;
			this.btnGet.Text = "取得";
			this.btnGet.UseVisualStyleBackColor = true;
			this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
			// 
			// group01
			// 
			this.group01.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.group01.Controls.Add(this.btnExport);
			this.group01.Controls.Add(this.rdo03);
			this.group01.Controls.Add(this.rdo02);
			this.group01.Controls.Add(this.rdo01);
			this.group01.Controls.Add(this.btnGet);
			this.group01.Location = new System.Drawing.Point(12, 12);
			this.group01.Name = "group01";
			this.group01.Size = new System.Drawing.Size(410, 67);
			this.group01.TabIndex = 1;
			this.group01.TabStop = false;
			// 
			// btnExport
			// 
			this.btnExport.Location = new System.Drawing.Point(329, 11);
			this.btnExport.Name = "btnExport";
			this.btnExport.Size = new System.Drawing.Size(75, 23);
			this.btnExport.TabIndex = 4;
			this.btnExport.Text = "エクスポート";
			this.btnExport.UseVisualStyleBackColor = true;
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			// 
			// rdo03
			// 
			this.rdo03.AutoSize = true;
			this.rdo03.Location = new System.Drawing.Point(195, 17);
			this.rdo03.Name = "rdo03";
			this.rdo03.Size = new System.Drawing.Size(71, 16);
			this.rdo03.TabIndex = 3;
			this.rdo03.Text = "商品検索";
			this.rdo03.UseVisualStyleBackColor = true;
			// 
			// rdo02
			// 
			this.rdo02.AutoSize = true;
			this.rdo02.Location = new System.Drawing.Point(100, 18);
			this.rdo02.Name = "rdo02";
			this.rdo02.Size = new System.Drawing.Size(67, 16);
			this.rdo02.TabIndex = 2;
			this.rdo02.Text = "カテゴリー";
			this.rdo02.UseVisualStyleBackColor = true;
			// 
			// rdo01
			// 
			this.rdo01.AutoSize = true;
			this.rdo01.Checked = true;
			this.rdo01.Location = new System.Drawing.Point(6, 18);
			this.rdo01.Name = "rdo01";
			this.rdo01.Size = new System.Drawing.Size(59, 16);
			this.rdo01.TabIndex = 1;
			this.rdo01.TabStop = true;
			this.rdo01.Text = "降水量";
			this.rdo01.UseVisualStyleBackColor = true;
			// 
			// grid01
			// 
			this.grid01.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grid01.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grid01.Location = new System.Drawing.Point(12, 85);
			this.grid01.Name = "grid01";
			this.grid01.RowTemplate.Height = 21;
			this.grid01.Size = new System.Drawing.Size(410, 314);
			this.grid01.TabIndex = 2;
			// 
			// FormMain
			// 
			this.AcceptButton = this.btnGet;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(434, 411);
			this.Controls.Add(this.grid01);
			this.Controls.Add(this.group01);
			this.Name = "FormMain";
			this.Text = "REST エクスポート";
			this.group01.ResumeLayout(false);
			this.group01.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.grid01)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnGet;
		private System.Windows.Forms.GroupBox group01;
		private System.Windows.Forms.RadioButton rdo03;
		private System.Windows.Forms.RadioButton rdo02;
		private System.Windows.Forms.RadioButton rdo01;
		private System.Windows.Forms.DataGridView grid01;
		private System.Windows.Forms.Button btnExport;
	}
}

