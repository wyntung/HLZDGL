using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Technology;

namespace HLZDGL
{
    public partial class ProductOrderAssociation : Form
    {
        private SqlConnection sqlConnection;
        private DataSet dataSet;
        private TechnologyLib technologyLib;

        private int OrderID = 0;
        private int FlowProjectID = 0;
        private int DebugProjectID = 0;
        
        public ProductOrderAssociation(SqlConnection mainConnection)
        {
            InitializeComponent();
            sqlConnection = mainConnection;
        }

        private void ProductOrderAssociation_Load(object sender, EventArgs e)
        {
            technologyLib = new TechnologyLib(sqlConnection);
            dataSet = new DataSet();
            button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tempOrderCode = "";
            dataSet = new DataSet();
            dataSet.Clear();
            this.dataGridView1.DataSource = dataSet;
            
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("订单号不能为空，请输入！");
            }
            else if (technologyLib.isExit(string.Format("SELECT * FROM ProductionOrder WHERE OrderCode = '{0}'", textBox1.Text)))
            {
                tempOrderCode = textBox1.Text;
                technologyLib.GetProductionOrderData(dataSet, tempOrderCode);
                dataGridView1.DataSource = dataSet.Tables[0];
                OrderID = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
            }
            else
            {
                MessageBox.Show("订单不存在，请重输！");
            }
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataSet = new DataSet();
            dataSet.Clear();
            this.dataGridView2.DataSource = dataSet;

            //int index = dataGridView1.SelectedRows.Count;
            //if(index > 0)
            //{
                if (!technologyLib.isExit(string.Format("SELECT * FROM OrderSchemeRS WHERE OrderID = {0} ", OrderID)))
                {
                    MessageBox.Show("当前订单缺少对应工序方案，请关联！", "警告");
                    button2.Enabled = true;
                }
                else
                {
                    technologyLib.GetProductOrderFlowDebugProjectData(dataSet, OrderID);
                    FlowProjectID = Convert.ToInt32(dataSet.Tables["ProductOrderFlowDebugProject"].Rows[0][1]);
                    technologyLib.GetFlowProjectDetailsDataWithFlowProjectID(dataSet, FlowProjectID);
                    dataGridView2.DataSource = dataSet.Tables[1];
                }

            //}
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataSet = new DataSet();
            dataSet.Clear();
            this.dataGridView3.DataSource = dataSet;

            if (!technologyLib.isExit(string.Format("SELECT * FROM OrderSchemeRS WHERE DebugProjectID = {0}",DebugProjectID)))
            {
                MessageBox.Show("当前工序无对应方案！", "警告");
            }
            else
            {
                if (dataGridView2.CurrentRow.Selected)
                {
                    DebugProjectID = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value);
                }
                technologyLib.GetDebugItemWithDebugProjectID(dataSet, DebugProjectID);
                dataGridView3.DataSource = dataSet.Tables[0];
                
            }
        }
    }
}
