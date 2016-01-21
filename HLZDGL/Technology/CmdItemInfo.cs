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
    public partial class CmdItemInfo : Form
    {
        private SqlConnection sqlConnection;
        private DataSet dataSet;
        private TechnologyLib technologylib;

        private int[] cmdItemWidths = new int[] { 3, 10, 10, 4, 4, 4, 5, 4, 4, 5, 4, 4, 8, 8, 4 };
        private string[] cmdItem = new string[] { "命令ID","命令名称","描述","CommBof","CommCtrl","CommAddress","CommDataType","CommVSQ","CommCOT","CommIdentify","CommStandby","CommEof","CommProtocalName","CommPortName","通讯延时"};
        private ListViewColumnSorter listViewColumnSorter;

        public CmdItemInfo(SqlConnection mainConnection)
        {
            InitializeComponent();
            sqlConnection = mainConnection;
            listViewColumnSorter = new ListViewColumnSorter();
            this.listView1.ListViewItemSorter = listViewColumnSorter;
        }

        private void CmdItemInfo_Load(object sender, EventArgs e)
        {
            technologylib = new TechnologyLib(sqlConnection);
            dataSet = new DataSet();
            technologylib.GetCmdItemDetailsViewData(dataSet);
            InitialListView(listView1);
        }

        private void InitialListView(ListView listView)
        {
            listView1.Clear();
            InsertAllColumns(0,listView);
            AddItem_CmdItem(listView);
        }

        /// <summary>
        /// 初始化列名
        /// </summary>
        private void InsertAllColumns(int flag,ListView listView)
        {
            int[] widths = new int[20];
            string[] columnHeads = new string[20];
            switch(flag)
            {
                case 0:
                    cmdItemWidths.CopyTo(widths,0);
                    cmdItem.CopyTo(columnHeads,0);
                    break;
                default:
                    break;
            }
            for (int i = 0; i < cmdItemWidths.Length;i++ )
            {
                listView.Columns.Add(new ColumnHeader());
                listView.Columns[i].Text = columnHeads[i];
                listView.Columns[i].Width = widths[i] * 20;
            }
        }

        /// <summary>
        /// 显示CmdItem数据项到listView1中
        /// </summary>
        /// <param name="listView"></param>
        private void AddItem_CmdItem(ListView listView)
        {
            for (int i = 0; i < dataSet.Tables["CmdItemDetailsView"].Rows.Count; i++)
            {
                ListViewItem listViewItem = new ListViewItem(dataSet.Tables["CmdItemDetailsView"].Rows[i][0].ToString());
                for (int j=1;j<cmdItem.Length;j++)
                {
                    listViewItem.SubItems.Add(dataSet.Tables["CmdItemDetailsView"].Rows[i][j].ToString());
                }
                listView.Items.AddRange(new ListViewItem[] { listViewItem });
            }
        }

        /// <summary>
        /// 列排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if(e.Column == listViewColumnSorter.SortColumn)
            {
                if(listViewColumnSorter.Order == System.Windows.Forms.SortOrder.Ascending)
                {
                    listViewColumnSorter.Order = System.Windows.Forms.SortOrder.Descending;
                }
                else
                {
                    listViewColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                }
            }
            else
            {
                listViewColumnSorter.SortColumn = e.Column;
                listViewColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
            }
            this.listView1.Sort();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddCmdItemForm addCmdItem = new AddCmdItemForm(sqlConnection);
            
        }
    }
}
