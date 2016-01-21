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
    public partial class AddDebugItemProject : Form
    {
        private SqlConnection sqlConnection;
        private DataSet dataSet;
        private TechnologyLib technologyLib;

        private int m_ProjectID;
        public int m_OperateFlag = 1;   //操作标志 1：添加 2：修改

        public string ProjectName
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

        public AddDebugItemProject(SqlConnection mainConnection)
        {
            InitializeComponent();
            sqlConnection = mainConnection;
        }

        private void AddDebugItemProject_Load(object sender, EventArgs e)
        {
            technologyLib = new TechnologyLib(sqlConnection);
            dataSet = new DataSet();

            technologyLib.GetDebugItemProjectData(dataSet);
            m_ProjectID = technologyLib.GetDebugItemProjectID(ProjectName);

            switch (m_OperateFlag)
            {
                case 1:
                    break;
                case 2:
                    for (int i = 0; i < dataSet.Tables["DebugItemProject"].Rows.Count;i++ )
                    {
                        if (m_ProjectID == Convert.ToInt32(dataSet.Tables["DebugItemProject"].Rows[i]["ProjectID"]))
                        {
                            textBox1.Text = dataSet.Tables["DebugItemProject"].Rows[i]["ProjectID"].ToString();
                            richTextBox1.Text = dataSet.Tables["DebugItemProject"].Rows[i]["Description"].ToString();
                        }
                        else
                        {
                            return;
                        }
                    }
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string projectName = textBox1.Text;
            string description = richTextBox1.Text;

            if (projectName == "")
            {
                MessageBox.Show("调试方案名称不能为空，请重输！");
                this.DialogResult = DialogResult.None;
                return;
            }

            switch(m_OperateFlag)
            {
                case 1:
                    if (technologyLib.IsSameDebugItemProject(projectName))
                    {
                        MessageBox.Show("调试方案名称已存在，请重输！");
                        this.DialogResult = DialogResult.None;
                        return;
                    }
                    break;
                case 2:
                    if (technologyLib.IsSameDebugItemProject(projectName,m_ProjectID))
                    {
                        MessageBox.Show("调试方案名称已存在，请重输！");
                        this.DialogResult = DialogResult.None;
                        return;
                    }
                    break;
            }
                string sqlStr = string.Format("SELECT * FROM DebugItemProject");
                string tableName = "DebugItemProject";
                CreateCmdsAndUpdate(sqlStr, tableName, projectName, description);
        }

        //新增一条记录
        public DataSet CreateCmdsAndUpdate(string pSelectQuery, string pTableName, string pProjectName, string pDescription)
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = new SqlCommand(pSelectQuery,sqlConnection);
            SqlCommandBuilder builder = new SqlCommandBuilder(sqlDataAdapter);

            DataSet ds = new DataSet();
            sqlDataAdapter.Fill(ds);
            try
            {
                switch(m_OperateFlag)
                {
                    //添加
                    case 1:
                        DataRow dataRow = ds.Tables[0].NewRow();
                        dataRow["ProjectName"] = pProjectName;
                        dataRow["Description"] = pDescription;
                        dataRow["Status"] = 1;
                        ds.Tables[0].Rows.Add(dataRow);
                        break;
                    //修改
                    case 2:
                        for(int i=0;i<ds.Tables[0].Rows.Count;i++)
                        {
                            if (m_ProjectID == Convert.ToInt32(ds.Tables[0].Rows[i]["ProjectID"]))
                            {
                                ds.Tables[0].Rows[i]["ProjectName"] = pProjectName;
                                ds.Tables[0].Rows[i]["Description"] = pDescription;
                            }
                        }
                        break;
                }
                sqlDataAdapter.Update(ds);
                this.Close();
                MessageBox.Show("成功！");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return ds;
        }
    }
}
