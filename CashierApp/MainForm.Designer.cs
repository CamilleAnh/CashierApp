using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System;

namespace CashierApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem quảnLýKhoHàngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thanhToánToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem đăngXuấtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thoátToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.FlowLayoutPanel productPanel;
        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.DataGridView invoiceGrid;
        private System.Windows.Forms.Label totalLabel;
        private System.Windows.Forms.Button payButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeDataGridView()
        {
            // Xóa cột cũ nếu có
            invoiceGrid.Columns.Clear();
            this.invoiceGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.invoiceGrid_CellContentClick);
            // Thêm các cột mới
            invoiceGrid.Columns.Add("ProductName", "Product Name");
            invoiceGrid.Columns.Add("UnitPrice", "Unit Price");
            invoiceGrid.Columns.Add("Quantity", "Quantity");
            invoiceGrid.Columns.Add("Total", "Total");

            // Thêm cột điều khiển
            DataGridViewButtonColumn increaseButtonColumn = new DataGridViewButtonColumn
            {
                Name = "IncreaseButton",
                HeaderText = "Increase",
                Text = "+",
                UseColumnTextForButtonValue = true
            };
            invoiceGrid.Columns.Add(increaseButtonColumn);

            DataGridViewButtonColumn decreaseButtonColumn = new DataGridViewButtonColumn
            {
                Name = "DecreaseButton",
                HeaderText = "Decrease",
                Text = "-",
                UseColumnTextForButtonValue = true
            };
            invoiceGrid.Columns.Add(decreaseButtonColumn);

            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn
            {
                Name = "DeleteButton",
                HeaderText = "Delete",
                Text = "X",
                UseColumnTextForButtonValue = true
            };
            invoiceGrid.Columns.Add(deleteButtonColumn);

            // Thiết lập định dạng cho các cột nếu cần
            invoiceGrid.Columns["Total"].DefaultCellStyle.Format = "C2"; // Định dạng tiền tệ
        }

        
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.quảnLýTàiKhoảnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quảnLýKhoHàngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thanhToánToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hóaĐơnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.đăngXuấtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thoátToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.productPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.searchButton = new System.Windows.Forms.Button();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.invoiceGrid = new System.Windows.Forms.DataGridView();
            this.payButton = new System.Windows.Forms.Button();
            this.totalLabel = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.leftPanel.SuspendLayout();
            this.rightPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.invoiceGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quảnLýTàiKhoảnToolStripMenuItem,
            this.quảnLýKhoHàngToolStripMenuItem,
            this.thanhToánToolStripMenuItem,
            this.hóaĐơnToolStripMenuItem,
            this.đăngXuấtToolStripMenuItem,
            this.thoátToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // quảnLýTàiKhoảnToolStripMenuItem
            // 
            this.quảnLýTàiKhoảnToolStripMenuItem.Name = "quảnLýTàiKhoảnToolStripMenuItem";
            this.quảnLýTàiKhoảnToolStripMenuItem.Size = new System.Drawing.Size(116, 20);
            this.quảnLýTàiKhoảnToolStripMenuItem.Text = "Quản Lý Tài Khoản";
            this.quảnLýTàiKhoảnToolStripMenuItem.Click += new System.EventHandler(this.quảnLýTàiKhoảnToolStripMenuItem_Click);
            // 
            // quảnLýKhoHàngToolStripMenuItem
            // 
            this.quảnLýKhoHàngToolStripMenuItem.Name = "quảnLýKhoHàngToolStripMenuItem";
            this.quảnLýKhoHàngToolStripMenuItem.Size = new System.Drawing.Size(113, 20);
            this.quảnLýKhoHàngToolStripMenuItem.Text = "Quản lý kho hàng";
            this.quảnLýKhoHàngToolStripMenuItem.Click += new System.EventHandler(this.quảnLýKhoHàngToolStripMenuItem_Click);
            // 
            // thanhToánToolStripMenuItem
            // 
            this.thanhToánToolStripMenuItem.Name = "thanhToánToolStripMenuItem";
            this.thanhToánToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.thanhToánToolStripMenuItem.Text = "Thanh toán";
            this.thanhToánToolStripMenuItem.Click += new System.EventHandler(this.thanhToánToolStripMenuItem_Click);
            // 
            // hóaĐơnToolStripMenuItem
            // 
            this.hóaĐơnToolStripMenuItem.Name = "hóaĐơnToolStripMenuItem";
            this.hóaĐơnToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.hóaĐơnToolStripMenuItem.Text = "Hóa Đơn";
            this.hóaĐơnToolStripMenuItem.Click += new System.EventHandler(this.hóaĐơnToolStripMenuItem_Click);
            // 
            // đăngXuấtToolStripMenuItem
            // 
            this.đăngXuấtToolStripMenuItem.Name = "đăngXuấtToolStripMenuItem";
            this.đăngXuấtToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.đăngXuấtToolStripMenuItem.Text = "Đăng xuất";
            this.đăngXuấtToolStripMenuItem.Click += new System.EventHandler(this.đăngXuấtToolStripMenuItem_Click);
            // 
            // thoátToolStripMenuItem
            // 
            this.thoátToolStripMenuItem.Name = "thoátToolStripMenuItem";
            this.thoátToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.thoátToolStripMenuItem.Text = "Thoát";
            this.thoátToolStripMenuItem.Click += new System.EventHandler(this.thoátToolStripMenuItem_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.leftPanel);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.rightPanel);
            this.splitContainer.Size = new System.Drawing.Size(800, 576);
            this.splitContainer.SplitterDistance = 400;
            this.splitContainer.TabIndex = 1;
            // 
            // leftPanel
            // 
            this.leftPanel.Controls.Add(this.productPanel);
            this.leftPanel.Controls.Add(this.searchButton);
            this.leftPanel.Controls.Add(this.searchBox);
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftPanel.Location = new System.Drawing.Point(0, 0);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(400, 576);
            this.leftPanel.TabIndex = 0;
            // 
            // productPanel
            // 
            this.productPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productPanel.Location = new System.Drawing.Point(0, 43);
            this.productPanel.Name = "productPanel";
            this.productPanel.Size = new System.Drawing.Size(400, 533);
            this.productPanel.TabIndex = 3;
            // 
            // searchButton
            // 
            this.searchButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchButton.Location = new System.Drawing.Point(0, 20);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(400, 23);
            this.searchButton.TabIndex = 2;
            this.searchButton.Text = "🔍";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // searchBox
            // 
            this.searchBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchBox.Location = new System.Drawing.Point(0, 0);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(400, 20);
            this.searchBox.TabIndex = 1;
            // 
            // rightPanel
            // 
            this.rightPanel.Controls.Add(this.button1);
            this.rightPanel.Controls.Add(this.invoiceGrid);
            this.rightPanel.Controls.Add(this.payButton);
            this.rightPanel.Controls.Add(this.totalLabel);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPanel.Location = new System.Drawing.Point(0, 0);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(396, 576);
            this.rightPanel.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Top;
            this.button1.Location = new System.Drawing.Point(0, 488);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(396, 41);
            this.button1.TabIndex = 3;
            this.button1.Text = "Làm Mới";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // invoiceGrid
            // 
            this.invoiceGrid.AllowUserToAddRows = false;
            this.invoiceGrid.AllowUserToDeleteRows = false;
            this.invoiceGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.invoiceGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.invoiceGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.invoiceGrid.Location = new System.Drawing.Point(0, 53);
            this.invoiceGrid.Name = "invoiceGrid";
            this.invoiceGrid.ReadOnly = true;
            this.invoiceGrid.Size = new System.Drawing.Size(396, 435);
            this.invoiceGrid.TabIndex = 0;
            this.invoiceGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.invoiceGrid_CellContentClick_1);
            // 
            // payButton
            // 
            this.payButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.payButton.Location = new System.Drawing.Point(0, 23);
            this.payButton.Name = "payButton";
            this.payButton.Size = new System.Drawing.Size(396, 30);
            this.payButton.TabIndex = 2;
            this.payButton.Text = "Thanh Toán";
            this.payButton.UseVisualStyleBackColor = true;
            this.payButton.Click += new System.EventHandler(this.payButton_Click);
            // 
            // totalLabel
            // 
            this.totalLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.totalLabel.Location = new System.Drawing.Point(0, 0);
            this.totalLabel.Name = "totalLabel";
            this.totalLabel.Size = new System.Drawing.Size(396, 23);
            this.totalLabel.TabIndex = 1;
            this.totalLabel.Text = "Tổng Tiền: 0.00 VND";
            this.totalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.totalLabel.Click += new System.EventHandler(this.totalLabel_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CashierApp";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.leftPanel.ResumeLayout(false);
            this.leftPanel.PerformLayout();
            this.rightPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.invoiceGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ToolStripMenuItem hóaĐơnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quảnLýTàiKhoảnToolStripMenuItem;
        private System.Windows.Forms.Button button1;
    }

}
