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
    public partial class AddUserGroup : Form
    {
        private SqlConnection sqlConnection;
        private DataSet dataSet;
        private TechnologyLib technologyLib;
        private int m_UserGrpID;

        public int m_OperateFlag = 1;	//操作标志 1：添加； 2：属性

        public string UserGrpName   // 用户组名称属性
        {
            set
            {
                textBox1.Text = value;
            }
            get
            {
                return textBox1.Text;
            }
        }

        public AddUserGroup(SqlConnection mainConnection)
        {
            InitializeComponent();
            sqlConnection = mainConnection;
        }

        private void AddUserGroup_Load(object sender, EventArgs e)
        {
            technologyLib = new TechnologyLib(sqlConnection);
            dataSet = new DataSet();
            technologyLib.GetRoleData(dataSet);
            technologyLib.GetUserPopedomData(dataSet);
            technologyLib.GetUserGrpDetailsViewData(dataSet);
            technologyLib.GetUserGrpData(dataSet, UserGrpName);
            m_UserGrpID = technologyLib.GetUserGrpID(UserGrpName);

            switch (m_OperateFlag)
            {
                case 1:	//添加
                    break;
                case 2:	//属性
                    for (int i = 0; i < dataSet.Tables["UserGrpDetailsView"].Rows.Count; i++)
                    {
                        if (m_UserGrpID == Convert.ToInt16(dataSet.Tables["UserGrpDetailsView"].Rows[i]["UserGrpID"]))
                        {
                            richTextBox1.Text = dataSet.Tables["UserGrpDetailsView"].Rows[i]["Description"].ToString();
                            break;
                        }
                    }
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //角色ID
            int roleID = 0;
            for (int i = 0; i < dataSet.Tables["Role"].Rows.Count; i++)
            {
                if (comboBox1.Text == dataSet.Tables["Role"].Rows[i][1].ToString())
                {
                    roleID = Convert.ToUInt16(dataSet.Tables["Role"].Rows[i][0]);
                    break;
                }
            }
            
            //用户组名称
            string userGrpName;
            userGrpName = textBox1.Text;
            //描述
            string description;
            description = richTextBox1.Text;

            if (userGrpName == "")
            {
                MessageBox.Show("请输入用户组名称!");
                this.DialogResult = DialogResult.None;
                return;
            }

            switch (m_OperateFlag)
            {
                case 1:
                    if (technologyLib.IsSameUserGrpName(userGrpName))
                    {
                        MessageBox.Show("用户组名称已经存在，请重新输入!");
                        this.DialogResult = DialogResult.None;
                        return;
                    }
                    break;
                case 2:
                    if (technologyLib.IsSameUserGrpName(userGrpName, m_UserGrpID))
                    {
                        MessageBox.Show("用户组名称已经存在，请重新输入!");
                        this.DialogResult = DialogResult.None;
                        return;
                    }
                    break;
            }

            if (roleID == 0)
            {
                MessageBox.Show("请选择用户组角色!");
                this.DialogResult = DialogResult.None;
                return;
            }

            string mySelectQuery = "SELECT * FROM UserGrp";
            string myTableName = "UserGrp";
            CreateCmdsAndUpdate(mySelectQuery, myTableName, roleID, userGrpName, description);
        }

        public DataSet CreateCmdsAndUpdate(string mySelectQuery, string myTableName, int roleID, string userGrpName, string description)
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
                        myDataRow["RoleID"] = roleID;
                        myDataRow["UserGrpName"] = userGrpName;
                        myDataRow["Description"] = description;
                        custDS.Tables[0].Rows.Add(myDataRow);
                        break;
                    case 2:	//属性
                        for (int i = 0; i < custDS.Tables[0].Rows.Count; i++)
                        {
                            if (m_UserGrpID == Convert.ToInt16(custDS.Tables[0].Rows[i]["UserGrpID"]))
                            {
                                custDS.Tables[0].Rows[i]["RoleID"] = roleID;
                                custDS.Tables[0].Rows[i]["UserGrpName"] = userGrpName;
                                custDS.Tables[0].Rows[i]["Description"] = description;
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

        //用户组角色选择
        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();

            dataSet.Tables["Role"].Clear();
            technologyLib.GetRoleData(dataSet);

            for (int i = 0; i < dataSet.Tables["Role"].Rows.Count; i++)
            {
                comboBox1.Items.Add(dataSet.Tables["Role"].Rows[i][1]);
            }
        }
    }
}
