namespace Named_Pipe_Example
{
    partial class frmMain
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
            this.lblPipeStep_Value = new System.Windows.Forms.Label();
            this.lblPipeStatus_Value = new System.Windows.Forms.Label();
            this.lblPipeStep = new System.Windows.Forms.Label();
            this.lblPipeStatus = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssl_PipeStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslPipeStatus_Value = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCurrentStep_Value = new System.Windows.Forms.Label();
            this.lblCurrentStatus_Value = new System.Windows.Forms.Label();
            this.lblCurrentStep = new System.Windows.Forms.Label();
            this.lblCurrentStatus = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPipeStep_Value
            // 
            this.lblPipeStep_Value.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPipeStep_Value.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblPipeStep_Value.Location = new System.Drawing.Point(215, 42);
            this.lblPipeStep_Value.Margin = new System.Windows.Forms.Padding(3, 10, 10, 10);
            this.lblPipeStep_Value.Name = "lblPipeStep_Value";
            this.lblPipeStep_Value.Size = new System.Drawing.Size(20, 13);
            this.lblPipeStep_Value.TabIndex = 0;
            this.lblPipeStep_Value.Text = "-";
            this.lblPipeStep_Value.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPipeStatus_Value
            // 
            this.lblPipeStatus_Value.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPipeStatus_Value.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblPipeStatus_Value.Location = new System.Drawing.Point(215, 19);
            this.lblPipeStatus_Value.Margin = new System.Windows.Forms.Padding(3, 10, 10, 10);
            this.lblPipeStatus_Value.Name = "lblPipeStatus_Value";
            this.lblPipeStatus_Value.Size = new System.Drawing.Size(20, 13);
            this.lblPipeStatus_Value.TabIndex = 0;
            this.lblPipeStatus_Value.Text = "-";
            this.lblPipeStatus_Value.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPipeStep
            // 
            this.lblPipeStep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPipeStep.AutoSize = true;
            this.lblPipeStep.Location = new System.Drawing.Point(152, 42);
            this.lblPipeStep.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lblPipeStep.Name = "lblPipeStep";
            this.lblPipeStep.Size = new System.Drawing.Size(58, 13);
            this.lblPipeStep.TabIndex = 0;
            this.lblPipeStep.Text = "Step Main:";
            // 
            // lblPipeStatus
            // 
            this.lblPipeStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPipeStatus.AutoSize = true;
            this.lblPipeStatus.Location = new System.Drawing.Point(144, 19);
            this.lblPipeStatus.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.lblPipeStatus.Name = "lblPipeStatus";
            this.lblPipeStatus.Size = new System.Drawing.Size(66, 13);
            this.lblPipeStatus.TabIndex = 0;
            this.lblPipeStatus.Text = "Status Main:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_PipeStatus,
            this.tsslPipeStatus_Value});
            this.statusStrip1.Location = new System.Drawing.Point(0, 119);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(254, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssl_PipeStatus
            // 
            this.tssl_PipeStatus.BackColor = System.Drawing.Color.Transparent;
            this.tssl_PipeStatus.Name = "tssl_PipeStatus";
            this.tssl_PipeStatus.Size = new System.Drawing.Size(68, 17);
            this.tssl_PipeStatus.Text = "Pipe Status:";
            // 
            // tsslPipeStatus_Value
            // 
            this.tsslPipeStatus_Value.BackColor = System.Drawing.Color.Transparent;
            this.tsslPipeStatus_Value.Name = "tsslPipeStatus_Value";
            this.tsslPipeStatus_Value.Size = new System.Drawing.Size(12, 17);
            this.tsslPipeStatus_Value.Text = "-";
            // 
            // lblCurrentStep_Value
            // 
            this.lblCurrentStep_Value.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblCurrentStep_Value.Location = new System.Drawing.Point(101, 42);
            this.lblCurrentStep_Value.Margin = new System.Windows.Forms.Padding(10);
            this.lblCurrentStep_Value.Name = "lblCurrentStep_Value";
            this.lblCurrentStep_Value.Size = new System.Drawing.Size(20, 13);
            this.lblCurrentStep_Value.TabIndex = 2;
            this.lblCurrentStep_Value.Text = "-";
            this.lblCurrentStep_Value.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrentStatus_Value
            // 
            this.lblCurrentStatus_Value.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblCurrentStatus_Value.Location = new System.Drawing.Point(101, 19);
            this.lblCurrentStatus_Value.Margin = new System.Windows.Forms.Padding(10);
            this.lblCurrentStatus_Value.Name = "lblCurrentStatus_Value";
            this.lblCurrentStatus_Value.Size = new System.Drawing.Size(20, 13);
            this.lblCurrentStatus_Value.TabIndex = 3;
            this.lblCurrentStatus_Value.Text = "-";
            this.lblCurrentStatus_Value.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrentStep
            // 
            this.lblCurrentStep.AutoSize = true;
            this.lblCurrentStep.Location = new System.Drawing.Point(27, 42);
            this.lblCurrentStep.Margin = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.lblCurrentStep.Name = "lblCurrentStep";
            this.lblCurrentStep.Size = new System.Drawing.Size(69, 13);
            this.lblCurrentStep.TabIndex = 4;
            this.lblCurrentStep.Text = "Current Step:";
            // 
            // lblCurrentStatus
            // 
            this.lblCurrentStatus.AutoSize = true;
            this.lblCurrentStatus.Location = new System.Drawing.Point(19, 19);
            this.lblCurrentStatus.Margin = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.lblCurrentStatus.Name = "lblCurrentStatus";
            this.lblCurrentStatus.Size = new System.Drawing.Size(77, 13);
            this.lblCurrentStatus.TabIndex = 5;
            this.lblCurrentStatus.Text = "Current Status:";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(90, 86);
            this.btnStart.Margin = new System.Windows.Forms.Padding(10);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 6;
            this.btnStart.TabStop = false;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(254, 141);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblCurrentStep_Value);
            this.Controls.Add(this.lblCurrentStatus_Value);
            this.Controls.Add(this.lblCurrentStep);
            this.Controls.Add(this.lblCurrentStatus);
            this.Controls.Add(this.lblPipeStep_Value);
            this.Controls.Add(this.lblPipeStatus_Value);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lblPipeStep);
            this.Controls.Add(this.lblPipeStatus);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Named Pipe";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslPipeStatus_Value;
        private System.Windows.Forms.Label lblPipeStatus_Value;
        private System.Windows.Forms.Label lblPipeStatus;
        private System.Windows.Forms.Label lblPipeStep_Value;
        private System.Windows.Forms.Label lblPipeStep;
        private System.Windows.Forms.ToolStripStatusLabel tssl_PipeStatus;
        private System.Windows.Forms.Label lblCurrentStep_Value;
        private System.Windows.Forms.Label lblCurrentStatus_Value;
        private System.Windows.Forms.Label lblCurrentStep;
        private System.Windows.Forms.Label lblCurrentStatus;
        private System.Windows.Forms.Button btnStart;
    }
}

