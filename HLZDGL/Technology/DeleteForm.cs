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
    public partial class DeleteForm : Form
    {
        private SqlConnection sqlConnection;
        private DataSet dataSet;
        private TechnologyLib technologyLib;

        //删除标志 1：删除权限 2：删除用户组 3：删除用户 
        //4：删除工序 5:删除工序方案 6：删除调试项目 7:	删除调试方案 8:删除调试命令
        //9:删除考机方案
        //10:删除验证模块
        //11:删除验证模块权限
        //12:删除类型名称
        private int m_DeleteFlag;
        public int DeleteFlag
        {
            set
            {
                m_DeleteFlag = value;
            }
            get
            {
                return m_DeleteFlag;
            }
        }

        //权限
        private int m_PopedomID;
        private string m_PopedomName;
        public string PopedomName
        {
            set
            {
                m_PopedomName = value;
            }
            get
            {
                return m_PopedomName;
            }
        }

        //用户组
        private string m_UserGrpName;
        public string UserGrpName
        {
            set
            {
                m_UserGrpName = value;
            }
            get
            {
                return m_UserGrpName;
            }
        }

        //用户条码
        private string m_BarCode;
        public string BarCode
        {
            set
            {
                m_BarCode = value;
            }
            get
            {
                return m_BarCode;
            }
        }

        //工序
        private string m_FlowName;
        public string FlowName
        {
            set
            {
                m_FlowName = value;
            }
            get
            {
                return m_FlowName;
            }
        }

        //工序方案
        private int m_FlowProjectID;
        private string m_FlowProjectName;
        public string FlowProjectName
        {
            set
            {
                m_FlowProjectName = value;
            }
            get
            {
                return m_FlowProjectName;
            }
        }

        //调试项目
        private int m_DebugItemID;
        private string m_DebugItemName;
        public string DebugItemName
        {
            set
            {
                m_DebugItemName = value;
            }
            get
            {
                return m_DebugItemName;
            }
        }

        //调试方案
        private int m_DebugItemProjectID;
        private string m_DebugItemProjectName;
        public string DebugItemProjectName
        {
            set
            {
                m_DebugItemProjectName = value;
            }
            get
            {
                return m_DebugItemProjectName;
            }
        }

        //调试命令
        private int m_CmdItemID;
        private string m_CmdItemName;
        public string CmdItemName
        {
            set
            {
                m_CmdItemName = value;
            }
            get
            {
                return m_CmdItemName;
            }
        }

        //考机方案
        private int m_TestRunProjectID;
        private string m_TestRunProjectName;
        public string TestRunProjectName
        {
            set
            {
                m_TestRunProjectName = value;
            }
            get
            {
                return m_TestRunProjectName;
            }
        }

        //验证模块
        private int m_VerifyModuleID;
        private string m_VerifyModuleName;
        public string VerifyModuleName
        {
            set
            {
                m_VerifyModuleName = value;
            }
            get
            {
                return m_VerifyModuleName;
            }
        }
        //权限
        private int m_VerifyPopedomID;
        private string m_VerifyPopedomName;
        public string VerifyPopedomName
        {
            set
            {
                m_VerifyPopedomName = value;
            }
            get
            {
                return m_VerifyPopedomName;
            }
        }
        //模块类型名称
        private int m_ModuleClassID;
        private string m_ModuleClassName;
        public string ModuleClassName
        {
            set
            {
                m_ModuleClassName = value;
            }
            get
            {
                return m_ModuleClassName;
            }
        }
        //模块条码编码
        private int m_ModuleID;
        private string m_ModuleCode;
        public string ModuleCode
        {
            set
            {
                m_ModuleCode = value;
            }
            get
            {
                return m_ModuleCode;
            }
        }

        //装配项目
        private int m_AssembleItemId;
        private string m_AssembleItemName;
        public string AssembleItemName
        {
            set
            {
                m_AssembleItemName = value;
            }
            get
            {
                return m_AssembleItemName;
            }
        }

        //装配方案
        private int m_AssembleSchId;
        private string m_AssembleSchName;
        public string AssembleSchName
        {
            set
            {
                m_AssembleSchName = value;
            }
            get
            {
                return m_AssembleSchName;
            }
        }

        public DeleteForm(SqlConnection mainConnection)
        {
            InitializeComponent();
            sqlConnection = mainConnection;
        }

        private void DeleteForm_Load(object sender, EventArgs e)
        {
            //technologyLib = new TechnologyLib(sqlConnection);
            //dataSet = new DataSet();
            //technologyLib.GetUserPopedomData(dataSet);

            //string textName = "确定要删除调试方案：\r\n" + m_DebugItemProjectName;
            //m_DebugItemProjectID = technologyLib.GetDebugItemProjectID(m_DebugItemProjectName);
            technologyLib = new TechnologyLib(sqlConnection);
            dataSet = new DataSet();
            technologyLib.GetUserPopedomData(dataSet);

            string textName = "";
            switch (m_DeleteFlag)
            {
                case 1:
                    m_PopedomID = technologyLib.GetUserPopedomID(m_PopedomName);
                    textName = "确定要删除权限方案：\r\n" + m_PopedomName;
                    break;
                case 2:
                    textName = "确定要删除用户组：\r\n" + m_UserGrpName;
                    break;
                case 3:
                    textName = "确定要删除用户：\r\n" + m_BarCode;
                    break;
                case 4:
                    textName = "确定要删除工序：\r\n" + m_FlowName;
                    break;
                case 5:
                    m_FlowProjectID = technologyLib.GetFlowProjectID(m_FlowProjectName);
                    textName = "确定要删除工序方案：\r\n" + m_FlowProjectName;
                    break;
                case 6:
                    m_DebugItemID = technologyLib.GetDebugItemID(m_DebugItemName);
                    textName = "确定要删除调试项目：\r\n" + m_DebugItemName;
                    break;
                case 7:
                    m_DebugItemProjectID = technologyLib.GetDebugItemProjectID(m_DebugItemProjectName);
                    textName = "确定要删除调试方案：\r\n" + m_DebugItemProjectName;
                    break;
                case 8:
                    m_CmdItemID = technologyLib.GetCmdItemID(m_CmdItemName);
                    textName = "确定要删除调试命令：\r\n" + m_CmdItemName;
                    break;
                case 9:
                    m_TestRunProjectID = technologyLib.GetT_TestRunProjectID(m_TestRunProjectName);
                    textName = "确定要删除考机方案：\r\n" + m_TestRunProjectName;
                    break;
                case 10:
                    m_VerifyModuleID = technologyLib.GetVerifyModuleID(m_VerifyModuleName);
                    textName = "确定要删除验证模块：\r\n" + m_VerifyModuleName;
                    break;
                case 11:
                    m_VerifyPopedomID = technologyLib.GetVerifyPopedomID(m_VerifyPopedomName);
                    textName = "确定要删除验证模块权限方案：\r\n" + m_VerifyPopedomName;
                    break;
                case 12:
                    m_ModuleClassID = technologyLib.GetModuleClassID(m_ModuleClassName);
                    textName = "确定要删除模块类型名称：\r\n" + m_ModuleClassName;
                    break;
                case 13:
                    textName = "确定要删除条码编码：\r\n" + m_ModuleCode;
                    break;
                case 14:
                    textName = "确定要删除装配项目：\r\n" + m_AssembleItemName;
                    break;
                case 15:
                    textName = "确定要删除装配方案：\r\n" + m_AssembleItemName;
                    break;
            }
            richTextBox1.Text = textName; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (technologyLib.IsDebugItemProjectIDInT_DebugInfo(m_DebugItemProjectID))
            //{
            //    MessageBox.Show("调试信息中存在该调试方案，不能删除此调试方案");
            //    this.DialogResult = DialogResult.None;
            //    return;
            //}
            //technologyLib.DeleteDebugItemProjectDetailsData(m_DebugItemProjectID);
            //technologyLib.DeleteDebugItemProjectData(m_DebugItemProjectID);

            switch (m_DeleteFlag)
            {
                //case 1:
                //    if (technologyLib.IsPopedomInUserGrp(m_PopedomName))
                //    {
                //        MessageBox.Show("已经存在用户组使用该权限方案，不能删除该权限!");
                //        this.DialogResult = DialogResult.None;
                //        return;
                //    }
                //    technologyLib.DeleteUserPopedomDetailsData(m_PopedomID);
                //    technologyLib.DeleteUserPopedomData(m_PopedomName);
                //    break;
                //case 2:
                //    if (technologyLib.IsUserInUserGrp(m_UserGrpName))
                //    {
                //        MessageBox.Show("该用户组中存在用户，请先删除用户!");
                //        this.DialogResult = DialogResult.None;
                //        return;
                //    }
                //    technologyLib.DeleteUserGrpData(m_UserGrpName);
                //    break;
                case 3:
                    //if (technologyLib.IsUserInUserOperate(m_BarCode))
                    //{
                    //    MessageBox.Show("该用户存在操作记录，不能删除该用户!");
                    //    this.DialogResult = DialogResult.None;
                    //    return;
                    //}
                    technologyLib.DeleteUserInfoData(m_BarCode);
                    break;
                case 4:
                    if (technologyLib.IsFlowInFlowProjectDetails(m_FlowName))
                    {
                        MessageBox.Show("工序方案中存在该工序，不能删除此工序!");
                        this.DialogResult = DialogResult.None;
                        return;
                    }
                    //if (technologyLib.IsFlowInUserPopedomDetailsView(m_FlowName))
                    //{
                    //    MessageBox.Show("用户权限中存在该工序，不能删除此工序!");
                    //    this.DialogResult = DialogResult.None;
                    //    return;
                    //}
                    technologyLib.DeleteFlowData(m_FlowName);
                    break;
                case 5:
                    //if (technologyLib.IsFlowProjectInT_FlowInfo(m_FlowProjectName))
                    //{
                    //    MessageBox.Show("工序信息中存在该工序方案，不能删除此工序方案!");
                    //    this.DialogResult = DialogResult.None;
                    //    return;
                    //}
                    technologyLib.DeleteFlowProjectDetailsData(m_FlowProjectID);
                    technologyLib.DeleteFlowProjectData(m_FlowProjectName);
                    break;
                //case 6:
                //    if (technologyLib.IsDebugItemIDInDebugItemProjectDetails(m_DebugItemID))
                //    {
                //        MessageBox.Show("调试方案中存在该调试项目，不能删除此调试项目!");
                //        this.DialogResult = DialogResult.None;
                //        return;
                //    }
                //    technologyLib.DeleteDebugItemDetailsData(m_DebugItemID);
                //    technologyLib.DeleteDebugItemData(m_DebugItemID);
                //    break;
                case 7:
                    if (technologyLib.IsDebugItemProjectIDInT_DebugInfo(m_DebugItemProjectID))
                    {
                        MessageBox.Show("调试信息中存在该调试方案，不能删除此调试方案!");
                        this.DialogResult = DialogResult.None;
                        return;
                    }
                    technologyLib.DeleteDebugItemProjectDetailsData(m_DebugItemProjectID);
                    technologyLib.DeleteDebugItemProjectData(m_DebugItemProjectID);
                    break;
                //case 8:
                //    if (technologyLib.IsCmdItemIDInDebugItemDetails(m_CmdItemID))
                //    {
                //        MessageBox.Show("调试方案中存在该调试命令，不能删除此调试命令!");
                //        this.DialogResult = DialogResult.None;
                //        return;
                //    }
                //    technologyLib.DeleteCmdItemData(m_CmdItemID);
                //    break;
                //case 9:
                //    if (technologyLib.IsProjectIDInTestRunPointInfo(m_TestRunProjectID))
                //    {
                //        MessageBox.Show("考机数据记录中中存在该方案，不能删除此方案!");
                //        this.DialogResult = DialogResult.None;
                //        return;
                //    }
                //    technologyLib.DeleteTestRunProjectDetailsData(m_TestRunProjectID);
                //    technologyLib.DeleteTestRunProjectData(m_TestRunProjectID);
                //    break;
                //case 10:
                //    if (technologyLib.IsVerifyModuleIDInVerifyPopedomDetails(m_VerifyModuleID))
                //    {
                //        MessageBox.Show("验证权限中存在该验证模块，不能删除此模块!");
                //        this.DialogResult = DialogResult.None;
                //        return;
                //    }
                //    technologyLib.DeleteVerifyModuleData(m_VerifyModuleID);
                //    break;
                //case 11:
                //    if (technologyLib.IsPopedomInModule(m_VerifyPopedomName))
                //    {
                //        MessageBox.Show("已经存在条码模块使用该权限方案，不能删除该权限!");
                //        this.DialogResult = DialogResult.None;
                //        return;
                //    }
                //    technologyLib.DeleteVerifyPopedomDetailsData(m_VerifyPopedomID);
                //    technologyLib.DeleteVerifyPopedomData(m_VerifyPopedomName);
                //    break;
                //case 12:
                //    if (technologyLib.IsModuleInModuleClass(m_ModuleClassName))
                //    {
                //        MessageBox.Show("已经存在条码模块使用该模块类型，不能删除该模块类型!");
                //        this.DialogResult = DialogResult.None;
                //        return;
                //    }
                //    technologyLib.DeleteModuleClassData(m_ModuleClassID);
                //    break;
                //case 13:
                //    if (technologyLib.IsModuleInBarCode(m_ModuleCode))
                //    {
                //        MessageBox.Show("已经存在条码模块使用该模块类型，不能删除该模块类型!");
                //        this.DialogResult = DialogResult.None;
                //        return;
                //    }
                //    technologyLib.DeleteModuleData(m_ModuleCode);
                //    break;
                //case 14:
                //    if (technologyLib.IsAssembleItemInScheme(m_AssembleItemId))
                //    {
                //        MessageBox.Show("已经存在装配方案使用该装配项目，不能删除该装配项目!");
                //        this.DialogResult = DialogResult.None;
                //        return;
                //    }
                //    technologyLib.DeleteAssembleItem(m_AssembleItemId);
                //    break;
                //case 15:
                //    if (technologyLib.IsAssembleSchInUse(m_AssembleSchId))
                //    {
                //        MessageBox.Show("该装配方案被使用，不能删除该装配方案!");
                //        this.DialogResult = DialogResult.None;
                //        return;
                //    }
                //    technologyLib.DeleteAssembleSch(m_AssembleSchId);
                //    break;
            }
        }
    }
}
