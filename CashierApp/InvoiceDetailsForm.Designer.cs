namespace CashierApp
{
    partial class InvoiceDetailsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView invoiceDetailsGridView;
        private System.Windows.Forms.TextBox productIDTextBox;
        private System.Windows.Forms.TextBox quantityTextBox;
        private System.Windows.Forms.TextBox priceTextBox;
        private System.Windows.Forms.Button addDetailButton;
        private System.Windows.Forms.Button editDetailButton;
        private System.Windows.Forms.Button deleteDetailButton;

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
            this.invoiceDetailsGridView = new System.Windows.Forms.DataGridView();
            this.productIDTextBox = new System.Windows.Forms.TextBox();
            this.quantityTextBox = new System.Windows.Forms.TextBox();
            this.priceTextBox = new System.Windows.Forms.TextBox();
            this.addDetailButton = new System.Windows.Forms.Button();
            this.editDetailButton = new System.Windows.Forms.Button();
            this.deleteDetailButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.invoiceDetailsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // invoiceDetailsGridView
            // 
            this.invoiceDetailsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.invoiceDetailsGridView.Location = new System.Drawing.Point(12, 12);
            this.invoiceDetailsGridView.Name = "invoiceDetailsGridView";
            this.invoiceDetailsGridView.Size = new System.Drawing.Size(600, 300);
            this.invoiceDetailsGridView.TabIndex = 0;
            // 
            // productIDTextBox
            // 
            this.productIDTextBox.Location = new System.Drawing.Point(640, 30);
            this.productIDTextBox.Name = "productIDTextBox";
            this.productIDTextBox.Size = new System.Drawing.Size(200, 20);
            this.productIDTextBox.TabIndex = 1;
        //    this.productIDTextBox.PlaceholderText = "Product ID";
            // 
            // quantityTextBox
            // 
            this.quantityTextBox.Location = new System.Drawing.Point(640, 70);
            this.quantityTextBox.Name = "quantityTextBox";
            this.quantityTextBox.Size = new System.Drawing.Size(200, 20);
            this.quantityTextBox.TabIndex = 2;
         //   this.quantityTextBox.PlaceholderText = "Quantity";
            // 
            // priceTextBox
            // 
            this.priceTextBox.Location = new System.Drawing.Point(640, 110);
            this.priceTextBox.Name = "priceTextBox";
            this.priceTextBox.Size = new System.Drawing.Size(200, 20);
            this.priceTextBox.TabIndex = 3;
         //   this.priceTextBox.PlaceholderText = "Price";
            // 
            // addDetailButton
            // 
            this.addDetailButton.Location = new System.Drawing.Point(640, 150);
            this.addDetailButton.Name = "addDetailButton";
            this.addDetailButton.Size = new System.Drawing.Size(75, 23);
            this.addDetailButton.TabIndex = 4;
            this.addDetailButton.Text = "Add Detail";
            this.addDetailButton.UseVisualStyleBackColor = true;
            this.addDetailButton.Click += new System.EventHandler(this.addDetailButton_Click);
            // 
            // editDetailButton
            // 
            this.editDetailButton.Location = new System.Drawing.Point(740, 150);
            this.editDetailButton.Name = "editDetailButton";
            this.editDetailButton.Size = new System.Drawing.Size(75, 23);
            this.editDetailButton.TabIndex = 5;
            this.editDetailButton.Text = "Edit Detail";
            this.editDetailButton.UseVisualStyleBackColor = true;
            this.editDetailButton.Click += new System.EventHandler(this.editDetailButton_Click);
            // 
            // deleteDetailButton
            // 
            this.deleteDetailButton.Location = new System.Drawing.Point(640, 190);
            this.deleteDetailButton.Name = "deleteDetailButton";
            this.deleteDetailButton.Size = new System.Drawing.Size(75, 23);
            this.deleteDetailButton.TabIndex = 6;
            this.deleteDetailButton.Text = "Delete Detail";
            this.deleteDetailButton.UseVisualStyleBackColor = true;
            this.deleteDetailButton.Click += new System.EventHandler(this.deleteDetailButton_Click);
            // 
            // InvoiceDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 321);
            this.Controls.Add(this.deleteDetailButton);
            this.Controls.Add(this.editDetailButton);
            this.Controls.Add(this.addDetailButton);
            this.Controls.Add(this.priceTextBox);
            this.Controls.Add(this.quantityTextBox);
            this.Controls.Add(this.productIDTextBox);
            this.Controls.Add(this.invoiceDetailsGridView);
            this.Name = "InvoiceDetailsForm";
            this.Text = "Invoice Details";
            this.Load += new System.EventHandler(this.InvoiceDetailsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.invoiceDetailsGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
