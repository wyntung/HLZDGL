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
    public partial class AddUser : Form
    {
        private SqlConnection sqlConnection;
        private DataSet dataSet;
        private TechnologyLib technologyLib;
        private int m_UserGrpID;

        public int m_OperateFlag = 1;	//操作标志 1：添加； 2：属性

        public string UserGrpName
        {
            set
            {
                comboBoxUserGrpName.Text = value;
            }
            get
            {
                return comboBoxUserGrpName.Text;
            }
        }

        public string BarCode
        {
            set
            {
                textBoxBarCode.Text = value;
            }
            get
            {
                return textBoxBarCode.Text;
            }
        }
        
        public AddUser(SqlConnection mainConnection)
        {
            InitializeComponent();
            sqlConnection = mainConnection;
        }

        private void AddUser_Load(object sender, EventArgs e)
        {
            technologyLib = new TechnologyLib(sqlConnection);
            dataSet = new DataSet();
            technologyLib.GetUserGrpData(dataSet, UserGrpName);
            technologyLib.GetUserGrpDetailsViewData(dataSet);
            technologyLib.GetUserInfoDetailsViewData(dataSet);
            m_UserGrpID = technologyLib.GetUserGrpID(UserGrpName);

            switch (m_OperateFlag)
            {
                case 1:	//添加
                    textBoxBarCode.Text  = Convert.ToString(technologyLib.GetUserInfoMaxBarCode() + 1);
                    break;
                case 2:	//属性
                    textBoxBarCode.ReadOnly = true;
                    for (int i = 0; i < dataSet.Tables["UserInfoDetailsView"].Rows.Count; i++)
                    {
                        if (BarCode == dataSet.Tables["UserInfoDetailsView"].Rows[i]["BarCode"].ToString())
                        {
                            comboBoxUserGrpName.Text = dataSet.Tables["UserInfoDetailsView"].Rows[i]["UserGrpName"].ToString();
                            textBoxBarCode.Text = dataSet.Tables["UserInfoDetailsView"].Rows[i]["BarCode"].ToString();
                            textBoxUserName.Text = dataSet.Tables["UserInfoDetailsView"].Rows[i]["UserName"].ToString();
                            textBoxUserPsw.Text = dataSet.Tables["UserInfoDetailsView"].Rows[i]["Password"].ToString();
                            textBoxVerifyPsw.Text = dataSet.Tables["UserInfoDetailsView"].Rows[i]["Password"].ToString();
                            comboBoxSex.Text = dataSet.Tables["UserInfoDetailsView"].Rows[i]["Sex"].ToString();
                            textBoxTelephone.Text = dataSet.Tables["UserInfoDetailsView"].Rows[i]["Telephone"].ToString();
                            dateTimePickerBirthday.Text = dataSet.Tables["UserInfoDetailsView"].Rows[i]["Birthday"].ToString();
                            richTextBoxDescription.Text = dataSet.Tables["UserInfoDetailsView"].Rows[i]["Description"].ToString();
                            comboBoxStatus.Text = dataSet.Tables["UserInfoDetailsView"].Rows[i]["Status"].ToString();
                            break;
                        }
                    }
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int groupid;
            string groupname;
            string barcode;
            string username;
            string userpsw;
            string verifypsw;
            string sex;
            string telephone;
            string birthday;
            string description;
            string status;

            groupname = this.comboBoxUserGrpName.Text;
            barcode = textBoxBarCode.Text;
            username = textBoxUserName.Text;
            userpsw = textBoxUserPsw.Text;
            verifypsw = textBoxVerifyPsw.Text;
            sex = comboBoxSex.Text;
            telephone = textBoxTelephone.Text;
            birthday = dateTimePickerBirthday.Text;
            description = richTextBoxDescription.Text;
            status = comboBoxStatus.Text;

            //if (barcode.Length != 7 || barcode.Substring(0, 2) != "96")
            //{
            //    MessageBox.Show("请输入正确人员条码，必须为7位且前2位为96!");
            //    this.DialogResult = DialogResult.None;
            //    return;
            //}

            

            switch (m_OperateFlag)
            {
                case 1:	//添加
                    if (technologyLib.IsSameUserInfoBarCode(barcode))
                    {
                        MessageBox.Show("该条码已存在，请重新输入条码!");
                        this.DialogResult = DialogResult.None;
                        return;
                    }
                    break;
            }
            if (groupname == "")
            {
                MessageBox.Show("请输入用户组名称，用户组名称不能为空!");
                this.DialogResult = DialogResult.None;
                return;
            }

            if (username == "")
            {
                MessageBox.Show("请输入用户姓名，用户姓名不能为空!");
                this.DialogResult = DialogResult.None;
                return;
            }

            if (userpsw != verifypsw)
            {
                MessageBox.Show("两次密码输入不一致，请重新输入！");
                textBoxUserPsw.Clear();
                textBoxVerifyPsw.Clear();
                this.DialogResult = DialogResult.None;
                return;
            }

            string mySelectQuery = "SELECT * FROM UserInfo";
            string myTableName = "UserInfo";
            groupid = technologyLib.GetUserGrpID(groupname);
            CreateCmdsAndUpdate(mySelectQuery, myTableName, groupid, barcode, username, userpsw, sex, birthday, telephone, description, status);
        }

        private void comboBoxUserGrpName_DropDown(object sender, EventArgs e)
        {
            comboBoxUserGrpName.Items.Clear();
            dataSet.Tables["UserGrpDetailsView"].Clear();
            technologyLib.GetUserGrpDetailsViewData(dataSet);

            for (int i = 0; i < dataSet.Tables["UserGrpDetailsView"].Rows.Count; i++)
            {
                comboBoxUserGrpName.Items.Add(dataSet.Tables["UserGrpDetailsView"].Rows[i][1]);
            }
        }

        //新建一行数据
        public DataSet CreateCmdsAndUpdate(string mySelectQuery, string myTableName, int userGrpId,
            string barcode, string userName, string password, string sex, string birthday, string telephone, string description, string status)
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
                        myDataRow["UserGrpID"] = userGrpId;
                        myDataRow["BarCode"] = barcode;
                        myDataRow["UserName"] = userName;
                        myDataRow["Password"] = password;
                        myDataRow["Sex"] = sex;
                        myDataRow["Birthday"] = birthday;
                        myDataRow["Telephone"] = telephone;
                        myDataRow["Description"] = description;
                        myDataRow["Status"] = status;
                        custDS.Tables[0].Rows.Add(myDataRow);
                        break;
                    case 2:	//属性
                        for (int i = 0; i < custDS.Tables[0].Rows.Count; i++)
                        {
                            if (barcode == custDS.Tables[0].Rows[i]["BarCode"].ToString())
                            {
                                custDS.Tables[0].Rows[i]["UserGrpID"] = userGrpId;
                                custDS.Tables[0].Rows[i]["UserName"] = userName;
                                custDS.Tables[0].Rows[i]["Password"] = password;
                                custDS.Tables[0].Rows[i]["Sex"] = sex;
                                custDS.Tables[0].Rows[i]["Birthday"] = birthday;
                                custDS.Tables[0].Rows[i]["Telephone"] = telephone;
                                custDS.Tables[0].Rows[i]["Description"] = description;
                                custDS.Tables[0].Rows[i]["Status"] = status;
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
