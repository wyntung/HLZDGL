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
    public partial class AddFlowProjectDetails : Form
    {
        private SqlConnection sqlConnection;
        private DataSet dataSet;
        private TechnologyLib technologyLib;

        private int m_FlowProjectID;
        private string m_FlowProjectName;
        public string FlowProjectName
        {
            get
            {
                return m_FlowProjectName;
            }
            set
            {
                m_FlowProjectName = value;
            }
        }

        public struct FlowProjectDetails
        {
            public int flowProjectID;
            public int flowID;
            public int preFlowID;
            public int backFlowID;
            public int flowSerialNo;
        }
        
        public AddFlowProjectDetails(SqlConnection mainConnection)
        {
            InitializeComponent();
            sqlConnection = mainConnection;
        }

        private void AddFlowProjectDetails_Load(object sender, EventArgs e)
        {
            technologyLib = new TechnologyLib(sqlConnection);
            dataSet = new DataSet();
            technologyLib.GetFlowData(dataSet);
            technologyLib.GetFlowProjectDetailsViewData(dataSet, m_FlowProjectName);

            m_FlowProjectID = technologyLib.GetFlowProjectID(m_FlowProjectName);
            InitialListView1(listView1);
            InitialListView2(listView2);
        }

        //排序算法
        //根据数据库中调试方案的数据排出项目列表
        private ListView SortListView()
        {
            ListView myTempList = new ListView();

            for (int i = 0; i < dataSet.Tables["OneFlowProjectDetailsView"].Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem(dataSet.Tables["OneFlowProjectDetailsView"].Rows[i]["FlowSerialNo"].ToString());
                item.SubItems.Add(dataSet.Tables["OneFlowProjectDetailsView"].Rows[i]["FlowID"].ToString());
                item.SubItems.Add(dataSet.Tables["OneFlowProjectDetailsView"].Rows[i]["BackFlowID"].ToString());
                item.SubItems.Add(dataSet.Tables["OneFlowProjectDetailsView"].Rows[i]["PreFlowID"].ToString());
                item.SubItems.Add(dataSet.Tables["OneFlowProjectDetailsView"].Rows[i]["FlowName"].ToString());
                item.SubItems.Add(dataSet.Tables["OneFlowProjectDetailsView"].Rows[i]["PreFlowName"].ToString());
                item.SubItems.Add(dataSet.Tables["OneFlowProjectDetailsView"].Rows[i]["BackFlowName"].ToString());
                item.SubItems.Add(dataSet.Tables["OneFlowProjectDetailsView"].Rows[i]["FlowProjectID"].ToString());
                item.SubItems.Add(dataSet.Tables["OneFlowProjectDetailsView"].Rows[i]["FlowProjectName"].ToString());
                myTempList.Items.AddRange(new ListViewItem[] { item });
            }

            return myTempList;
        }

        //初始化ListView1(已经选好的工序)
        private void InitialListView1(ListView pListView)
        {
            ListView myTempListView = SortListView();
            for (int i = 0; i < myTempListView.Items.Count; i++)
            {
                ListViewItem item = new ListViewItem(myTempListView.Items[i].SubItems[1].Text);
                item.SubItems.Add(myTempListView.Items[i].SubItems[4]);
                pListView.Items.AddRange(new ListViewItem[] { item });
            }
        }

        //初始化ListView2（没有被选择的工序）
        private void InitialListView2(ListView pListView)
        {
            for (int i = 0; i < dataSet.Tables["Flow"].Rows.Count; i++)
            {
                bool rc = true;
                int flowID1 = Convert.ToInt16(dataSet.Tables["Flow"].Rows[i]["FlowID"]);
                for (int j = 0; j < dataSet.Tables["OneFlowProjectDetailsView"].Rows.Count; j++)
                {
                    int flowID2 = Convert.ToInt16(dataSet.Tables["OneFlowProjectDetailsView"].Rows[j]["FlowID"]);
                    if (flowID1 == flowID2)
                    {
                        rc = false;
                        break;
                    }
                }
                if (rc)
                {
                    ListViewItem item = new ListViewItem(dataSet.Tables["Flow"].Rows[i]["FlowID"].ToString());
                    item.SubItems.Add(dataSet.Tables["Flow"].Rows[i]["FlowName"].ToString());
                    pListView.Items.AddRange(new ListViewItem[] { item });
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count != 0)
            {
                string itemColumnHead1 = listView2.SelectedItems[0].SubItems[0].Text;
                string itemColumnHead2 = listView2.SelectedItems[0].SubItems[1].Text;
                ListViewItem item = new ListViewItem(itemColumnHead1);
                item.SubItems.Add(itemColumnHead2);
                if (listView1.SelectedItems.Count != 0)
                {
                    int selectItemIndex = listView1.SelectedItems[0].Index;
                    listView1.Items.Insert(selectItemIndex, item);
                }
                else
                {
                    listView1.Items.Add(item);
                }
                listView2.SelectedItems[0].Remove();
            }
            else
            {
                MessageBox.Show("请选择要插入的工序！");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                string itemColumnHead1 = listView1.SelectedItems[0].SubItems[0].Text;
                string itemColumnHead2 = listView1.SelectedItems[0].SubItems[1].Text;
                ListViewItem item = new ListViewItem(itemColumnHead1);
                item.SubItems.Add(itemColumnHead2);

                bool rc = true;
                for (int i = 0; i < listView2.Items.Count; i++)
                {
                    int flowID = Convert.ToInt16(listView2.Items[i].SubItems[0].Text);
                    if (flowID > Convert.ToInt16(itemColumnHead1))
                    {
                        listView2.Items.Insert(i, item);
                        rc = false;
                        break;
                    }
                }
                if (rc)
                {
                    listView2.Items.Add(item);
                }

                listView1.SelectedItems[0].Remove();
            }
            else
            {
                MessageBox.Show("请选择要删除的工序！");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int totalCount = listView1.Items.Count;
            FlowProjectDetails[] flowProjectDetails = new FlowProjectDetails[totalCount];
            if (totalCount != 0)
            {
                if (totalCount == 1)
                {
                    //工序方案中只有一个工序
                    flowProjectDetails[0].flowID = Convert.ToInt16(listView1.Items[0].SubItems[0].Text);
                    flowProjectDetails[0].preFlowID = -1;
                    flowProjectDetails[0].backFlowID = -2;
                    flowProjectDetails[0].flowProjectID = m_FlowProjectID;
                    flowProjectDetails[0].flowSerialNo = 1;
                }
                else if (totalCount == 2)
                {
                    flowProjectDetails[0].flowID = Convert.ToInt16(listView1.Items[0].SubItems[0].Text);
                    flowProjectDetails[0].preFlowID = -1;
                    flowProjectDetails[0].backFlowID = Convert.ToInt16(listView1.Items[1].SubItems[0].Text);
                    flowProjectDetails[0].flowProjectID = m_FlowProjectID;
                    flowProjectDetails[0].flowSerialNo = 1;

                    flowProjectDetails[1].flowID = Convert.ToInt16(listView1.Items[1].SubItems[0].Text);
                    flowProjectDetails[1].preFlowID = Convert.ToInt16(listView1.Items[0].SubItems[0].Text);
                    flowProjectDetails[1].backFlowID = -2;
                    flowProjectDetails[1].flowProjectID = m_FlowProjectID;
                    flowProjectDetails[1].flowSerialNo = 2;
                }
                else
                {
                    flowProjectDetails[0].flowID = Convert.ToInt16(listView1.Items[0].SubItems[0].Text);
                    flowProjectDetails[0].preFlowID = -1;
                    flowProjectDetails[0].backFlowID = Convert.ToInt16(listView1.Items[1].SubItems[0].Text);
                    flowProjectDetails[0].flowProjectID = m_FlowProjectID;
                    flowProjectDetails[0].flowSerialNo = 1;

                    for (int i = 1; i < totalCount - 1; i++)
                    {
                        flowProjectDetails[i].flowID = Convert.ToInt16(listView1.Items[i].SubItems[0].Text);
                        flowProjectDetails[i].preFlowID = Convert.ToInt16(listView1.Items[i - 1].SubItems[0].Text);
                        flowProjectDetails[i].backFlowID = Convert.ToInt16(listView1.Items[i + 1].SubItems[0].Text);
                        flowProjectDetails[i].flowProjectID = m_FlowProjectID;
                        flowProjectDetails[i].flowSerialNo = i + 1;
                    }
                    flowProjectDetails[totalCount - 1].flowID = Convert.ToInt16(listView1.Items[totalCount - 1].SubItems[0].Text);
                    flowProjectDetails[totalCount - 1].preFlowID = Convert.ToInt16(listView1.Items[totalCount - 2].SubItems[0].Text);
                    flowProjectDetails[totalCount - 1].backFlowID = -2;
                    flowProjectDetails[totalCount - 1].flowProjectID = m_FlowProjectID;
                    flowProjectDetails[totalCount - 1].flowSerialNo = totalCount;
                }

                technologyLib.DeleteFlowProjectDetailsData(m_FlowProjectID);

                string mySelectQuery = "SELECT * FROM FlowProjectDetails";
                string myTableName = "FlowProjectDetails";
                for (int i = 0; i < totalCount; i++)
                {
                    CreateCmdsAndUpdate(mySelectQuery, myTableName, flowProjectDetails[i].flowID, flowProjectDetails[i].preFlowID,
                         flowProjectDetails[i].backFlowID, flowProjectDetails[i].flowProjectID, flowProjectDetails[i].flowSerialNo);
                }
            }
            else
            {
                //删除数据库中当前工序方案细节
                technologyLib.DeleteFlowProjectDetailsData(m_FlowProjectID);
            }
        }

        //新建一行数据
        public DataSet CreateCmdsAndUpdate(string mySelectQuery, string myTableName,
            int pFlowID, int pPreFlowID, int pBackFlowID, int pFlowProjectID, int pFlowSerialNo)
        {
            SqlDataAdapter myDataAdapter = new SqlDataAdapter();
            myDataAdapter.SelectCommand = new SqlCommand(mySelectQuery, sqlConnection);
            SqlCommandBuilder custCB = new SqlCommandBuilder(myDataAdapter);

            DataSet custDS = new DataSet();
            myDataAdapter.Fill(custDS);
            try
            {
                DataRow myDataRow;
                myDataRow = custDS.Tables[0].NewRow();
                myDataRow["FlowID"] = pFlowID;
                myDataRow["PreFlowID"] = pPreFlowID;
                myDataRow["BackFlowID"] = pBackFlowID;
                myDataRow["FlowProjectID"] = pFlowProjectID;
                myDataRow["FlowSerialNo"] = pFlowSerialNo;
                custDS.Tables[0].Rows.Add(myDataRow);

                //Without the OleDbCommandBuilder this line would fail
                myDataAdapter.Update(custDS);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
            }

            return custDS;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            this.button2_Click(sender,e);
        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            this.button1_Click(sender,e);
        }
    }
}
