using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Technology
{
    public partial class FlowProjectInfo : Form
    {
        private SqlConnection sqlConnection;
        private DataSet dataSet;
        private TechnologyLib technologyLib;
        private ListViewColumnSorter lvwColumnSorter;

        private int OperateFlag = 0;
        private int[] FlowProject_Widths = new int[] { 6, 10, 8 };
        private int[] FlowProjectDetails_Widths = new int[] { 3, 3, 8, 8, 8 };
        private string[] FlowProject = new string[] { "工序方案ID", "工序方案名称", "描述" };
        private string[] FlowProjectDetails = new string[] { "工序序号", "工序ID", "工序名称", "上一道工序", "后一道工序" };
        
        public FlowProjectInfo(SqlConnection mainConnection)
        {
            InitializeComponent();
            sqlConnection = mainConnection;
            lvwColumnSorter = new ListViewColumnSorter();
            this.listView1.ListViewItemSorter = lvwColumnSorter;
            
        }

        private void FlowProjectInfo_Load(object sender, EventArgs e)
        {
            technologyLib = new TechnologyLib(sqlConnection);
            dataSet = new DataSet();
            technologyLib.GetFlowProjectData(dataSet);
            technologyLib.GetFlowProjectDetailsViewData(dataSet);
            InitialTreeView(treeView1);
        }

        public void InitialTreeView(TreeView myTreeView)
        {
            TreeNode newNode;	//根节点
            newNode = new TreeNode("工序方案名称");
            newNode.ImageIndex = 0;
            newNode.SelectedImageIndex = 0;
            myTreeView.Nodes.Add(newNode);

            TreeNode newNode1;
            for (int i = 0; i < dataSet.Tables["FlowProject"].Rows.Count; i++)
            {
                int flowprojectid = Convert.ToInt16(dataSet.Tables["FlowProject"].Rows[i]["FlowProjectID"]);
                newNode1 = new TreeNode(string.Format("{0}.{1}", flowprojectid, dataSet.Tables["FlowProject"].Rows[i]["FlowProjectName"].ToString()));
                newNode1.ImageIndex = 1;
                newNode1.SelectedImageIndex = 1;
                myTreeView.Nodes[0].Nodes.Add(newNode1);
            }
            myTreeView.Nodes[0].Expand();
            myTreeView.Nodes[0].TreeView.Focus();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            treeView1.PathSeparator = "/";
            string path = treeView1.SelectedNode.FullPath.ToString();
            int count = 0;
            for (int i = 0; i < path.Length; i++)
            {   
                if (path[i].CompareTo('/') == 0)
                    count++;
            }

            switch (count)
            {
                case 0:	//第一级节点:显示工序方案名称
                    listView1.Clear();
                    InsertAllColumns(0);
                    AddItem_FlowProject();
                    break;
                case 1:	//第二级节点:显示具体对应工序
                    listView1.Clear();
                    InsertAllColumns(1);
                    AddItem_FlowProjectDetails(treeView1.SelectedNode.Text);
                    break;  
            }
        }

        private void InsertAllColumns(uint flag)
        {
            int columns = 0;
            int[] widths = new int[20];
            string[] columnHeads = new string[20];
            switch (flag)
            {
                case 0:
                    // Add columns and set their text.
                    columns = FlowProject.Length;
                    FlowProject_Widths.CopyTo(widths, 0);
                    FlowProject.CopyTo(columnHeads, 0);
                    break;
                case 1:
                    columns = FlowProjectDetails.Length;
                    FlowProjectDetails_Widths.CopyTo(widths, 0);
                    FlowProjectDetails.CopyTo(columnHeads, 0);
                    break;
                default:
                    break;
            }

            for (int i = 0; i < columns; i++)
            {
                this.listView1.Columns.Add(new ColumnHeader());
                this.listView1.Columns[i].Text = columnHeads[i];
                this.listView1.Columns[i].Width = widths[i] * 20;
            }
        }

        //显示FlowProject数据项到listView1中
        private void AddItem_FlowProject()
        {
            for (int i = 0; i < dataSet.Tables["FlowProject"].Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem(dataSet.Tables["FlowProject"].Rows[i][0].ToString());
                item.ImageIndex = 1;
                for (int j = 1; j < FlowProject.Length; j++)
                    item.SubItems.Add(dataSet.Tables["FlowProject"].Rows[i][j].ToString());
                listView1.Items.AddRange(new ListViewItem[] { item });
            }
        }

        //显示PopedomDetails数据项到listView1中
        private void AddItem_FlowProjectDetails(string pFlowProject)
        {
            string strVal = "";

            //try
            //{
            //    strVal = pFlowProject.Split('.')[1];
            //}
            //catch
            //{
            //    strVal = pFlowProject;
            //    this.listView1.Clear();
            //}

            try
            {
                strVal = pFlowProject.Split(new Char[] { '.' })[1];
            }
            catch
            {
                return;
            }

            for (int i = 0; i < dataSet.Tables["FlowProjectDetailsView"].Rows.Count; i++)
            {
                if (strVal == dataSet.Tables["FlowProjectDetailsView"].Rows[i]["FlowProjectName"].ToString())
                {
                    ListViewItem item = new ListViewItem(dataSet.Tables["FlowProjectDetailsView"].Rows[i][0].ToString());
                    item.ImageIndex = 2;

                    for (int j = 1; j < FlowProjectDetails.Length; j++)
                    {
                        //if (Convert.ToInt32(dataSet.Tables[0].Rows[i][8]) == 0)
                        //{
                            
                        //}
                        item.SubItems.Add(dataSet.Tables["FlowProjectDetailsView"].Rows[i][j].ToString());
                    }
                    listView1.Items.AddRange(new ListViewItem[] { item });
                }
            }
        }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            treeView1.PathSeparator = "/";
            string path = treeView1.SelectedNode.FullPath.ToString();
            int count = 0;
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i].CompareTo('/') == 0)
                    count++;
            }

            switch (count)
            {
                case 0:	//第一级节点:显示工序方案名称
                    this.contextMenuStrip1.Items[0].Visible = false;
                    this.contextMenuStrip1.Items[1].Visible = true;
                    this.contextMenuStrip1.Items[2].Visible = true;
                    this.contextMenuStrip1.Items[3].Visible = true;
                    break;
                case 1:	//第二级节点:显示对应工序
                    this.contextMenuStrip1.Items[0].Visible = true;
                    this.contextMenuStrip1.Items[1].Visible = false;
                    this.contextMenuStrip1.Items[2].Visible = false;
                    this.contextMenuStrip1.Items[3].Visible = false;
                    break;
            }
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == System.Windows.Forms.SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.listView1.Sort();
        }

        //添加工序方案
        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AddFlowProject addFlowProject = new AddFlowProject(sqlConnection);
            addFlowProject.Text = "添加工序方案";
            addFlowProject.m_OperateFlag = 1;
            if (addFlowProject.ShowDialog(this) == DialogResult.OK)
            {
                treeView1.Nodes.Clear();
                dataSet.Tables["FlowProject"].Clear();
                dataSet.Tables["FlowProjectDetailsView"].Clear();
                technologyLib.GetFlowProjectData(dataSet);
                technologyLib.GetFlowProjectDetailsViewData(dataSet);
                InitialTreeView(treeView1);
                listView1.Items.Clear();
                AddItem_FlowProject();
            }
        }

        //修改工序方案
        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                AddFlowProject addFlowProject = new AddFlowProject(sqlConnection);
                addFlowProject.Text = "修改工序方案";
                addFlowProject.m_OperateFlag = 2;
                addFlowProject.FlowProjectName = listView1.SelectedItems[0].SubItems[1].Text;
                if (addFlowProject.ShowDialog(this) == DialogResult.OK)
                {
                    treeView1.Nodes.Clear();
                    dataSet.Tables["FlowProject"].Clear();
                    dataSet.Tables["FlowProjectDetailsView"].Clear();
                    technologyLib.GetFlowProjectData(dataSet);
                    technologyLib.GetFlowProjectDetailsViewData(dataSet);
                    InitialTreeView(treeView1);
                    listView1.Items.Clear();
                    AddItem_FlowProject();
                }
            }
            else
            {
                MessageBox.Show("请选择要修改的工序方案!");
            }
            
        }

        //删除工序方案
        private void ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                DeleteForm deleteForm = new DeleteForm(sqlConnection);
                deleteForm.Text = "删除工序方案";
                deleteForm.DeleteFlag = 5;
                deleteForm.FlowProjectName = listView1.SelectedItems[0].SubItems[1].Text;
                if (deleteForm.ShowDialog(this) == DialogResult.OK)
                {
                    treeView1.Nodes.Clear();
                    dataSet.Tables["FlowProject"].Clear();
                    dataSet.Tables["FlowProjectDetailsView"].Clear();
                    technologyLib.GetFlowProjectData(dataSet);
                    technologyLib.GetFlowProjectDetailsViewData(dataSet);
                    InitialTreeView(treeView1);
                    listView1.Items.Clear();
                    AddItem_FlowProject();
                }
            }
            else
            {
                MessageBox.Show("请选择要删除的工序方案!");
            }
        }

        //添加、移除工序
        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddFlowProjectDetails addFlowProjectDetails = new AddFlowProjectDetails(sqlConnection);
            addFlowProjectDetails.FlowProjectName = treeView1.SelectedNode.Text.Split(new char[] { '.' })[1];
            string flowProjectName = treeView1.SelectedNode.Text.Split(new char[] { '.' })[1];
            if (addFlowProjectDetails.ShowDialog(this) == DialogResult.OK)
            {
                listView1.Items.Clear();
                dataSet.Tables["FlowProject"].Clear();
                dataSet.Tables["FlowProjectDetailsView"].Clear();
                technologyLib.GetFlowProjectData(dataSet);
                technologyLib.GetFlowProjectDetailsViewData(dataSet);
                AddItem_FlowProjectDetails(flowProjectName);
            }
        }
    }
}
