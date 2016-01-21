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
    public partial class AddFlow : Form
    {
        private SqlConnection sqlConnection;
        private DataSet dataSet;
        private TechnologyLib technologyLib;
        private int m_FlowID;
        public int m_OperateFlag = 1;

        public string FlowName
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
            }
        }

        public AddFlow(SqlConnection mainConnection)
        {
            sqlConnection = mainConnection;
            InitializeComponent();
        }

        private void AddFlow_Load(object sender, EventArgs e)
        {
            technologyLib = new TechnologyLib(sqlConnection);
            dataSet = new DataSet();

            technologyLib.GetFlowDetailsData(dataSet);
            m_FlowID = technologyLib.GetFlowID(FlowName);

            //获取子工序名称
            technologyLib.GetFlowSubData(dataSet);
            for (int i = 0;i < dataSet.Tables["FlowSub"].Rows.Count;i++ )
            {
                this.comboBox1.Items.Add(dataSet.Tables["FlowSub"].Rows[i][1]);
            }

            switch (m_OperateFlag)
            {
                case 1:	//添加
                    break;
                case 2:	//属性
                    for (int i = 0; i < dataSet.Tables["FlowDetails"].Rows.Count; i++)
                    {
                        if (m_FlowID == Convert.ToInt16(dataSet.Tables["FlowDetails"].Rows[i]["FlowID"]))
                        {
                            textBox1.Text = dataSet.Tables["FlowDetails"].Rows[i]["FlowName"].ToString();
                            richTextBox1.Text = dataSet.Tables["FlowDetails"].Rows[i]["Description"].ToString();
                            break;
                        }
                    }
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string flowName;
            string description;
            flowName = textBox1.Text;
            description = richTextBox1.Text;

            if (flowName == "")
            {
                MessageBox.Show("请输入工序名称，工序名称不能为空!");
                this.DialogResult = DialogResult.None;
                return;
            }

            switch (m_OperateFlag)
            {
                case 1:
                    if (technologyLib.IsSameFlow(flowName))
                    {
                        MessageBox.Show("该工序名称已存在，请重新输入工序名称!");
                        this.DialogResult = DialogResult.None;
                        return;
                    }
                    break;
                case 2:
                    if (technologyLib.IsSameFlow(flowName, m_FlowID))
                    {
                        MessageBox.Show("该工序名称已存在，请重新输入!");
                        this.DialogResult = DialogResult.None;
                        return;
                    }
                    break;
            }

            string mySelectQuery = "SELECT * FROM Flow";
            string myTableName = "Flow";
            CreateCmdsAndUpdate(mySelectQuery, myTableName, flowName, description);
        }

        //新建一行数据
        public DataSet CreateCmdsAndUpdate(string mySelectQuery, string myTableName, string pFlowName, string pDescription)
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
                        myDataRow["FlowID"] = Convert.ToInt32(technologyLib.GetMaxFlowID()+1);
                        myDataRow["FlowName"] = pFlowName;
                        myDataRow["Description"] = pDescription;
                        custDS.Tables[0].Rows.Add(myDataRow);
                        break;
                    case 2:	//属性
                        for (int i = 0; i < custDS.Tables[0].Rows.Count; i++)
                        {
                            if (m_FlowID == Convert.ToInt16(custDS.Tables[0].Rows[i]["FlowID"]))
                            {
                                custDS.Tables[0].Rows[i]["FlowName"] = pFlowName;
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
