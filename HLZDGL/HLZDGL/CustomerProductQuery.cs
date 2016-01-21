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
    public partial class CustomerProductQuery : Form
    {
        private SqlConnection sqlConnection;
        private DataSet dataSet;

        private string orderCode;
        private string productType;
        private string productSpec;
        private string customerName;

        public CustomerProductQuery(SqlConnection mainConnection)
        {
            InitializeComponent();
            sqlConnection = mainConnection;
        }

        private void CustomerProductQuery_Load(object sender, EventArgs e)
        {

        }

        public void QueryData(string orderCode, string productType, string productSpec, string customerName)
        {
            string sqlStr = string.Format("SELECT * FROM ProductPlan WHERE ORDERCODE='{0}' AND TYPE ='{1}' AND SPEC ='{2}' AND CUSTOMERNAME ='{3}'", orderCode, productType, productSpec, customerName);
            SqlCommand sqlCommand = new SqlCommand(sqlStr,sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            
            if (sqlDataReader.HasRows)
            {
                try
                {
                    sqlDataReader.Read();
                    sqlDataReader.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString());
                } 
            }
            
        }

    }
}
