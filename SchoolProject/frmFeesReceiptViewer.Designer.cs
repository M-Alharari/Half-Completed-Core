namespace SchoolProject.Receipts
{
    partial class frmFeesReceiptViewer
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
            btnClick = new Button();
            btnPrintIt = new Button();
            SuspendLayout();
            // 
            // btnClick
            // 
            btnClick.Location = new Point(488, 611);
            btnClick.Margin = new Padding(4, 3, 4, 3);
            btnClick.Name = "btnClick";
            btnClick.Size = new Size(108, 38);
            btnClick.TabIndex = 0;
            btnClick.Text = "Save as file";
            btnClick.UseVisualStyleBackColor = true;
            btnClick.Click += btnClick_Click;
            // 
            // btnPrintIt
            // 
            btnPrintIt.Location = new Point(378, 611);
            btnPrintIt.Margin = new Padding(4, 3, 4, 3);
            btnPrintIt.Name = "btnPrintIt";
            btnPrintIt.Size = new Size(103, 38);
            btnPrintIt.TabIndex = 1;
            btnPrintIt.Text = "button1";
            btnPrintIt.UseVisualStyleBackColor = true;
            btnPrintIt.Click += btnPrintIt_Click;
            // 
            // frmFeesReceiptViewer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(652, 698);
            Controls.Add(btnPrintIt);
            Controls.Add(btnClick);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Margin = new Padding(4, 3, 4, 3);
            Name = "frmFeesReceiptViewer";
            Text = "frmFeesReceiptViewer";
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClick;
        private System.Windows.Forms.Button btnPrintIt;
    }
}