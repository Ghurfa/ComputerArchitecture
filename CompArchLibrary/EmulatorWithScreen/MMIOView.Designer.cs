namespace EmulatorWithScreen
{
    partial class MMIOView
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.mmioDebugLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.mmioDebugLabel);
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Margin = new System.Windows.Forms.Padding(10);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(162, 440);
            this.panel1.TabIndex = 0;
            // 
            // mmioDebugLabel
            // 
            this.mmioDebugLabel.AutoSize = true;
            this.mmioDebugLabel.Location = new System.Drawing.Point(12, 10);
            this.mmioDebugLabel.Name = "mmioDebugLabel";
            this.mmioDebugLabel.Size = new System.Drawing.Size(35, 13);
            this.mmioDebugLabel.TabIndex = 0;
            this.mmioDebugLabel.Text = "label1";
            // 
            // MMIOView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 461);
            this.Controls.Add(this.panel1);
            this.Name = "MMIOView";
            this.Text = "MMIOView";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label mmioDebugLabel;
    }
}