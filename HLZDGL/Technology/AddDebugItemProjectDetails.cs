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
    public partial class AddDebugItemProjectDetails : Form
    {
        private SqlConnection sqlConnection;
        private DataSet dataSet;
        private TechnologyLib technologyLib;

        private int m_DebugItemProjectID;
        private string m_DebugItemProjectName;
        public string DebugItemProjectName
        {
            get
            {
                return m_DebugItemProjectName;
            }
            set
            {
                m_DebugItemProjectName = value;
            }
        }

        public struct DebugItemProjectDetails
        {
            public int projectID;
            public int itemID;
            public int itemSerialNo;
        }

        public AddDebugItemProjectDetails(SqlConnection mainConnection)
        {
            InitializeComponent();
            sqlConnection = mainConnection;
        }

        private void AddDebugItemProjectDetails_Load(object sender, EventArgs e)
        {
            technologyLib = new TechnologyLib(sqlConnection);
            dataSet = new DataSet();
            technologyLib.GetDebugItemData(dataSet);
            technologyLib.GetDebugItemProjectDetailsViewData(dataSet, DebugItemProjectName);
            m_DebugItemProjectID = technologyLib.GetDebugItemProjectID(DebugItemProjectName);
            InitialListView1(listView1);
            InitialListView2(listView2);
        }

        /// <summary>
        /// 所有的调试项目列表
        /// </summary>
        /// <param name="listView1"></param>
        private void InitialListView1(ListView listView1)
        {
            for (int i = 0; i < dataSet.Tables["DebugItem"].Rows.Count; i++)
            {
                ListViewItem listViewItem = new ListViewItem(dataSet.Tables["DebugItem"].Rows[i]["DebugItemID"].ToString());
                listViewItem.SubItems.Add(dataSet.Tables["DebugItem"].Rows[i]["DebugItemName"].ToString());
                listView1.Items.AddRange(new ListViewItem[] { listViewItem });
            }
        }

        /// <summary>
        /// 已选的调试项目列表
        /// </summary>
        /// <param name="listView2"></param>
        private void InitialListView2(ListView listView2)
        {
            for (int i = 0; i < dataSet.Tables["OneDebugItemProjectDetailsView"].Rows.Count; i++)
            {
                ListViewItem listViewItem = new ListViewItem(dataSet.Tables["OneDebugItemProjectDetailsView"].Rows[i]["DebugItemID"].ToString());
                listViewItem.SubItems.Add(dataSet.Tables["OneDebugItemProjectDetailsView"].Rows[i]["DebugItemName"].ToString());
                listView2.Items.AddRange(new ListViewItem[] { listViewItem });
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count != 0)
            {
                string itemColumnHead1 = listView1.SelectedItems[0].SubItems[0].Text;
                string itemColumnHead2 = listView1.SelectedItems[0].SubItems[1].Text;
                ListViewItem item = new ListViewItem(itemColumnHead1);
                item.SubItems.Add(itemColumnHead2);

                if (listView2.SelectedItems.Count !=0)
                {
                    int selectItemIndex = listView2.SelectedItems[0].Index;
                    listView2.Items.Insert(selectItemIndex,item);
                }
                else
                {
                    listView2.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("请选择要插入的调试项目！");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count != 0)
            {
                listView2.SelectedItems[0].Remove();
            }
            else
            {
                MessageBox.Show("请选择要删除的调试项目！");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int count = listView2.SelectedItems.Count;
            DebugItemProjectDetails[] debugItemProjectDetails = new DebugItemProjectDetails[count];

            if (count != 0)
            {
                for (int i = 0; i < count;i++ )
                {
                    debugItemProjectDetails[i].itemSerialNo = i + 1;
                    debugItemProjectDetails[i].itemID = Convert.ToInt32(listView2.Items[i].SubItems[0].Text);
                    debugItemProjectDetails[i].projectID = m_DebugItemProjectID;
                }

                technologyLib.DeleteDebugItemProjectDetailsData(m_DebugItemProjectID);

                string sqlStr = string.Format("SELECT * FROM DebugItemProjectDetails");
                string tableName = "DebugItemProjectDetails";
                for (int i = 0; i < count;i++ )
                {
                    CreateCmdsAndUpdate(sqlStr, tableName, debugItemProjectDetails[i].itemSerialNo,debugItemProjectDetails[i].itemID,debugItemProjectDetails[i].projectID);
                }
            }
            else
            {
                technologyLib.DeleteDebugItemProjectDetailsData(m_DebugItemProjectID);
            }
        }

        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="pSelectQuery"></param>
        /// <param name="pTableName"></param>
        /// <param name="pItemSerialNo"></param>
        /// <param name="pItemID"></param>
        /// <param name="pProjectID"></param>
        /// <returns></returns>
        public DataSet CreateCmdsAndUpdate(string pSelectQuery, string pTableName, int pItemSerialNo, int pItemID, int pProjectID)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(pSelectQuery, sqlConnection);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            try
            {
                DataRow dataRow = ds.Tables[0].NewRow();
                dataRow["pItemSerialNo"] = pItemSerialNo;
                dataRow["pItemID"] = pItemID;
                dataRow["pProjectID"] = pProjectID;
                ds.Tables[0].Rows.Add(dataRow);
                adapter.Update(ds);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return ds;
        }

    }
}
