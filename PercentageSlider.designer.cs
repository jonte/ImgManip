namespace ImgResize
{
    partial class PercentageSlider
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
            this.percSlider = new System.Windows.Forms.TrackBar();
            this.okBtn = new System.Windows.Forms.Button();
            this.percLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.percSlider)).BeginInit();
            this.SuspendLayout();
            // 
            // percSlider
            // 
            this.percSlider.Location = new System.Drawing.Point(12, 12);
            this.percSlider.Maximum = 100;
            this.percSlider.Name = "percSlider";
            this.percSlider.Size = new System.Drawing.Size(219, 45);
            this.percSlider.TabIndex = 0;
            this.percSlider.TickFrequency = 10;
            this.percSlider.Value = 25;
            this.percSlider.Scroll += new System.EventHandler(this.percSlider_Scroll);
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(12, 63);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(260, 23);
            this.okBtn.TabIndex = 1;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // percLabel
            // 
            this.percLabel.AutoSize = true;
            this.percLabel.Location = new System.Drawing.Point(237, 13);
            this.percLabel.Name = "percLabel";
            this.percLabel.Size = new System.Drawing.Size(0, 13);
            this.percLabel.TabIndex = 2;
            // 
            // PercentageSlider
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 99);
            this.Controls.Add(this.percLabel);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.percSlider);
            this.Name = "PercentageSlider";
            this.Text = "PercentageSlider";
            this.Load += new System.EventHandler(this.PercentageSlider_Load);
            ((System.ComponentModel.ISupportInitialize)(this.percSlider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar percSlider;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Label percLabel;
    }
}