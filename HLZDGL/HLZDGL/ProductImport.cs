using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataAccessClassLibrary;
using System.Data.SqlClient;

namespace HLZDGL
{
    public partial class ProductImport : Form
    {
        private DataAccessClassLibrary.DataAccessClass oraOLEDB;
        private DataAccessClassLibrary.DataAccessClass sqlODBC;
        private int count;
        
        public ProductImport()
        {
            InitializeComponent();
            oraOLEDB = new DataAccessClassLibrary.DataAccessClass(4, System.Configuration.ConfigurationManager.AppSettings["OraOLEDB"]);
            sqlODBC = new DataAccessClassLibrary.DataAccessClass(1, System.Configuration.ConfigurationManager.AppSettings["SqlODBC"]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetProductPlan(DateTime.Parse(this.dateTimePicker1.Value.ToShortDateString()), DateTime.Parse(this.dateTimePicker2.Value.ToShortDateString()));
            this.richTextBox1.Text = "共导入"+ count +"条记录";
        }

        public void GetProductPlan(DateTime startTime,DateTime endTime)
        {
            string sqlStr = string.Empty;
            DataTable dt = null;
            sqlStr = string.Format("select ordercode,productcode,type,spec,unit,num,techrequire,customername,salebranch,remark,oranizer,organizertime,reciever,recievetime,lastupdatetime,attribute4,generalratio,schedule_makeflag,lastupdateperson,affirm_flag,routing_makeflag,cast_complete_flag,character,addup_complete_num,actual_cast_num,product_line,order_type,voltage,customer_require,shop_code,shop_name,xx,frequency,dltype,erp_ordercode,vendor_id,branch_id,product_id,product_num_per_case,serial_no_start,serial_no_end,created_person_code,created_person_name,customer_mark,product_start_bit,product_end_bit,is_valid,type_standard_name,is_export,jingdu,bomcode from info.productplan where ordercode not like 'D%' and recievetime >='{0}' and recievetime <'{1}'", startTime, endTime);
            dt = oraOLEDB.GetTable(sqlStr);
            count = dt.Rows.Count;
            if (dt==null ||dt.Rows.Count <= 0)
            {
                return;
            }
            for (int i = 0; i < dt.Rows.Count;i++ )
            {
                try
                {
                    sqlStr = string.Format(@"insert into PRODUCTPLAN (ordercode,productcode,type,spec,unit,num,techrequire,customername,salebranch,remark,oranizer,organizertime,reciever,recievetime,lastupdatetime,attribute4,generalratio,schedule_makeflag,lastupdateperson,affirm_flag,routing_makeflag,cast_complete_flag,character,addup_complete_num,actual_cast_num,product_line,order_type,voltage,customer_require,shop_code,shop_name,xx,frequency,dltype,erp_ordercode,vendor_id,branch_id,product_id,product_num_per_case,serial_no_start,serial_no_end,created_person_code,created_person_name,customer_mark,product_start_bit,product_end_bit,is_valid,type_standard_name,is_export,jingdu,bomcode) values ('{0}','{1}','{2}','{3}','{4}',{5},'{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}',{16},'{17}','{18}','{19}','{20}','{21}','{22}',{23},{24},'{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}',{35},{36},{37},{38},'{39}','{40}','{41}','{42}','{43}',{44},{45},'{46}','{47}','{48}','{49}','{50}');", dt.Rows[i][0], dt.Rows[i][1], dt.Rows[i][2], dt.Rows[i][3], dt.Rows[i][4], dt.Rows[i][5], dt.Rows[i][6], dt.Rows[i][7], dt.Rows[i][8], dt.Rows[i][9], dt.Rows[i][10], dt.Rows[i][11], dt.Rows[i][12], dt.Rows[i][13], dt.Rows[i][14], dt.Rows[i][15], dt.Rows[i][16], dt.Rows[i][17], dt.Rows[i][18], dt.Rows[i][19], dt.Rows[i][20], dt.Rows[i][21], dt.Rows[i][22], dt.Rows[i][23], dt.Rows[i][24], dt.Rows[i][25], dt.Rows[i][26], dt.Rows[i][27], dt.Rows[i][28], dt.Rows[i][29], dt.Rows[i][30], dt.Rows[i][31], dt.Rows[i][32], dt.Rows[i][33], dt.Rows[i][34], dt.Rows[i][35], dt.Rows[i][36], dt.Rows[i][37], dt.Rows[i][38], dt.Rows[i][39], dt.Rows[i][40], dt.Rows[i][41], dt.Rows[i][42], dt.Rows[i][43], dt.Rows[i][44], dt.Rows[i][45], dt.Rows[i][46], dt.Rows[i][47], dt.Rows[i][48], dt.Rows[i][49], dt.Rows[i][50]);
                    sqlODBC.ExeSQL(sqlStr);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
