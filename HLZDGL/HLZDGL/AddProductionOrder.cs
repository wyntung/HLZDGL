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
    public partial class AddProductionOrder : Form
    {
        private SqlConnection sqlConnection;
        private SqlDataAdapter sqlDataAdapter;
        
        public AddProductionOrder(SqlConnection mainConnection)
        {
            sqlConnection = mainConnection;
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string orderCode = textBox1.Text.Trim();
            string type = textBox2.Text;
            string specification = textBox3.Text;
            string barCode = textBox4.Text;
            string number = textBox5.Text;
            string customer = textBox6.Text;
            string branch = textBox7.Text;
            string productName = textBox8.Text;
            string workshop = textBox9.Text;            
            string packingNumber = textBox10.Text;
            string voltage = textBox11.Text;
            string jingdu = textBox12.Text;

            if (SelectExistRow())
            {
                MessageBox.Show("订单号已存在，请重输");
                return;
            }
            else
            {
                if (!string.IsNullOrEmpty(orderCode))
                {
                    try
                    {
                        string sqlStr = string.Format("INSERT INTO ProductionOrder (OrderCode, Type, Specification, BarCode, Customer, Branch, Number, Workshop, ProductName, PackingNumber, Voltage, jingdu) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', {6}, '{7}', '{8}', {9}, '{10}', '{11}')", orderCode, type, specification, barCode, customer, branch, number, workshop, productName, packingNumber, voltage, jingdu);
                        SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
                        sqlCommand.ExecuteNonQuery();
                        MessageBox.Show("保存成功");
                    }
                    catch(Exception)
                    {
                        
                    }
                }
                else
                {
                    this.textBox1.Clear();
                    this.textBox2.Clear();
                    this.textBox3.Clear();
                    this.textBox4.Clear();
                    this.textBox5.Clear();
                    this.textBox6.Clear();
                    this.textBox7.Clear();
                    this.textBox8.Clear();
                    this.textBox9.Clear();
                    this.textBox10.Clear();
                    this.textBox11.Clear();
                    this.textBox12.Clear();
                    this.textBox1.Focus();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool SelectExistRow()
        {
            bool rc = false;
            try
            {
                string sqlStr = string.Format("SELECT COUNT(*) FROM ProductionOrder WHERE OrderCode = '{0}'", textBox1.Text.ToString());
                SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
                sqlCommand.ExecuteScalar();

                if (sqlCommand.ExecuteScalar().Equals(0))
                {
                    rc = false;
                }
                else
                {
                    rc = true;
                }
            }
            catch (Exception)
            {
                rc = false;
            }
            return rc;
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int kc = (int)e.KeyChar;
                if((kc < 48 ||kc > 57)&& kc !=8)
                {
                    e.Handled = true; 
                }
            }
            catch(Exception)
            {
                return;
            }
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int kc = (int)e.KeyChar;
                if ((kc < 48 || kc > 57) && kc != 8)
                {
                    e.Handled = true;
                }
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
