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
    public partial class AddFlowProject : Form
    {
        private TechnologyLib technologyLib;
        private DataSet dataSet;
        private SqlConnection sqlConnection;
        private int m_FlowProjectID;
        public int m_OperateFlag = 1;	//操作标志 1：添加； 2：属性
        public string FlowProjectName
        {
            get { return textBox1.Text.Trim(); }
            set { textBox1.Text = value; }
        }

        public AddFlowProject(SqlConnection mainConnection)
        {
            InitializeComponent();
            sqlConnection = mainConnection;
        }

        private void AddFlowProject_Load(object sender, EventArgs e)
        {
            technologyLib = new TechnologyLib(sqlConnection);
            dataSet = new DataSet();

            technologyLib.GetFlowProjectData(dataSet);
            m_FlowProjectID = technologyLib.GetFlowProjectID(FlowProjectName);

            switch (m_OperateFlag)
            {
                case 1:
                    break;
                case 2:
                    for (int i = 0; i < dataSet.Tables["FlowProject"].Rows.Count; i++)
                    {
                        if (m_FlowProjectID == Convert.ToInt16(dataSet.Tables["FlowProject"].Rows[i]["FlowProjectID"]))
                        {
                            textBox1.Text = dataSet.Tables["FlowProject"].Rows[i]["FlowProjectName"].ToString();
                            richTextBox1.Text = dataSet.Tables["FlowProject"].Rows[i]["Description"].ToString();
                            break;
                        }
                    }
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string flowPorjectName;
            string description;
            flowPorjectName = textBox1.Text.Trim();
            description = richTextBox1.Text.Trim();
            if (flowPorjectName == string.Empty)
            {
                MessageBox.Show("工序方案名称不能为空，请输入！", "警告");
            }

            switch (m_OperateFlag)
            {
                case 1:
                    if (technologyLib.IsSameFlowProject(flowPorjectName))
                    {
                        MessageBox.Show("该工序方案名称已存在，请重新输入工序方案名称!");
                        this.DialogResult = DialogResult.None;
                        return;
                    }
                    break;
                case 2:
                    if (technologyLib.IsSameFlowProject(flowPorjectName, m_FlowProjectID))
                    {
                        MessageBox.Show("该工序方案名称已存在，请重新输入工序方案名称!");
                        this.DialogResult = DialogResult.None;
                        return;
                    }
                    break;
            }

            //else if (technologyLib.isExit(string.Format("SELECT FlowProjectID FROM FlowProject WHERE FlowProjectName = '{0}'", flowPorjectName)))
            //{
            //    MessageBox.Show("工序方案名称已存在！", "警告");
            //    textBox1.Clear();
            //    richTextBox1.Clear();
            //}
            //else
            //{
            //    string strSql = string.Format("INSERT INTO FlowProject (FlowProjectID, FlowProjectName, Description) VALUES ({0}, '{1}', '{2}')", technologyLib.GetMaxFlowProjectID()+1, flowPorjectName, desctripion);
            //    SqlCommand cmd = new SqlCommand(strSql,sqlConnection);
            //    SqlCommandBuilder builder = new SqlCommandBuilder();

            //    try
            //    {
            //        cmd.ExecuteNonQuery();
            //        MessageBox.Show("保存成功！","提示");
            //        this.Close();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message.ToString());
            //    } 
            //}

            string mySelectQuery = "SELECT * FROM FlowProject";
            string myTableName = "FlowProject";
            CreateCmdsAndUpdate(mySelectQuery, myTableName, flowPorjectName, description);
        }

        //新建一行数据
        public DataSet CreateCmdsAndUpdate(string mySelectQuery, string myTableName, string pFlowProjectName, string pDescription)
        {
            SqlDataAdapter myDataAdapter = new SqlDataAdapter();
            myDataAdapter.SelectCommand = new SqlCommand(mySelectQuery, sqlConnection);
            SqlCommandBuilder custCB = new SqlCommandBuilder(myDataAdapter);

            DataSet custDS = new DataSet();
            myDataAdapter.Fill(custDS);
            try
            {
                //code to modify data in dataset here
                switch (m_OperateFlag)
                {
                    case 1:	//添加
                        DataRow myDataRow;
                        myDataRow = custDS.Tables[0].NewRow();
                        myDataRow["FlowProjectID"] = technologyLib.GetMaxFlowProjectID() + 1;
                        myDataRow["FlowProjectName"] = pFlowProjectName;
                        myDataRow["Description"] = pDescription;
                        custDS.Tables[0].Rows.Add(myDataRow);
                        break;
                    case 2:	//属性
                        for (int i = 0; i < custDS.Tables[0].Rows.Count; i++)
                        {
                            if (m_FlowProjectID == Convert.ToInt16(custDS.Tables[0].Rows[i]["FlowProjectID"]))
                            {
                                custDS.Tables[0].Rows[i]["FlowProjectName"] = pFlowProjectName;
                                custDS.Tables[0].Rows[i]["Description"] = pDescription;
                            }
                        }
                        break;
                }
                //Without the OleDbCommandBuilder this line would fail
                myDataAdapter.Update(custDS);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
            }

            return custDS;
        }
    }
}
