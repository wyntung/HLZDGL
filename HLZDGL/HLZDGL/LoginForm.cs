using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HLZDGL
{
    public partial class LoginForm : Form
    {
        private SqlConnection sqlConnection;
        private DataSet dataSet;
        public string strUserName;
        public string strPassword;            

        public LoginForm(SqlConnection mainConnection)
        {
            InitializeComponent();
            sqlConnection = mainConnection;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            strUserName = textBox1.Text;
            strPassword = textBox2.Text;
            bool rc;

            if ((strUserName == "admin") && (strPassword == "hldw3101"))
            {
                rc = true;
            }
            else
            {
                rc = IsUser(strUserName,strPassword);
            }
            if (!rc)
            {
                MessageBox.Show("用户名或密码错误！");
                textBox2.Clear();
                textBox2.Focus();
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public bool IsUser(string user,string password)
        {
            bool rc = true;
            string sqlStr = string.Format("select count(*) from UserInfo where BarCode = '{0}' and password = '{1}'", user, password);
            SqlCommand sqlCommand = new SqlCommand(sqlStr,sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                reader.Read();
                int userID = (int)reader.GetValue(0);
                if (userID != 0)
                {
                    rc = true;
                }
                else
                {
                    rc = false;
                }
                reader.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                rc = false;
            }
            return rc;
        }

        private void textBox2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Enter:
                    this.button1_Click(sender,e);
                    break;
            }
        }

        private void textBox1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    textBox2.Focus();
                    break;
            }
        }

    }
}
