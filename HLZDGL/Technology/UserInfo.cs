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
    public partial class UserInfo : Form
    {
        private SqlConnection sqlConnection;
        private DataSet dataSet;
        private TechnologyLib technologyLib;
        private ListViewColumnSorter listViewColumnSorter;

        private int UserGrp_Columns = 5;
        private int UserInfo_Columns = 11;
        private int[] UserGrp_Widths = new int[] { 4, 10, 8, 10, 10 };
        private int[] UserInfo_Widths = new int[] { 3, 6, 6, 0, 2, 6, 6, 6, 4, 6, 3 };
        private string[] UserGrpDetails = new string[] { "用户组ID", "用户组名称", "用户组角色", "用户组权限", "描述" };
        private string[] UserInfoDetails = new string[] { "用户ID", "条码", "用户名", "密码", "性别", "生日", "电话", "描述", "用户组ID", "用户组名称", "状态" };

        public UserInfo(SqlConnection mainConnection)
        {
            InitializeComponent();
            sqlConnection = mainConnection;
            TechnologyLib technologyLib = new TechnologyLib(sqlConnection);
            listViewColumnSorter = new ListViewColumnSorter();
            this.listView1.ListViewItemSorter = listViewColumnSorter;
        }

        private void UserInfo_Load(object sender, EventArgs e)
        {
            TechnologyLib technologyLib = new TechnologyLib(sqlConnection);
            dataSet = new DataSet();
            technologyLib.GetUserGrpDetailsViewData(dataSet);
            technologyLib.GetUserInfoDetailsViewData(dataSet);
            InitialTreeView(treeView1);
        }

        private void InitialTreeView(TreeView myTreeView)
        {
            TreeNode newNode;	//根节点
            newNode = new TreeNode("系统制造部");
            newNode.ImageIndex = 0;
            newNode.SelectedImageIndex = 0;
            myTreeView.Nodes.Add(newNode);

            TreeNode newNode1;
            for (int i = 0; i < dataSet.Tables["UserGrpDetailsView"].Rows.Count; i++)
            {
                int groupid = Convert.ToInt16(dataSet.Tables["UserGrpDetailsView"].Rows[i]["UserGrpID"]);
                newNode1 = new TreeNode(dataSet.Tables["UserGrpDetailsView"].Rows[i]["UserGrpName"].ToString());
                myTreeView.Nodes[0].Nodes.Add(newNode1);
                int index1 = myTreeView.Nodes[0].Nodes.IndexOf(newNode1);

                TreeNode newNode2;
                for (int j = 0; j < dataSet.Tables["UserInfoDetailsView"].Rows.Count; j++)
                {
                    if (groupid == Convert.ToInt16(dataSet.Tables["UserInfoDetailsView"].Rows[j]["UserGrpID"]))
                    {
                        newNode2 = new TreeNode(dataSet.Tables["UserInfoDetailsView"].Rows[j]["BarCode"].ToString());
                        myTreeView.Nodes[0].Nodes[index1].Nodes.Add(newNode2);
                    }
                }
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
                case 0:	//第一级节点:显示用户组
                    listView1.Clear();
                    InsertAllColumns(0);
                    AddItem_UserGrp();
                    break;
                case 1:	//第二级节点:显示用户信息
                    listView1.Clear();
                    InsertAllColumns(1);
                    AddItem_UserInfo(treeView1.SelectedNode.Text);
                    break;
                case 2:	//第三级节点:显示单个用户信息
                    listView1.Clear();
                    InsertAllColumns(2);
                    AddItem_OneUserInfo(treeView1.SelectedNode.Text);
                    break;
            }
        }

        private void InsertAllColumns(int flag)
        {
            int columns = 0;
            int[] widths = new int[20];
            string[] columnHeads = new string[20];

            switch (flag)
            {
                case 0:
                    // Add columns and set their text.
                    columns = UserGrp_Columns;
                    UserGrp_Widths.CopyTo(widths, 0);
                    UserGrpDetails.CopyTo(columnHeads, 0);
                    break;
                case 1:
                    columns = UserInfo_Columns;
                    UserInfo_Widths.CopyTo(widths, 0);
                    UserInfoDetails.CopyTo(columnHeads, 0);
                    break;
                case 2:
                    columns = UserInfo_Columns;
                    UserInfo_Widths.CopyTo(widths, 0);
                    UserInfoDetails.CopyTo(columnHeads, 0);
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

        //显示UserGrp数据项到listView1中
        private void AddItem_UserGrp()
        {
            for (int i = 0; i < dataSet.Tables["UserGrpDetailsView"].Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem(dataSet.Tables["UserGrpDetailsView"].Rows[i][0].ToString());
                item.ImageIndex = 1;
                for (int j = 1; j < UserGrp_Columns; j++)
                    item.SubItems.Add(dataSet.Tables["UserGrpDetailsView"].Rows[i][j].ToString());
                listView1.Items.AddRange(new ListViewItem[] { item });
            }

        }

        //显示UserInfo数据项到listView1中
        private void AddItem_UserInfo(string pUserGrpName)
        {
            for (int i = 0; i < dataSet.Tables["UserInfoDetailsView"].Rows.Count; i++)
            {
                if (pUserGrpName == dataSet.Tables["UserInfoDetailsView"].Rows[i]["UserGrpName"].ToString())
                {
                    ListViewItem item = new ListViewItem(dataSet.Tables["UserInfoDetailsView"].Rows[i][0].ToString());
                    if (dataSet.Tables["UserInfoDetailsView"].Rows[i]["Status"].ToString() == "在职")
                        item.ImageIndex = 2;
                    else
                        item.ImageIndex = 3;

                    for (int j = 1; j < UserInfo_Columns; j++)
                    {
                        item.SubItems.Add(dataSet.Tables["UserInfoDetailsView"].Rows[i][j].ToString());
                    }
                    listView1.Items.AddRange(new ListViewItem[] { item });
                }
            }
        }

        //显示单个UserInfo数据项到listView1中
        private void AddItem_OneUserInfo(string pBarCode)
        {
            for (int i = 0; i < dataSet.Tables["UserInfoDetailsView"].Rows.Count; i++)
            {
                if (pBarCode == dataSet.Tables["UserInfoDetailsView"].Rows[i]["BarCode"].ToString())
                {
                    ListViewItem item = new ListViewItem(dataSet.Tables["UserInfoDetailsView"].Rows[i][0].ToString());
                    if (dataSet.Tables["UserInfoDetailsView"].Rows[i]["Status"].ToString() == "在职")
                        item.ImageIndex = 2;
                    else
                        item.ImageIndex = 3;

                    for (int j = 1; j < UserInfo_Columns; j++)
                    {
                        item.SubItems.Add(dataSet.Tables["UserInfoDetailsView"].Rows[i][j].ToString());
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
                case 0:	//第一级节点:选中根节点
                    contextMenuStrip1.Items[0].Visible = false;
                    contextMenuStrip1.Items[1].Visible = false;
                    contextMenuStrip1.Items[2].Visible = false;
                    contextMenuStrip1.Items[3].Visible = false;
                    contextMenuStrip1.Items[4].Visible = false;
                    contextMenuStrip1.Items[5].Visible = false;
                    break;
                case 1:	//第二级节点:选中用户组
                    contextMenuStrip1.Items[0].Visible = false;
                    contextMenuStrip1.Items[1].Visible = false;
                    contextMenuStrip1.Items[2].Visible = false;
                    contextMenuStrip1.Items[3].Visible = true;
                    contextMenuStrip1.Items[4].Visible = true;
                    contextMenuStrip1.Items[5].Visible = true;
                    break;
                case 2:	//第三级节点:选中用户
                    contextMenuStrip1.Items[0].Visible = false;
                    contextMenuStrip1.Items[1].Visible = false;
                    contextMenuStrip1.Items[2].Visible = false;
                    contextMenuStrip1.Items[3].Visible = false;
                    contextMenuStrip1.Items[4].Visible = true;
                    contextMenuStrip1.Items[5].Visible = true;
                    break;
            }
        }

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
                //设置缺省排序为升序
                listViewColumnSorter.SortColumn = e.Column;
                listViewColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
            }
            this.listView1.Sort(); 
        }

        //添加用户组
        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            treeView1.PathSeparator = "/";
            string path = treeView1.SelectedNode.FullPath.ToString();
            int count = 0;
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i].CompareTo('/') == 0)
                    count++;
            }

            AddUserGroup addUserGroup = new AddUserGroup(sqlConnection);
            AddUser addUser = new AddUser(sqlConnection);
            string userGrpName = "";
            switch (count)
            {
                case 0:	//第一级节点:添加用户组
                    addUserGroup.Text = "添加用户组";
                    addUserGroup.m_OperateFlag = 1;
                    if (addUserGroup.ShowDialog(this) == DialogResult.OK)
                    {
                        //添加用户组配置到数据库中
                        dataSet.Tables["UserGrpDetailsView"].Clear();
                        dataSet.Tables["UserInfoDetailsView"].Clear();
                        treeView1.Nodes.Clear();
                        listView1.Clear();

                        technologyLib.GetUserGrpDetailsViewData(dataSet);
                        technologyLib.GetUserInfoDetailsViewData(dataSet);

                        InitialTreeView(treeView1);
                        InsertAllColumns(0);
                        AddItem_UserInfo(userGrpName);
                    }
                    break;
            }
        }

        //删除用户组
        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                DeleteForm deleteForm = new DeleteForm(sqlConnection);
                deleteForm.Text = "删除用户组";
                deleteForm.DeleteFlag = 2;
                deleteForm.UserGrpName = listView1.SelectedItems[0].SubItems[1].Text;
                if (deleteForm.ShowDialog(this) == DialogResult.OK)
                {
                    dataSet.Tables["UserGrpDetailsView"].Clear();
                    dataSet.Tables["UserInfoDetailsView"].Clear();
                    treeView1.Nodes.Clear();

                    technologyLib.GetUserGrpDetailsViewData(dataSet);
                    technologyLib.GetUserInfoDetailsViewData(dataSet);

                    InitialTreeView(treeView1);
                    listView1.Clear();
                    InsertAllColumns(0);
                    AddItem_UserGrp();
                }
            }
            else
            {
                MessageBox.Show("请选择要删除的用户组!");
            }
        }

        //用户组属性
        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            treeView1.PathSeparator = "/";
            string path = treeView1.SelectedNode.FullPath.ToString();
            int count = 0;
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i].CompareTo('/') == 0)
                    count++;
            }

            if (listView1.SelectedItems.Count != 0)
            {
                AddUserGroup addUserGroup = new AddUserGroup(sqlConnection);
                AddUser addUser = new AddUser(sqlConnection);
                string userGrpName = "";
                switch (count)
                {
                    case 0:	//第一级节点:用户组属性
                        addUserGroup.UserGrpName = listView1.SelectedItems[0].SubItems[1].Text;
                        addUserGroup.Text = "用户组属性";
                        addUserGroup.m_OperateFlag = 2;
                        if (addUserGroup.ShowDialog(this) == DialogResult.OK)
                        {
                            //添加用户组配置到数据库中
                            dataSet.Tables["UserGrpDetailsView"].Clear();
                            dataSet.Tables["UserInfoDetailsView"].Clear();
                            treeView1.Nodes.Clear();
                            listView1.Clear();

                            //technologyLib.GetUserGrpDetailsViewData(dataSet);
                            technologyLib.GetUserInfoDetailsViewData(dataSet);

                            InitialTreeView(treeView1);
                            InsertAllColumns(0);
                            AddItem_UserInfo(userGrpName);
                        }
                        break;
                }
            }
            else
            {
                MessageBox.Show("请选择对应的用户组!");
            }
        }

        //添加用户
        private void ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            treeView1.PathSeparator = "/";
            string path = treeView1.SelectedNode.FullPath.ToString();
            int count = 0;
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i].CompareTo('/') == 0)
                    count++;
            }

            AddUserGroup addUserGroup = new AddUserGroup(sqlConnection);
            AddUser addUser = new AddUser(sqlConnection);
            string userGrpName = "";
            switch (count)
            {
                case 1:	//第二级节点：添加用户
                    addUser.UserGrpName = treeView1.SelectedNode.Text;
                    userGrpName = treeView1.SelectedNode.Text;
                    addUser.Text = "添加用户";
                    addUser.m_OperateFlag = 1;
                    if (addUser.ShowDialog(this) == DialogResult.OK)
                    {
                        dataSet.Tables["UserGrpDetailsView"].Clear();
                        dataSet.Tables["UserInfoDetailsView"].Clear();
                        treeView1.Nodes.Clear();

                        technologyLib.GetUserGrpDetailsViewData(dataSet);
                        technologyLib.GetUserInfoDetailsViewData(dataSet);

                        InitialTreeView(treeView1);
                        listView1.Clear();
                        InsertAllColumns(1);
                        AddItem_UserInfo(userGrpName);
                    }
                    break;
            }
        }

        //删除用户
        private void ToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            treeView1.PathSeparator = "/";
            string path = treeView1.SelectedNode.FullPath.ToString();
            int count = 0;
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i].CompareTo('/') == 0)
                    count++;
            }

            string userGrpName = "";
            DeleteForm deleteForm = new DeleteForm(sqlConnection);
            deleteForm.Text = "删除用户";
            deleteForm.DeleteFlag = 3;
            if (listView1.SelectedItems.Count != 0)
            {
                deleteForm.BarCode = listView1.SelectedItems[0].SubItems[1].Text;
                switch (count)
                {
                    case 1:	//第二级节点：用户属性
                        userGrpName = treeView1.SelectedNode.Text;
                        if (deleteForm.ShowDialog(this) == DialogResult.OK)
                        {
                            dataSet.Tables["UserGrpDetailsView"].Clear();
                            dataSet.Tables["UserInfoDetailsView"].Clear();
                            treeView1.Nodes.Clear();

                            //technologyLib.GetUserGrpDetailsViewData(dataSet);
                            technologyLib.GetUserInfoDetailsViewData(dataSet);

                            InitialTreeView(treeView1);
                            listView1.Clear();
                            InsertAllColumns(1);
                            AddItem_UserInfo(userGrpName);
                        }
                        break;
                    case 2: //第三级节点：单个用户属性
                        userGrpName = treeView1.SelectedNode.Parent.Text;
                        if (deleteForm.ShowDialog(this) == DialogResult.OK)
                        {
                            dataSet.Tables["UserGrpDetailsView"].Clear();
                            dataSet.Tables["UserInfoDetailsView"].Clear();
                            treeView1.Nodes.Clear();

                            //technologyLib.GetUserGrpDetailsViewData(dataSet);
                            technologyLib.GetUserInfoDetailsViewData(dataSet);

                            InitialTreeView(treeView1);
                            listView1.Clear();
                            InsertAllColumns(2);
                            AddItem_UserInfo(userGrpName);
                        }
                        break;
                }
            }
            else
            {
                MessageBox.Show("请选择要删除的用户!");
            }
        }

        //用户属性
        private void ToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            treeView1.PathSeparator = "/";
            string path = treeView1.SelectedNode.FullPath.ToString();
            int count = 0;
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i].CompareTo('/') == 0)
                    count++;
            }

            if (listView1.SelectedItems.Count != 0)
            {
                AddUserGroup addUserGroup = new AddUserGroup(sqlConnection);
                AddUser addUser = new AddUser(sqlConnection);
                string userGrpName = "";
                switch (count)
                {
                    case 1:	//第二级节点：用户属性
                        userGrpName = treeView1.SelectedNode.Text;
                        addUser.UserGrpName = treeView1.SelectedNode.Text;
                        addUser.BarCode = listView1.SelectedItems[0].SubItems[1].Text;
                        addUser.Text = "用户属性";
                        addUser.m_OperateFlag = 2;
                        if (addUser.ShowDialog(this) == DialogResult.OK)
                        {
                            dataSet.Tables["UserGrpDetailsView"].Clear();
                            dataSet.Tables["UserInfoDetailsView"].Clear();
                            treeView1.Nodes.Clear();

                            technologyLib.GetUserGrpDetailsViewData(dataSet);
                            technologyLib.GetUserInfoDetailsViewData(dataSet);

                            InitialTreeView(treeView1);
                            listView1.Clear();
                            InsertAllColumns(1);
                            AddItem_UserInfo(userGrpName);
                        }
                        break;
                    case 2: //第三级节点：单个用户属性
                        userGrpName = treeView1.SelectedNode.Parent.Text;
                        addUser.UserGrpName = treeView1.SelectedNode.Parent.Text;
                        addUser.BarCode = listView1.SelectedItems[0].SubItems[1].Text;
                        addUser.Text = "用户属性";
                        addUser.m_OperateFlag = 2;
                        if (addUser.ShowDialog(this) == DialogResult.OK)
                        {
                            dataSet.Tables["UserGrpDetailsView"].Clear();
                            dataSet.Tables["UserInfoDetailsView"].Clear();
                            treeView1.Nodes.Clear();

                            technologyLib.GetUserGrpDetailsViewData(dataSet);
                            technologyLib.GetUserInfoDetailsViewData(dataSet);

                            InitialTreeView(treeView1);
                            listView1.Clear();
                            InsertAllColumns(2);
                            AddItem_UserInfo(userGrpName);
                        }
                        break;
                }
            }
            else
            {
                MessageBox.Show("请选择相应的用户!");
            }
        }
    }
}
