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
    public partial class AddCmdItemForm : Form
    {
        private SqlConnection sqlConnection;
        private DataSet dataSet;
        private TechnologyLib technologyLib;

        public AddCmdItemForm(SqlConnection mainConnection)
        {
            InitializeComponent();
            sqlConnection = mainConnection;
        }

        private void AddCmdItemForm_Load(object sender, EventArgs e)
        {
            technologyLib = new TechnologyLib(sqlConnection);
            dataSet = new DataSet();

            technologyLib.GetCmdItemDetailsViewData(dataSet);

            technologyLib.GetProtocalData(dataSet);
            for (int i = 0; i < dataSet.Tables["Protocal"].Rows.Count; i++)
            {
                comboBox1.Items.Add(dataSet.Tables["Protocal"].Rows[i][1].ToString());
            }

            technologyLib.GetCommPortData(dataSet);
            for (int i = 0; i < dataSet.Tables["CommPort"].Rows.Count; i++)
            {
                comboBox2.Items.Add(dataSet.Tables["CommPort"].Rows[i][1].ToString());
            }
        }
    }
}
