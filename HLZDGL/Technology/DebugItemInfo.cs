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
    public partial class DebugItemInfo : Form
    {
        private SqlConnection sqlConnection;
        private DataSet dataSet;
        private TechnologyLib technologylib;

        private int[] DebugItem_Widths = new int[] { 5, 8, 8, 4, 10 };
        private int[] DebugItemDetails_Widths = new int[] { 8, 8, 12, 12 };
        private string[] DebugItem = new string[] { "调试项目ID", "调试项目名称", "标准值", "类型", "描述" };
        private string[] DebugItemDetails = new string[] { "调试命令序号", "调试命令ID", "调试命令名称", "描述" };
        private ListViewColumnSorter listViewColumnSorter;

        public DebugItemInfo(SqlConnection mainConnection)
        {
            InitializeComponent();
            sqlConnection = mainConnection;
            listViewColumnSorter = new ListViewColumnSorter();
            this.listView1.ListViewItemSorter = listViewColumnSorter;
        }

        private void DebugItemInfo_Load(object sender, EventArgs e)
        {
            technologylib = new TechnologyLib(sqlConnection);
            dataSet = new DataSet();
            technologylib.GetDebugItemViewData(dataSet);
            technologylib.GetDebugItemDetailsViewData(dataSet);
            InitialTreeView(treeView1);
        }

        /// <summary>
        /// 初始化调试项目树
        /// </summary>
        /// <param name="treeView"></param>
        private void InitialTreeView(TreeView treeView)
        {
            TreeNode treeNode = new TreeNode("调试项目名称");
            treeView.Nodes.Add(treeNode);
            for (int i = 0; i < dataSet.Tables["DebugItemView"].Rows.Count; i++)
            {
                treeNode = new TreeNode(string.Format("{0}.{1}", Convert.ToInt32(dataSet.Tables["DebugItemView"].Rows[i]["DebugItemID"]), dataSet.Tables["DebugItemView"].Rows[i]["DebugItemName"].ToString()));
                treeView.Nodes[0].Nodes.Add(treeNode);
            }
            treeView.Nodes[0].Expand();
            treeView.Nodes[0].TreeView.Focus();
        }

        /// <summary>
        /// 加载调试命令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            treeView1.PathSeparator = "/";
            string path = treeView1.SelectedNode.FullPath.ToString();
            int count = 0;
            for (int i = 0; i < path.Length;i++ )
            {
                if (path[i].CompareTo('/') == 0)
                {
                    count++;
                }
            }

            switch (count)
            {
                case 0:
                    listView1.Clear();
                    InsertAllColumns(0);
                    listView1.BeginUpdate();
                    AddItem_DebugItem();
                    listView1.EndUpdate();
                    break;
                case 1:
                    listView1.Clear();
                    InsertAllColumns(0);
                    listView1.BeginUpdate();
                    AddItem_DebugItemDetails(treeView1.SelectedNode.Text);
                    listView1.EndUpdate();
                    break;
            }
        }

        /// <summary>
        /// 初始化listView1需要显示的列名
        /// </summary>
        /// <param name="flag"></param>
        public void InsertAllColumns(int flag)
        {
            int columns = 0; //列数量
            int[] widths = new int[20]; //列宽度
            string[] columnHeads = new string[20]; //列名

            switch(flag)
            {
                case 0:
                    columns = DebugItem.Length;
                    DebugItem_Widths.CopyTo(widths,0);
                    DebugItem.CopyTo(columnHeads,0);
                    break;
                case 1:
                    columns = DebugItemDetails.Length;
                    DebugItemDetails_Widths.CopyTo(widths,0);
                    DebugItemDetails.CopyTo(columnHeads,0);
                    break;
            }

            for (int i = 0; i < columns;i++ )
            {
                this.listView1.Columns.Add(new ColumnHeader());
                this.listView1.Columns[i].Text = columnHeads[i];
                this.listView1.Columns[i].Width = widths[i] * 20;
            }
        }

        /// <summary>
        /// 显示DebugItem数据项到listView1中
        /// </summary>
        private void AddItem_DebugItem()
        {
            for (int i = 0; i < dataSet.Tables["DebugItemView"].Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem(dataSet.Tables["DebugItemView"].Rows[i][0].ToString());
                for (int j = 1; j < DebugItem.Length;j++ )
                {
                    item.SubItems.Add(dataSet.Tables["DebugItemView"].Rows[i][j].ToString());
                }
                listView1.Items.AddRange(new ListViewItem[]{item});
            }
        }

        /// <summary>
        /// 显示调试命令数据到listView1
        /// </summary>
        /// <param name="p"></param>
        private void AddItem_DebugItemDetails(string p)
        {
            string strVal = "";
            try 
            {
                strVal = p.Split(new char[] {'.'})[1];
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }
            for (int i = 0; i < dataSet.Tables["DebugItemDetailsView"].Rows.Count;i++ )
            {
                if (strVal == dataSet.Tables["DebugItemDetailsView"].Rows[i]["DebugItemName"].ToString())
                {
                    ListViewItem item = new ListViewItem(dataSet.Tables["DebugItemDetailsView"].Rows[i][0].ToString());
                    for (int j = 1; j < DebugItemDetails.Length;j++ )
                    {
                        item.SubItems.Add(dataSet.Tables["DebugItemDetailsView"].Rows[i][j].ToString());
                    }
                    listView1.Items.AddRange(new ListViewItem[] {item});
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
                if (listViewColumnSorter.Order == System.Windows.Forms.SortOrder.Ascending)
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
                listViewColumnSorter.SortColumn = e.Column;
                listViewColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
            }
            this.listView1.Sort();
        }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {

        }
    }
}
