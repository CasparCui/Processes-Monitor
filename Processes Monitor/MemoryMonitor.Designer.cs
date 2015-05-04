namespace Processes_Monitor
{
    partial class MemoryMonitor
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
            this.monitorControl1 = new SerialportSample.MonitorControl();
            this.SuspendLayout();
            // 
            // monitorControl1
            // 
            this.monitorControl1.Location = new System.Drawing.Point(-2, -1);
            this.monitorControl1.MonitorBlackColor = System.Drawing.Color.Black;
            this.monitorControl1.MonitorName = null;
            this.monitorControl1.MonitorUserColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.monitorControl1.Name = "monitorControl1";
            this.monitorControl1.Size = new System.Drawing.Size(296, 226);
            this.monitorControl1.TabIndex = 0;
            // 
            // MemoryMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 228);
            this.Controls.Add(this.monitorControl1);
            this.Name = "MemoryMonitor";
            this.Text = "MemoryMonitor";
            this.Load += new System.EventHandler(this.MemoryMonitor_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private SerialportSample.MonitorControl monitorControl1;
    }
}