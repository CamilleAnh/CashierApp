namespace CashierApp
{
    partial class InvoiceForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView invoicesGridView;
        private System.Windows.Forms.TextBox invoiceNumberTextBox;
        private System.Windows.Forms.TextBox totalAmountTextBox;
        private System.Windows.Forms.TextBox invoiceDateTextBox;
        private System.Windows.Forms.ComboBox cashierComboBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button viewDetailsButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.invoicesGridView = new System.Windows.Forms.DataGridView();
            this.invoiceNumberTextBox = new System.Windows.Forms.TextBox();
            this.totalAmountTextBox = new System.Windows.Forms.TextBox();
            this.invoiceDateTextBox = new System.Windows.Forms.TextBox();
            this.cashierComboBox = new System.Windows.Forms.ComboBox();
            this.addButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.viewDetailsButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.invoicesGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // invoicesGridView
            // 
            this.invoicesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.invoicesGridView.Location = new System.Drawing.Point(12, 12);
            this.invoicesGridView.Name = "invoicesGridView";
            this.invoicesGridView.Size = new System.Drawing.Size(600, 300);
            this.invoicesGridView.TabIndex = 0;
            // 
            // invoiceNumberTextBox
            // 
            this.invoiceNumberTextBox.Location = new System.Drawing.Point(640, 30);
            this.invoiceNumberTextBox.Name = "invoiceNumberTextBox";
            this.invoiceNumberTextBox.Size = new System.Drawing.Size(200, 20);
            this.invoiceNumberTextBox.TabIndex = 1;
            // 
            // totalAmountTextBox
            // 
            this.totalAmountTextBox.Location = new System.Drawing.Point(640, 70);
            this.totalAmountTextBox.Name = "totalAmountTextBox";
            this.totalAmountTextBox.Size = new System.Drawing.Size(200, 20);
            this.totalAmountTextBox.TabIndex = 2;
            // 
            // invoiceDateTextBox
            // 
            this.invoiceDateTextBox.Location = new System.Drawing.Point(640, 110);
            this.invoiceDateTextBox.Name = "invoiceDateTextBox";
            this.invoiceDateTextBox.Size = new System.Drawing.Size(200, 20);
            this.invoiceDateTextBox.TabIndex = 3;
            // 
            // cashierComboBox
            // 
            this.cashierComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cashierComboBox.FormattingEnabled = true;
            this.cashierComboBox.Location = new System.Drawing.Point(640, 150);
            this.cashierComboBox.Name = "cashierComboBox";
            this.cashierComboBox.Size = new System.Drawing.Size(200, 21);
            this.cashierComboBox.TabIndex = 4;
            this.cashierComboBox.SelectedIndexChanged += new System.EventHandler(this.cashierComboBox_SelectedIndexChanged);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(640, 200);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 5;
            this.addButton.Text = "Add Invoice";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(740, 200);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(75, 23);
            this.editButton.TabIndex = 6;
            this.editButton.Text = "Edit Invoice";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(640, 240);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 7;
            this.deleteButton.Text = "Delete Invoice";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // viewDetailsButton
            // 
            this.viewDetailsButton.Location = new System.Drawing.Point(740, 240);
            this.viewDetailsButton.Name = "viewDetailsButton";
            this.viewDetailsButton.Size = new System.Drawing.Size(100, 23);
            this.viewDetailsButton.TabIndex = 8;
            this.viewDetailsButton.Text = "View Details";
            this.viewDetailsButton.UseVisualStyleBackColor = true;
            this.viewDetailsButton.Click += new System.EventHandler(this.viewDetailsButton_Click);
            // 
            // InvoiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 321);
            this.Controls.Add(this.viewDetailsButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.cashierComboBox);
            this.Controls.Add(this.invoiceDateTextBox);
            this.Controls.Add(this.totalAmountTextBox);
            this.Controls.Add(this.invoiceNumberTextBox);
            this.Controls.Add(this.invoicesGridView);
            this.Name = "InvoiceForm";
            this.Text = "Invoices";
            this.Load += new System.EventHandler(this.InvoiceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.invoicesGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}