using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace HLZDGL
{
    public struct CurFlowConfig
    {
        //当前工序配置信息
        public int flowID;
        public int flowProjectID_P;
        public int flowProjectID_L;
        public int flowProjectID_C;
        public int flowProjectID_G;
        public int flowProjectID_T;
        public int flowProjectID_M;
        public int flowProjectID_E;
        public string flowName;			//当前工序名称
        public string flowProject_P;	//电源板当前工序方案名称
        public string flowProject_L;	//液晶当前工序方案名称
        public string flowProject_C;	//CPU当前工序方案名称
        public string flowProject_G;	//GPRS当前工序方案名称
        public string flowProject_T;	//整机当前工序方案名称
        public string flowProject_M;	//主板工序方案名称
        public string flowProject_E;	//扩展板工序方案名称
        public int debugItemID_P;
        public int debugItemID_L;
        public int debugItemID_C;
        public int debugItemID_G;
        public int debugItemID_T;
        public int debugItemID_F;
        public string debugItem_P;	//电源板当前调试项目
        public string debugItem_L;	//液晶当前调试项目
        public string debugItem_C;	//CPU当前调试项目
        public string debugItem_G;	//GPRS当前调试项目
        public string debugItem_T;	//整机当前调试项目
        public string debugItem_F;  //出厂检验方案
    }
    
    class Global
    {
        private SqlConnection sqlConnection;

        public static bool bClosing;

        public CurFlowConfig m_flowConfig;

        public Global(SqlConnection sqlConn)
        {
            sqlConnection = sqlConn;
            InitFlowConfig(ref m_flowConfig);
        }

        /// <summary>
        /// 获取操作员ID
        /// </summary>
        /// <param name="pBarCode"></param>
        /// <returns>userID</returns>
        public int GetUserIDInUserInfo(string pBarCode)
        {
            int userID = 0;
            string sqlStr = "select UserID from UserInfo where BarCode = '" + pBarCode + "'";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                reader.Read();
                userID = Convert.ToInt16(reader.GetValue(0));
                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
            }
            return userID;
        }

        /// <summary>
        /// 获取操作员角色ID
        /// </summary>
        /// <param name="pBarCode"></param>
        /// <returns></returns>
        public int GetRoleID(string pBarCode)
        {
            int roleID = 0;
            string sqlStr = "select RoleID from UserLoginPopedomView where BarCode = '" + pBarCode + "'";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                reader.Read();
                roleID = Convert.ToInt16(reader.GetValue(0));
                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
            }
            return roleID;
        }

        /// <summary>
        /// 判断是否为当前工序的有效用户
        /// </summary>
        /// <param name="pUserBarCode"></param>
        /// <param name="pCurFlowName"></param>
        /// <returns></returns>
        public bool IsFlowUser(string pUserBarCode, string pCurFlowName)
        {
            bool rc = true;
            string status = "";
            string sqlStr = string.Format("select status from UserLoginPopedomView where BarCode = '{0}' and FlowName = '{1}'",pUserBarCode,pCurFlowName);
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                reader.Read();
                if (reader.HasRows)
                {
                    status = reader.GetValue(0).ToString();
                    if (status != "在职")
                    {
                        MessageBox.Show("登录用户不是在职员工，如有疑问请与管理员联系！");
                        rc = false;
                    }
                }
                else
                {
                    MessageBox.Show("登录用户没有当前工序操作权限，请使用具有工序操作权限用户登录！");
                    rc = false;
                }
                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
                rc = false;
            }

            return rc;
        }

        /// <summary>
        /// 获取登陆用户的权限数据
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="pUserBarCode"></param>
        public void GetUserLoginPopedomViewData(DataSet ds, string pUserBarCode)
        {
            string sqlStr = "select RoleID,RoleName,FlowID,FlowName from UserLoginPopedomView"
                + " where BarCode = '" + pUserBarCode + "'";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            sqlCommand.CommandTimeout = 30;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;

            sqlDataAdapter.Fill(ds, "UserLoginPopedom");
        }

        /// <summary>
        /// 用户登陆日志写入到数据库
        /// </summary>
        public bool InsertUserOperateRecord(int pUserID, string pOperateType,string pOperateTime, string pFlowName, string pDescription)
        {
            bool rc = true;
            string sqlStr = string.Format("INSERT INTO UserOperate (UserID,OperateType,OperateTime,FlowName,Description) VALUES ({0},'{1}','{2}','{3}','{4}')", pUserID, pOperateType, pOperateTime, pFlowName, pDescription);
            SqlCommand cmd = new SqlCommand(sqlStr,sqlConnection);
            cmd.CommandTimeout = 30;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                cmd.Dispose();
                rc = false;
            }
            return rc;
        }

        /// <summary>
        /// 根据调试方案名称返回调试方案ID
        /// </summary>
        /// <param name="pDebugItemProjectName"></param>
        /// <returns></returns>
        public int GetDebugItemProjectID(string pDebugItemProjectName)
        {
            int debugItemProjectID = 0;
            string sqlStr = "SELECT ProjectID from DebugItemProject where ProjectName = '" + pDebugItemProjectName + "'";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    debugItemProjectID = Convert.ToInt16(reader["ProjectID"]);
                }
                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
            }

            return debugItemProjectID;
        }

        /// <summary>
        /// 获得工序方案ID
        /// </summary>
        /// <param name="pCurFlowProjectName"></param>
        /// <returns></returns>
        public int GetFlowProjectID(string pCurFlowProjectName)
        {
            int pFlowPrjID = 0;
            string sqlStr = "SELECT FlowProjectID FROM FlowProject WHERE FlowProjectName='" + pCurFlowProjectName + "'";
            SqlCommand comm = new SqlCommand(sqlStr, sqlConnection);
            SqlDataReader reader = null;
            try
            {
                reader = comm.ExecuteReader();
                reader.Read();
                pFlowPrjID = reader.GetInt32(0);
                reader.Close();
            }
            catch
            {
                if (reader != null)
                    reader.Close();
            }
            return pFlowPrjID;
        }


        /// <summary>
        /// 获得工序ID
        /// </summary>
        /// <param name="PFlowName"></param>
        /// <returns></returns>
        public int GetFlowID(string PFlowName)
        {
            int intFlowID = -1;
            string sqlStr = string.Format("select flowid from flow where flowname= '{0}'", PFlowName);
            SqlCommand cmd = new SqlCommand(sqlStr,sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    intFlowID = Convert.ToInt32(reader["flowid"]);
                }
                reader.Close();
            }
            catch
            {
                reader.Close();
            }
            return intFlowID;
        }

        /// <summary>
        /// 初始化当前工序
        /// </summary>
        public void InitFlowConfig(ref CurFlowConfig flowConfig)
        {
            DataSet ds = new DataSet();
            ds.ReadXml("Config.xml");
            flowConfig.flowName = ds.Tables["FlowConfig"].Rows[0]["CurrentFlow"].ToString();
            flowConfig.flowProject_P = ds.Tables["FlowConfig"].Rows[0]["CurrentProject_P"].ToString();
            flowConfig.flowProject_L = ds.Tables["FlowConfig"].Rows[0]["CurrentProject_L"].ToString();
            flowConfig.flowProject_C = ds.Tables["FlowConfig"].Rows[0]["CurrentProject_C"].ToString();
            flowConfig.flowProject_G = ds.Tables["FlowConfig"].Rows[0]["CurrentProject_G"].ToString();
            flowConfig.flowProject_T = ds.Tables["FlowConfig"].Rows[0]["CurrentProject_T"].ToString();
            flowConfig.flowProject_E = ds.Tables["FlowConfig"].Rows[0]["CurrentProject_E"].ToString();
            flowConfig.flowProject_M = ds.Tables["FlowConfig"].Rows[0]["CurrentProject_M"].ToString();

            flowConfig.debugItem_P = ds.Tables["DebugItemConfig"].Rows[0]["P_DebugItemConfig"].ToString();
            flowConfig.debugItem_L = ds.Tables["DebugItemConfig"].Rows[0]["L_DebugItemConfig"].ToString();
            flowConfig.debugItem_C = ds.Tables["DebugItemConfig"].Rows[0]["C_DebugItemConfig"].ToString();
            flowConfig.debugItem_G = ds.Tables["DebugItemConfig"].Rows[0]["G_DebugItemConfig"].ToString();
            flowConfig.debugItem_T = ds.Tables["DebugItemConfig"].Rows[0]["T_DebugItemConfig"].ToString();
            flowConfig.debugItem_F = ds.Tables["DebugItemConfig"].Rows[0]["F_DebugItemConfig"].ToString();

            flowConfig.flowID = GetFlowID(flowConfig.flowName);
            flowConfig.flowProjectID_P = GetFlowProjectID(flowConfig.flowProject_P);
            flowConfig.flowProjectID_L = GetFlowProjectID(flowConfig.flowProject_L);
            flowConfig.flowProjectID_C = GetFlowProjectID(flowConfig.flowProject_C);
            flowConfig.flowProjectID_G = GetFlowProjectID(flowConfig.flowProject_G);
            flowConfig.flowProjectID_T = GetFlowProjectID(flowConfig.flowProject_T);

            flowConfig.debugItemID_P = GetDebugItemProjectID(flowConfig.debugItem_P);
            flowConfig.debugItemID_L = GetDebugItemProjectID(flowConfig.debugItem_L);
            flowConfig.debugItemID_C = GetDebugItemProjectID(flowConfig.debugItem_C);
            flowConfig.debugItemID_G = GetDebugItemProjectID(flowConfig.debugItem_G);
            flowConfig.debugItemID_T = GetDebugItemProjectID(flowConfig.debugItem_T);
            flowConfig.debugItemID_F = GetDebugItemProjectID(flowConfig.debugItem_F);

        }

        /// <summary>
        /// 网络异常时，恢复数据库连接
        /// </summary>
        /// <param name="pSqlConn"></param>
        /// <returns></returns>
        public bool IsDBConnected(SqlConnection pSqlConn)
        {
            if (!pSqlConn.State.ToString().Equals("Open"))
            {
                try
                {
                    pSqlConn.Open();
                }
                catch
                {
                    MessageBox.Show("数据库连接失败，请联系管理员！");
                    return false;
                }
            }
            return true;
        }
    }
}
