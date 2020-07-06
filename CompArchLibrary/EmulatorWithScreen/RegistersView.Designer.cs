namespace EmulatorWithScreen
{
    partial class RegistersView
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
            this.registersDebugLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // registersDebugLabel
            // 
            this.registersDebugLabel.AutoSize = true;
            this.registersDebugLabel.Location = new System.Drawing.Point(13, 8);
            this.registersDebugLabel.Name = "registersDebugLabel";
            this.registersDebugLabel.Size = new System.Drawing.Size(51, 13);
            this.registersDebugLabel.TabIndex = 0;
            this.registersDebugLabel.Text = "Registers";
            // 
            // RegistersView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 450);
            this.Controls.Add(this.registersDebugLabel);
            this.Name = "RegistersView";
            this.Text = "RegistersView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label registersDebugLabel;
    }
}