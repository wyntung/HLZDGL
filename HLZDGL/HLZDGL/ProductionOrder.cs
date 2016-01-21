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
    public partial class ProductionOrder : Form
    {
        private SqlConnection sqlConnection;
        private DataSet dataSet;

        public ProductionOrder(SqlConnection mainConnection)
        {
            sqlConnection = mainConnection;
            InitializeComponent();
        }

        private void ProductionOrder_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“mESDataSet.ProductionOrder”中。您可以根据需要移动或删除它。
            this.productionOrderTableAdapter.Fill(this.mESDataSet.ProductionOrder);
            GetProductionOrderInfo(dataSet);
            dataGridView1.DataSource = dataSet;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddProductionOrder addProductionOrder = new AddProductionOrder(sqlConnection);
            addProductionOrder.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        public void GetProductionOrderInfo(DataSet ds)
        {
            string sqlStr = string.Format("SELECT OrderCode,Type,Specification,BarCode,Customer,Branch,Number,Workshop,ProductName,PackingNumber,Voltage,jingdu FROM ProductionOrder");
            SqlCommand sqlCommand = new SqlCommand(sqlStr,sqlConnection);
            sqlCommand.CommandTimeout = 30;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;
            sqlDataAdapter.Fill(ds, "ProductionOrder");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
