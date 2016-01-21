namespace Technology
{
    partial class FlowInfo
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddFlowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ModifyFlowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteFlowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(596, 346);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "工序ID";
            this.columnHeader1.Width = 70;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "工序名称";
            this.columnHeader2.Width = 205;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "描述";
            this.columnHeader3.Width = 280;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddFlowToolStripMenuItem,
            this.ModifyFlowToolStripMenuItem,
            this.DeleteFlowToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 70);
            // 
            // AddFlowToolStripMenuItem
            // 
            this.AddFlowToolStripMenuItem.Name = "AddFlowToolStripMenuItem";
            this.AddFlowToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.AddFlowToolStripMenuItem.Text = "添加工序";
            this.AddFlowToolStripMenuItem.Click += new System.EventHandler(this.AddFlowToolStripMenuItem_Click);
            // 
            // ModifyFlowToolStripMenuItem
            // 
            this.ModifyFlowToolStripMenuItem.Name = "ModifyFlowToolStripMenuItem";
            this.ModifyFlowToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.ModifyFlowToolStripMenuItem.Text = "修改工序";
            this.ModifyFlowToolStripMenuItem.Click += new System.EventHandler(this.ModifyFlowToolStripMenuItem_Click);
            // 
            // DeleteFlowToolStripMenuItem
            // 
            this.DeleteFlowToolStripMenuItem.Name = "DeleteFlowToolStripMenuItem";
            this.DeleteFlowToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.DeleteFlowToolStripMenuItem.Text = "删除工序";
            this.DeleteFlowToolStripMenuItem.Click += new System.EventHandler(this.DeleteFlowToolStripMenuItem_Click);
            // 
            // FlowInfo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(596, 346);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.listView1);
            this.Name = "FlowInfo";
            this.Text = "工艺流程";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FlowInfo_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem AddFlowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ModifyFlowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteFlowToolStripMenuItem;
    }
}