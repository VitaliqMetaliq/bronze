namespace MyTestApplication
{
    partial class sampleForm
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
            this.sampleGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.sampleGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // sampleGridView
            // 
            this.sampleGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sampleGridView.Location = new System.Drawing.Point(12, 12);
            this.sampleGridView.Name = "sampleGridView";
            this.sampleGridView.RowHeadersWidth = 51;
            this.sampleGridView.RowTemplate.Height = 24;
            this.sampleGridView.Size = new System.Drawing.Size(859, 421);
            this.sampleGridView.TabIndex = 0;
            // 
            // sampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 445);
            this.Controls.Add(this.sampleGridView);
            this.Name = "sampleForm";
            this.Text = "sampleForm";
            ((System.ComponentModel.ISupportInitialize)(this.sampleGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView sampleGridView;
    }
}