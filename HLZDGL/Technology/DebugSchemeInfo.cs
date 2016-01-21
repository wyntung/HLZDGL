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
    public partial class DebugSchemeInfo : Form
    {
        private SqlConnection sqlConnection;
        private DataSet dataSet;
        private TechnologyLib technologyLib;

        private int debugItemProjectColumns = 3;
        private int debugItemProjectDetailsColumns = 6;
        private int[] debugItemProjectWidths = new int[] { 6, 10, 8 };
        private int[] debugItemProjectDetailsWidths = new int[] { 3, 3, 8, 4, 8, 8 };
        private string[] debugItemProject = new string[] { "调试方案编码", "调试方案名称", "描述" };
        private string[] debugItemProjectDetails = new string[] { "项目序号", "项目编码", "项目名称", "类型", "标准值", "描述" };
        private ListViewColumnSorter listViewColumnSorter;

        public DebugSchemeInfo(SqlConnection mainConnection)
        {
            InitializeComponent();
            sqlConnection = mainConnection;
            listViewColumnSorter = new ListViewColumnSorter();
            this.listView1.ListViewItemSorter = listViewColumnSorter;
        }

        private void DebugSchemeInfo_Load(object sender, EventArgs e)
        {
            technologyLib = new TechnologyLib(sqlConnection);
            dataSet = new DataSet();
            technologyLib.GetDebugItemProjectData(dataSet);
            technologyLib.GetDebugItemProjectDetailsViewData(dataSet);
            InitialTreeView(treeView1);
        }

        private void InitialTreeView(TreeView treeView)
        {
            TreeNode newNode = new TreeNode("调试方案名称");
            treeView.Nodes.Add(newNode);

            TreeNode newNode2;
            for (int i = 0; i < dataSet.Tables["DebugItemProject"].Rows.Count;i++ )
            {
                newNode2 = new TreeNode(string.Format("{0}.{1}", Convert.ToInt32(dataSet.Tables["DebugItemProject"].Rows[i]["ProjectID"]), dataSet.Tables["DebugItemProject"].Rows[i]["ProjectName"].ToString()));
                treeView.Nodes[0].Nodes.Add(newNode2);
            }
            treeView.Nodes[0].Expand();
            treeView.Nodes[0].TreeView.Focus();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            treeView1.PathSeparator = "/";
            string path = treeView1.SelectedNode.FullPath.ToString();
            int count = 0;

            for (int i = 0; i < path.Length;i++ )
            {
                if(path[i].CompareTo('/')==0)
                {
                    count++;
                }
            }

            switch(count)
            {
                case 0:
                    listView1.Clear();
                    InsertAllColumns(0);
                    AddItem_DebugItemProject();
                    break;
                case 1:
                    listView1.Clear();
                    InsertAllColumns(1);
                    AddItem_DebugItemProjectDetails(treeView1.SelectedNode.Text);
                    break;
            }
        }

        private void InsertAllColumns(int p)
        {
            int column = 0;
            int[] widths = new int[20];
            string[] columnHeads = new string[20];
            
            switch(p)
            {
                case 0:
                    column = debugItemProjectColumns;
                    debugItemProjectWidths.CopyTo(widths, 0);
                    debugItemProject.CopyTo(columnHeads, 0);
                    break;
                case 1:
                    column = debugItemProjectDetailsColumns;
                    debugItemProjectDetailsWidths.CopyTo(widths, 0);
                    debugItemProjectDetails.CopyTo(columnHeads, 0);
                    break;
            }

            for (int i = 0; i < column;i++ )
            {
                listView1.Columns.Add(new ColumnHeader());
                listView1.Columns[i].Text = columnHeads[i];
                listView1.Columns[i].Width = widths[i] * 20;
            }
        }

        /// <summary>
        /// 显示DebugItemProject数据项到listView1中
        /// </summary>
        private void AddItem_DebugItemProject()
        {
            for (int i = 0; i < dataSet.Tables["DebugItemProject"].Rows.Count;i++ )
            {
                ListViewItem item = new ListViewItem(dataSet.Tables["DebugItemProject"].Rows[i][0].ToString());

                for (int j = 1; j < debugItemProjectColumns;j++ )
                {
                    item.SubItems.Add(dataSet.Tables["DebugItemProject"].Rows[i][j].ToString());
                }
                listView1.Items.AddRange(new ListViewItem[] { item });
            }
        }

        /// <summary>
        /// 显示已选择调试方案的调试项目信息
        /// </summary>
        /// <param name="p"></param>
        private void AddItem_DebugItemProjectDetails(string p)
        {
            string strVal = "";
            try
            {
                strVal = p.Split(new char[] {'.'})[1];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }

            for (int i = 0; i < dataSet.Tables["DebugItemProjectDetailsView"].Rows.Count; i++)
            {
                if (strVal == dataSet.Tables["DebugItemProjectDetailsView"].Rows[i]["ProjectName"].ToString())
                {
                    ListViewItem listViewItem = new ListViewItem(dataSet.Tables["DebugItemProjectDetailsView"].Rows[i][0].ToString());

                    for (int j = 1; j < debugItemProjectDetailsColumns; j++ )
                    {
                        listViewItem.SubItems.Add(dataSet.Tables["DebugItemProjectDetailsView"].Rows[i][j].ToString());
                    }

                    listView1.Items.AddRange(new ListViewItem[] { listViewItem });
                }
            }
        }

        /// <summary>
        /// 添加/移除调试项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddDebugItemProjectDetails addDebugItemProjectDetails = new AddDebugItemProjectDetails(sqlConnection);
            addDebugItemProjectDetails.DebugItemProjectName = treeView1.SelectedNode.Text.Split(new char[] {'.'})[1];
            string debugItemProjectName = treeView1.SelectedNode.Text.Split(new char[] {'.'})[1];
            if (addDebugItemProjectDetails.ShowDialog(this)== DialogResult.OK)
            {
                listView1.Items.Clear();
                dataSet.Tables["DebugItemProject"].Clear();
                dataSet.Tables["DebugItemProjectDetailsView"].Clear();
                technologyLib.GetDebugItemProjectData(dataSet);
                technologyLib.GetDebugItemProjectDetailsViewData(dataSet);
                AddItem_DebugItemProjectDetails(debugItemProjectName);
            }
        }

        /// <summary>
        /// 添加调试方案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AddDebugItemProject addDebugItemProject = new AddDebugItemProject(sqlConnection);
            addDebugItemProject.Text = "添加调试方案";
            addDebugItemProject.m_OperateFlag = 1;

            if (addDebugItemProject.ShowDialog(this) == DialogResult.OK)
            {
                treeView1.Nodes.Clear();
                dataSet.Tables["DebugItemProject"].Clear();
                dataSet.Tables["DebugItemProjectDetailsView"].Clear();
                technologyLib.GetDebugItemData(dataSet);
                technologyLib.GetDebugItemDetailsViewData(dataSet);
                InitialTreeView(treeView1);
                listView1.Items.Clear();
                AddItem_DebugItemProject();
            }
        }

        /// <summary>
        /// 修改调试方案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                AddDebugItemProject addDebugItemProject = new AddDebugItemProject(sqlConnection);
                addDebugItemProject.Text = "修改调试方案";
                addDebugItemProject.m_OperateFlag = 2;
                addDebugItemProject.ProjectName = listView1.SelectedItems[0].SubItems[1].Text;

                if (addDebugItemProject.ShowDialog(this)==DialogResult.OK)
                {
                    treeView1.Nodes.Clear();
                    dataSet.Tables["DebugItemProject"].Clear();
                    dataSet.Tables["DebugItemProjectDetailsView"].Clear();
                    technologyLib.GetDebugItemProjectData(dataSet);
                    technologyLib.GetDebugItemProjectDetailsViewData(dataSet);
                    InitialTreeView(treeView1);
                    listView1.Items.Clear();
                    AddItem_DebugItemProject();
                }
            }
            else
            {
                MessageBox.Show("请选择要修改的调试方案！");
            }
        }

        /// <summary>
        /// 删除调试方案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count !=0)
            {
                DeleteForm deleteForm = new DeleteForm(sqlConnection);
                deleteForm.Text = "删除调试方案";
                deleteForm.DebugItemProjectName = listView1.SelectedItems[0].SubItems[1].Text;
                if(deleteForm.ShowDialog(this) == DialogResult.OK)
                {
                    treeView1.Nodes.Clear();
                    dataSet.Tables["DebugItemProject"].Clear();
                    dataSet.Tables["DebugItemProjectDetailsView"].Clear();
                    technologyLib.GetDebugItemData(dataSet);
                    technologyLib.GetDebugItemDetailsViewData(dataSet);
                    InitialTreeView(treeView1);
                    listView1.Items.Clear();
                    AddItem_DebugItemProject();
                }
            }
        }

        /// <summary>
        /// 列排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == listViewColumnSorter.SortColumn)
            {
                if(listViewColumnSorter.Order ==System.Windows.Forms.SortOrder.Ascending)
                {
                    listViewColumnSorter.Order = System.Windows.Forms.SortOrder.Descending;
                }
                else
                {
                    listViewColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                }
            }
            else
            {
                //设置缺省排序为升序
                listViewColumnSorter.SortColumn = e.Column;
                listViewColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
            }
            this.listView1.Sort();
        }

        private void listView1_MouseUp_1(object sender, MouseEventArgs e)
        {
            treeView1.PathSeparator = "/";
            string path = treeView1.SelectedNode.FullPath.ToString();
            int count = 0;
            for (int i = 0; i < path.Length;i++ )
            {
                if(path[i].CompareTo('/')==0)
                {
                    count++;
                }
            }

            switch (count)
            {
                //第一级节点，显示调试方案名称
                case 0:
                    contextMenuStrip1.Items[0].Visible = false;
                    contextMenuStrip1.Items[1].Visible = true;
                    contextMenuStrip1.Items[2].Visible = true;
                    contextMenuStrip1.Items[3].Visible = true;
                    break;
                //第二级节点，显示调试方案对应项目
                case 1:
                    contextMenuStrip1.Items[0].Visible = true;
                    contextMenuStrip1.Items[1].Visible = false;
                    contextMenuStrip1.Items[2].Visible = false;
                    contextMenuStrip1.Items[3].Visible = false;
                    break;
            }
        }
    }
}
