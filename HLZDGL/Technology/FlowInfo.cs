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
    public partial class FlowInfo : Form
    {
        private SqlConnection sqlConnection;
        private DataSet dataSet;
        private TechnologyLib technologyLib;
        
        public FlowInfo(SqlConnection mainConnection)
        {
            InitializeComponent();
            sqlConnection = mainConnection;
        }

        private void FlowInfo_Load(object sender, EventArgs e)
        {
            technologyLib = new TechnologyLib(sqlConnection);
            dataSet = new DataSet();
            technologyLib.GetFlowDetailsData(dataSet);
            InitialListView(listView1);
        }

        public void InitialListView(ListView listView)
        {
            listView.Clear();
            InsertAllColumns(listView);
            listView.BeginUpdate();
            AddFlowItem(listView);
            listView.EndUpdate();
        }

        public void InsertAllColumns(ListView listView)
        {
            listView.Columns.Add(this.columnHeader1);
            listView.Columns.Add(this.columnHeader2);
            listView.Columns.Add(this.columnHeader3);
        }

        public void AddFlowItem(ListView listView)
        {
            for (int i = 0; i < dataSet.Tables["FlowDetails"].Rows.Count;i++ )
            {
                ListViewItem item = new ListViewItem(dataSet.Tables["FlowDetails"].Rows[i][0].ToString());
                for (int j = 1; j < this.listView1.Columns.Count;j++ )
                {
                    item.SubItems.Add(dataSet.Tables["FlowDetails"].Rows[i][j].ToString());
                }
                listView.Items.AddRange(new ListViewItem[] { item });
            }
        }

        private void AddFlowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddFlow addFlow = new AddFlow(sqlConnection);
            addFlow.Text = "添加工序";
            addFlow.m_OperateFlag = 1;
            if (addFlow.ShowDialog(this) == DialogResult.OK)
            {
                dataSet.Tables["FlowDetails"].Clear();
                technologyLib.GetFlowDetailsData(dataSet);
                InitialListView(listView1);
            }

        }

        private void ModifyFlowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                AddFlow addFlow = new AddFlow(sqlConnection);
                addFlow.Text = "修改工序";
                addFlow.m_OperateFlag = 2;
                addFlow.FlowName = listView1.SelectedItems[0].SubItems[1].Text;
                if (addFlow.ShowDialog(this) == DialogResult.OK)
                {
                    dataSet.Tables["FlowDetails"].Clear();
                    technologyLib.GetFlowDetailsData(dataSet);
                    InitialListView(listView1);
                }
            }
            else
            {
                MessageBox.Show("请选择要修改的工序!");
            }
        }

        private void DeleteFlowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                DeleteForm deleteForm = new DeleteForm(sqlConnection);
                deleteForm.Text = "删除工序";
                deleteForm.DeleteFlag = 4;
                deleteForm.FlowName = listView1.SelectedItems[0].SubItems[1].Text;
                if (deleteForm.ShowDialog(this) == DialogResult.OK)
                {
                    dataSet.Tables["FlowDetails"].Clear();
                    technologyLib.GetFlowDetailsData(dataSet);
                    InitialListView(listView1);
                }
            }
            else
            {
                MessageBox.Show("请选择要删除的工序!");
            }
        }

    }
}
