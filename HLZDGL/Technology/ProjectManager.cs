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
    public partial class ProjectManager : Form
    {
        private SqlConnection sqlConnection;
        private DataSet dataSet;
        private TechnologyLib technologyLib;

        int projectID = 0;
        int itemID = 0;
        int selectedRowIndex = 0;
        int selectedRowIndex2 = 0;
        
        public ProjectManager(SqlConnection mainConnection)
        {
            InitializeComponent();
            sqlConnection = mainConnection;
        }

        private void ProjectManager_Load(object sender, EventArgs e)
        {
            dataSet = new DataSet();
            technologyLib = new TechnologyLib(sqlConnection);
            InitialForm(dataSet);
        }

        public void InitialForm(DataSet ds)
        {
            technologyLib.GetDebugItemProjectData(ds);
            dataGridView1.DataSource = ds.Tables["DebugItemProject"];
        }

        private void tabControl1_Layout(object sender, LayoutEventArgs e)
        {
            InitialForm(dataSet);
            //dataGridView1.Rows[0].Selected = false;
            //dataGridView1.Rows[selectedRowIndex].Selected = true;
        }

        private void tabPage2_Layout(object sender, LayoutEventArgs e)
        {
            dataSet = new DataSet();
            GetOneProjectDebugItemData(dataSet, projectID);
            dataGridView2.DataSource = dataSet.Tables["OneProjectDebugItem"];
            //if (dataGridView2.Rows.Count > 0)
            //{
            //    dataGridView2.Rows[0].Selected = false;
            //    dataGridView2.Rows[selectedRowIndex].Selected = true;
            //}
            //else
            //{
            //    return;
            //}
        }

        private void tabPage3_Layout(object sender, LayoutEventArgs e)
        {
            dataSet = new DataSet();
            GetOneDebugCmdItemData(dataSet, itemID);
            dataGridView3.DataSource = dataSet.Tables["OneDebugCmdItem"];
        }

        public void GetOneProjectDebugItemData(DataSet ds, int projectID)
        {
            string strSql = string.Format("SELECT d.DebugItemID,p.ItemSerialNo,d.DebugItemName,t.ProjectTypeName,d.StandardValue,d.Description FROM DebugItemProject a,DebugItem d,DebugItemProjectDetails p,DebugItemProjectType t WHERE d.DebugItemID = p.ItemID AND a.ProjectID = p.ProjectID AND d.DebugItemTypeID = t.ProjectType AND a.ProjectID = {0}", projectID);
            SqlCommand cmd = new SqlCommand(strSql, sqlConnection);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(ds, "OneProjectDebugItem");
        }

        public void GetOneDebugCmdItemData(DataSet ds, int itemID)
        {
            string strSql = string.Format("SELECT t.CmdID AS '命令编码',t.CmdName AS '命令名称',t.Description AS '描述',p.ProtocalName AS '规约名称',c.CommPortName AS '通讯端口名称',t.ExWaitTime AS '通讯延时' FROM CmdItem t, CommPort c,Protocal p, DebugItemDetails d WHERE t.CmdID = d.CmdID AND t.CommProtocalID = p.ProtocalID AND t.CommPortID = c.CommPortID AND d.DebugItemID = {0}", itemID);
            SqlCommand cmd = new SqlCommand(strSql,sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(ds, "OneDebugCmdItem");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            projectID = Convert.ToInt16(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            selectedRowIndex = dataGridView1.CurrentRow.Index;
            this.tabControl1.SelectedTab = tabPage2;
            //dataGridView1.Rows[e.RowIndex].Selected = true;
            dataSet = new DataSet();
            GetOneProjectDebugItemData(dataSet, projectID);
            dataGridView2.DataSource = dataSet.Tables["OneProjectDebugItem"];
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            itemID = Convert.ToInt16(dataGridView2.CurrentRow.Cells[0].Value.ToString());
            selectedRowIndex2 = dataGridView2.CurrentRow.Index;
            this.tabControl1.SelectedTab = tabPage3;
            //dataGridView1.Rows[e.RowIndex].Selected = true;
            dataSet = new DataSet();
            GetOneDebugCmdItemData(dataSet, itemID);
            dataGridView3.DataSource = dataSet.Tables["OneDebugCmdItem"];
        }
    }
}
