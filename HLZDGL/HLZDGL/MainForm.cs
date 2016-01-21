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
    public partial class MainForm : Form
    {
        private SqlConnection mainConnection;
        private DataSet dataSet;
        private Global global;

        private Form chForm;

        /// <summary>
        /// 登陆人员信息
        /// </summary>
        public struct LoginUserInfo
        {
            public int userID;
            public int roleID;
            public string userBarCode;
        }

        private LoginUserInfo loginUserInfo = new LoginUserInfo();

        private CurFlowConfig curFlowConfig = new CurFlowConfig();

        public MainForm()
        {
            InitializeComponent();
        }

        
        private void MainForm_Load(object sender, EventArgs e)
        {
            LoginOut();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void GetIniConfig()
        {
            dataSet = new DataSet();
            dataSet.ReadXml("Config.xml");
        }

        /// <summary>
        /// 获取当前流程
        /// </summary>
        /// <param name="ds"></param>
        public void InitialStatusBar(DataSet ds)
        {
            toolStripStatusLabel8.Text = ds.Tables["FlowConfig"].Rows[0]["CurrentFlow"].ToString();
        }

        public void LoginOut()
        {
            GetIniConfig();
            InitialStatusBar(dataSet);
            InitailSqlConnection(dataSet);
            global = new Global(mainConnection);
            curFlowConfig = global.m_flowConfig;

            LoginForm loginForm = new LoginForm(mainConnection);            
            if (loginForm.ShowDialog(this) == DialogResult.OK)
            {
                loginUserInfo.userBarCode = loginForm.strUserName;
                loginUserInfo.userID = global.GetUserIDInUserInfo(loginUserInfo.userBarCode);
                loginUserInfo.roleID = global.GetRoleID(loginUserInfo.userBarCode);

                if (global.IsFlowUser(loginUserInfo.userBarCode, curFlowConfig.flowName))
                {
                    global.InsertUserOperateRecord(loginUserInfo.userID, "登录", DateTime.Now.ToString(), toolStripStatusLabel8.Text, "成功");
                    toolStripStatusLabel4.Text = loginUserInfo.userBarCode;
                    toolStripStatusLabel6.Text = DateTime.Now.ToString();
                }
                else
                {
                    global.InsertUserOperateRecord(loginUserInfo.userID, "登录", DateTime.Now.ToString(), curFlowConfig.flowName, "失败");
                }
                loginForm.Dispose();
            }
            else
            {
                global = new Global(mainConnection);
                LogoutForm logoutForm = new LogoutForm(); 
                if (loginForm.ShowDialog(this) ==DialogResult.OK)
                {
                    global.InsertUserOperateRecord(loginUserInfo.userID, "注销", DateTime.Now.ToString(), curFlowConfig.flowName, "成功");
                    CloseMdiForm();
                    mainConnection.Close();
                }
                loginForm.Dispose();
            }
        }

        public void InitailSqlConnection(DataSet ds)
        {
            string strSqlConnection = System.Configuration.ConfigurationManager.AppSettings["SqlODBC"];
            mainConnection = new SqlConnection(strSqlConnection);
            try
            { 
                mainConnection.StateChange += new StateChangeEventHandler(mainSqlConnection_StateChange);
                mainConnection.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                mainConnection.Close();
            }
        }

        #region 操作按钮管理

        //权限管理
        private void AuthorityToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //人员管理
        private void PersonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string formName = "人员管理";
            OpenFlowForm(formName);
        }

        //备份恢复
        private void BackupToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //工艺流程
        private void ProcessFlowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string formName = "工艺流程管理";
            OpenFlowForm(formName);
        }

        //工艺方案
        private void ProcessSchemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string formName = "工艺方案管理";
            OpenFlowForm(formName);
        }

        //调试方案
        private void DebugSchemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string formName = "调试方案管理";
            OpenFlowForm(formName);
        }

        //调试项目
        private void DebugProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string formName = "调试项目管理";
            OpenFlowForm(formName);
        }

        //调试参数
        private void DebugParameterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string formName = "调试参数管理";
            OpenFlowForm(formName);
        }

        //调试命令
        private void DebugCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string formName = "调试命令管理";
            OpenFlowForm(formName);
        }

        //通讯参数
        private void CommunicationParameterToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ProjectManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string formName = "方案管理";
            OpenFlowForm(formName);
        }

        #endregion

        public void OpenFlowForm(string flowName)
        {
            //调用对应的工序模块
            //功能：
            //1、创建一个新的MDI子窗体实例
            //2、控制最多只能创建一个
            try
            {
                global = new Global(mainConnection);
                if (!global.IsDBConnected(mainConnection))
                {
                    return;
                }

                string curFlowName = this.curFlowConfig.flowName;
                string curFlowProject_P = this.curFlowConfig.flowProject_P;
                string curFlowProject_L = this.curFlowConfig.flowProject_L;
                string curFlowProject_C = this.curFlowConfig.flowProject_C;
                string curFlowProject_G = this.curFlowConfig.flowProject_G;
                string curFlowProject_T = this.curFlowConfig.flowProject_T;
                int userID = this.loginUserInfo.userID;

                switch (flowName)
                {
                    case "人员管理":
                        chForm = new UserInfo(mainConnection);
                        break;
                    case "工艺流程管理":
                        chForm = new FlowInfo(mainConnection);
                        break;
                    case "工艺方案管理":
                        chForm = new FlowProjectInfo(mainConnection);
                        break;
                    case "调试方案管理":
                        chForm = new DebugSchemeInfo(mainConnection);
                        break;
                    case "调试项目管理":
                        chForm = new DebugItemInfo(mainConnection);
                        break;
                    case "调试参数管理":
                        //chForm = new DebugParaConfigForm(mainConnection);
                        break;
                    case "调试命令管理":
                        chForm = new CmdItemInfo(mainConnection);
                        break;
                    case "方案管理":
                        chForm = new ProjectManager(mainConnection);
                        break;
                    case "生产订单关联":
                        chForm = new ProductOrderAssociation(mainConnection);
                        break;
                    default :
                        chForm = new Form();
                        break;
                }

                //获取当前实例的运行时类型
                string flowType = chForm.GetType().ToString();
                int index = flowType.LastIndexOf(".") + 1;
                int subLength = flowType.Length - index;
                flowType = flowType.Substring(index,subLength);

                Form[] chFormArry = this.MdiChildren;

                bool isLive = false;
                foreach(Form myForm in chFormArry)
                {
                    if(myForm.Name == flowType)
                    {
                        myForm.WindowState = FormWindowState.Maximized;
                        isLive = true;
                    }
                }
                if (!isLive)
                {
                    chForm.MdiParent = this;
                    chForm.WindowState = FormWindowState.Maximized;
                    chForm.Show();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("打开错误" + ex.Message, flowName,MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
        }

        /// <summary>
        /// 关闭打开的MDI窗体
        /// </summary>
        public void CloseMdiForm()
        {
            Global.bClosing = true;
            Form[] charr = this.MdiChildren;
            foreach (Form chform in charr)
            {
                chform.Close();
            }
            Global.bClosing = false;

        }

        public void mainSqlConnection_StateChange(object sender, StateChangeEventArgs e)
        {
            string strCurrentState;
            if (e.CurrentState.ToString().Equals("Open"))
                strCurrentState = "已连接";
            else if (e.CurrentState.ToString().Equals("Closed"))
                strCurrentState = "关闭";
            else if (e.CurrentState.ToString().Equals("Connecting"))
                strCurrentState = "正在连接";
            else if (e.CurrentState.ToString().Equals("Executing"))
                strCurrentState = "正在执行命令";
            else if (e.CurrentState.ToString().Equals("Closed"))
                strCurrentState = "关闭";
            else if (e.CurrentState.ToString().Equals("Fetching"))
                strCurrentState = "正在检索数据";
            else
                strCurrentState = "未知";

            toolStripStatusLabel2.Text = strCurrentState;
        }


        //客户订单导入
        private void ProductImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductImport productImport = new ProductImport();
            productImport.Show();
        }

        //客户订单查询
        private void CustomerProductQueryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerProductQuery customerProductQuery = new CustomerProductQuery(mainConnection);
            customerProductQuery.Show();
        }

        //生产订单查询
        private void 生产订单查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductionOrder productionOrder = new ProductionOrder(mainConnection);
            productionOrder.Show();
        }

        private void MachineAssembly1ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ProductOrderAssociationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string formName = "生产订单关联";
            OpenFlowForm(formName);
        }

    }
}
