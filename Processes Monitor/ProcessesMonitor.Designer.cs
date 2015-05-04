namespace Processes_Monitor
{
    partial class ProcessesMonitor
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
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabProcess = new System.Windows.Forms.TabPage();
            this.pEndTask_b = new System.Windows.Forms.Button();
            this.process_ListView = new System.Windows.Forms.ListView();
            this.pName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pPid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pUserName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pCPU = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pMemory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pLocalPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.process_ContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.endTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showMomoryMonitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goToSerivceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabService = new System.Windows.Forms.TabPage();
            this.service_ListView = new System.Windows.Forms.ListView();
            this.sName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sPid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.service_ContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reStartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goToProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabProcess.SuspendLayout();
            this.process_ContextMenu.SuspendLayout();
            this.tabService.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.service_ContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabProcess);
            this.tabControl1.Controls.Add(this.tabService);
            this.tabControl1.Location = new System.Drawing.Point(12, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(711, 590);
            this.tabControl1.TabIndex = 0;
            // 
            // tabProcess
            // 
            this.tabProcess.Controls.Add(this.pEndTask_b);
            this.tabProcess.Controls.Add(this.process_ListView);
            this.tabProcess.Location = new System.Drawing.Point(4, 22);
            this.tabProcess.Name = "tabProcess";
            this.tabProcess.Padding = new System.Windows.Forms.Padding(3);
            this.tabProcess.Size = new System.Drawing.Size(703, 564);
            this.tabProcess.TabIndex = 0;
            this.tabProcess.Text = "Process";
            this.tabProcess.UseVisualStyleBackColor = true;
            // 
            // pEndTask_b
            // 
            this.pEndTask_b.Location = new System.Drawing.Point(621, 535);
            this.pEndTask_b.Name = "pEndTask_b";
            this.pEndTask_b.Size = new System.Drawing.Size(75, 23);
            this.pEndTask_b.TabIndex = 1;
            this.pEndTask_b.Text = "End Task";
            this.pEndTask_b.UseVisualStyleBackColor = true;
            // 
            // process_ListView
            // 
            this.process_ListView.AllowDrop = true;
            this.process_ListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.process_ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.pName,
            this.pPid,
            this.pUserName,
            this.pCPU,
            this.pMemory,
            this.pLocalPath});
            this.process_ListView.ContextMenuStrip = this.process_ContextMenu;
            this.process_ListView.FullRowSelect = true;
            this.process_ListView.Location = new System.Drawing.Point(0, 0);
            this.process_ListView.Name = "process_ListView";
            this.process_ListView.Size = new System.Drawing.Size(703, 529);
            this.process_ListView.TabIndex = 0;
            this.process_ListView.UseCompatibleStateImageBehavior = false;
            this.process_ListView.View = System.Windows.Forms.View.Details;
            this.process_ListView.SelectedIndexChanged += new System.EventHandler(this.process_ListView_SelectedIndexChanged);
            // 
            // pName
            // 
            this.pName.Text = "Name";
            this.pName.Width = 196;
            // 
            // pPid
            // 
            this.pPid.Text = "PID";
            // 
            // pUserName
            // 
            this.pUserName.Text = "UserName";
            this.pUserName.Width = 100;
            // 
            // pCPU
            // 
            this.pCPU.Text = "CPU";
            // 
            // pMemory
            // 
            this.pMemory.Text = "Memory(Working set)";
            this.pMemory.Width = 113;
            // 
            // pLocalPath
            // 
            this.pLocalPath.Text = "LocalPath";
            this.pLocalPath.Width = 169;
            // 
            // process_ContextMenu
            // 
            this.process_ContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.endTaskToolStripMenuItem,
            this.showMomoryMonitorToolStripMenuItem,
            this.refreshToolStripMenuItem,
            this.goToSerivceToolStripMenuItem});
            this.process_ContextMenu.Name = "process_ContextMenu";
            this.process_ContextMenu.Size = new System.Drawing.Size(199, 92);
            // 
            // endTaskToolStripMenuItem
            // 
            this.endTaskToolStripMenuItem.Name = "endTaskToolStripMenuItem";
            this.endTaskToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.endTaskToolStripMenuItem.Text = "End Task";
            this.endTaskToolStripMenuItem.Click += new System.EventHandler(this.endTaskToolStripMenuItem_Click);
            // 
            // showMomoryMonitorToolStripMenuItem
            // 
            this.showMomoryMonitorToolStripMenuItem.Name = "showMomoryMonitorToolStripMenuItem";
            this.showMomoryMonitorToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.showMomoryMonitorToolStripMenuItem.Text = "Show Momory Monitor";
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // goToSerivceToolStripMenuItem
            // 
            this.goToSerivceToolStripMenuItem.Name = "goToSerivceToolStripMenuItem";
            this.goToSerivceToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.goToSerivceToolStripMenuItem.Text = "Go to Serivce";
            this.goToSerivceToolStripMenuItem.Click += new System.EventHandler(this.goToSerivceToolStripMenuItem_Click);
            // 
            // tabService
            // 
            this.tabService.Controls.Add(this.service_ListView);
            this.tabService.Location = new System.Drawing.Point(4, 22);
            this.tabService.Name = "tabService";
            this.tabService.Padding = new System.Windows.Forms.Padding(3);
            this.tabService.Size = new System.Drawing.Size(703, 564);
            this.tabService.TabIndex = 1;
            this.tabService.Text = "Service";
            this.tabService.UseVisualStyleBackColor = true;
            // 
            // service_ListView
            // 
            this.service_ListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.service_ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.sName,
            this.sDescription,
            this.sStatus,
            this.sPid});
            this.service_ListView.ContextMenuStrip = this.service_ContextMenu;
            this.service_ListView.FullRowSelect = true;
            this.service_ListView.Location = new System.Drawing.Point(0, 0);
            this.service_ListView.MultiSelect = false;
            this.service_ListView.Name = "service_ListView";
            this.service_ListView.Size = new System.Drawing.Size(703, 532);
            this.service_ListView.TabIndex = 0;
            this.service_ListView.UseCompatibleStateImageBehavior = false;
            this.service_ListView.View = System.Windows.Forms.View.Details;
            this.service_ListView.SelectedIndexChanged += new System.EventHandler(this.service_ListView_SelectedIndexChanged);
            // 
            // sName
            // 
            this.sName.Text = "Name";
            this.sName.Width = 161;
            // 
            // sDescription
            // 
            this.sDescription.Text = "Description";
            this.sDescription.Width = 175;
            // 
            // sStatus
            // 
            this.sStatus.Text = "Status";
            this.sStatus.Width = 80;
            // 
            // sPid
            // 
            this.sPid.Text = "PID";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(724, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.optionToolStripMenuItem.Text = "Option";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // service_ContextMenu
            // 
            this.service_ContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.reStartToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.goToProcessToolStripMenuItem});
            this.service_ContextMenu.Name = "service_ContextMenu";
            this.service_ContextMenu.Size = new System.Drawing.Size(150, 92);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // reStartToolStripMenuItem
            // 
            this.reStartToolStripMenuItem.Name = "reStartToolStripMenuItem";
            this.reStartToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.reStartToolStripMenuItem.Text = "ReStart";
            this.reStartToolStripMenuItem.Click += new System.EventHandler(this.reStartToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // goToProcessToolStripMenuItem
            // 
            this.goToProcessToolStripMenuItem.Name = "goToProcessToolStripMenuItem";
            this.goToProcessToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.goToProcessToolStripMenuItem.Text = "Go To Process";
            this.goToProcessToolStripMenuItem.Click += new System.EventHandler(this.goToProcessToolStripMenuItem_Click);
            // 
            // ProcessesMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 620);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ProcessesMonitor";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.ProcessesMonitor_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabProcess.ResumeLayout(false);
            this.process_ContextMenu.ResumeLayout(false);
            this.tabService.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.service_ContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabProcess;
        private System.Windows.Forms.TabPage tabService;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ListView process_ListView;
        public System.Windows.Forms.ColumnHeader pName;
        private System.Windows.Forms.ColumnHeader pPid;
        private System.Windows.Forms.ColumnHeader pUserName;
        private System.Windows.Forms.ColumnHeader pCPU;
        private System.Windows.Forms.ColumnHeader pMemory;
        private System.Windows.Forms.ColumnHeader pLocalPath;
        private System.Windows.Forms.ListView service_ListView;
        private System.Windows.Forms.ColumnHeader sName;
        private System.Windows.Forms.ColumnHeader sDescription;
        private System.Windows.Forms.ColumnHeader sStatus;
        private System.Windows.Forms.ColumnHeader sPid;
        private System.Windows.Forms.Button pEndTask_b;
        private System.Windows.Forms.ContextMenuStrip process_ContextMenu;
        private System.Windows.Forms.ToolStripMenuItem endTaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showMomoryMonitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goToSerivceToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip service_ContextMenu;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reStartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goToProcessToolStripMenuItem;
    }
}

