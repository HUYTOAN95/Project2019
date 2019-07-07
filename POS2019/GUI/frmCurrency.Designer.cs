namespace POS2019.GUI
{
    partial class frmCurrency
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.DateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrencyCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Buy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Transfer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sell = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DateTime,
            this.CurrencyCode,
            this.Buy,
            this.Transfer,
            this.Sell});
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(5, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(582, 269);
            this.dataGridView1.TabIndex = 0;
            // 
            // DateTime
            // 
            this.DateTime.DataPropertyName = "DateTime";
            this.DateTime.HeaderText = "Ngày ";
            this.DateTime.Name = "DateTime";
            this.DateTime.Width = 120;
            // 
            // CurrencyCode
            // 
            this.CurrencyCode.DataPropertyName = "CurrencyCode";
            this.CurrencyCode.HeaderText = "Đơn vị";
            this.CurrencyCode.Name = "CurrencyCode";
            this.CurrencyCode.Width = 120;
            // 
            // Buy
            // 
            this.Buy.DataPropertyName = "Buy";
            this.Buy.HeaderText = "TG Mua vào";
            this.Buy.Name = "Buy";
            // 
            // Transfer
            // 
            this.Transfer.DataPropertyName = "Transfer";
            this.Transfer.HeaderText = "TG Giao dịch";
            this.Transfer.Name = "Transfer";
            // 
            // Sell
            // 
            this.Sell.DataPropertyName = "Sell";
            this.Sell.HeaderText = "TG Bán ra";
            this.Sell.Name = "Sell";
            // 
            // frmCurrency
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 309);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmCurrency";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tỷ giá tiền tệ";
            this.Load += new System.EventHandler(this.frmCurrency_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrencyCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Buy;
        private System.Windows.Forms.DataGridViewTextBoxColumn Transfer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sell;
    }
}