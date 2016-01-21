using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Technology
{
    public class TechnologyLib
    {
        private SqlConnection sqlConnection;

        public TechnologyLib(SqlConnection mainConnection)
        {
            sqlConnection = mainConnection;
        }

        public int GetMaxFlowProjectID()
        {
            int maxFlowProjectID = 0;
            string strSql = string.Format("SELECT MAX(FlowProjectID) FROM FlowProject");
            SqlCommand cmd = new SqlCommand(strSql,sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();

            try
            { 
                if (reader.HasRows)
                {
                    reader.Read();
                    maxFlowProjectID = Convert.ToInt32(reader[0]);
                    reader.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            return maxFlowProjectID;
        }

        public bool isExit(string pStrSql)
        {
            bool rc = false;
            SqlCommand cmd = new SqlCommand(pStrSql,sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();

            try 
            {
                if(reader.HasRows)
                {
                    reader.Read();
                    rc = true;
                }
                reader.Close();
            }
            catch(Exception ex)
            {
                rc = false;
                MessageBox.Show(ex.Message.ToString());
            }
            return rc;
        }

        /// <summary>
        /// 获得调试方案编码及名称
        /// </summary>
        /// <param name="ds"></param>
        public void GetDebugItemProjectData(DataSet ds)
        {
            string sqlStr = string.Format("SELECT ProjectID AS '调试方案编码',ProjectName AS '调试方案名称',Description AS '描述' FROM DebugItemProject");
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            sqlCommand.CommandTimeout = 30;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;
            sqlDataAdapter.Fill(ds, "DebugItemProject");
        }

        public void GetDebugItemProjectDetailsViewData(DataSet ds)
        {
            string sqlStr = string.Format("SELECT ItemSerialNo,DebugItemID,DebugItemName,ItemTypeName,StandardValue,Description,ProjectID,ProjectName,ItemTypeID FROM DebugItemProjectDetailsView ORDER BY ItemSerialNo ASC");
            SqlCommand sqlCommand = new SqlCommand(sqlStr,sqlConnection);
            sqlCommand.CommandTimeout = 30;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;
            sqlDataAdapter.Fill(ds, "DebugItemProjectDetailsView");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        public void GetDebugItemData(DataSet ds)
        {
            string sqlStr = string.Format("SELECT * FROM DebugItem");
            SqlCommand sqlCommand = new SqlCommand(sqlStr,sqlConnection);
            sqlCommand.CommandTimeout = 30;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;
            sqlDataAdapter.Fill(ds, "DebugItem");
        }

        public void GetDebugItemProjectDetailsViewData(DataSet ds, string pDebugItemProjectName)
        {
            string sqlStr = string.Format("SELECT ItemSerialNo,DebugItemID,DebugItemName,ItemTypeName,StandardValue,Description,ProjectID,ProjectName,ItemTypeID FROM DebugItemProjectDetailsView WHERE ProjectName = '{0}' ORDER BY ItemSerialNo ASC", pDebugItemProjectName);
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            sqlCommand.CommandTimeout = 30;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;
            sqlDataAdapter.Fill(ds, "OneDebugItemProjectDetailsView");
        }

        /// <summary>
        /// 获得指定调试方案的调试编码
        /// </summary>
        /// <param name="pDebugItemProjectName"></param>
        /// <returns></returns>
        public int GetDebugItemProjectID(string pDebugItemProjectName)
        {
            int debugItemProjectID = 0;
            string sqlStr = string.Format("SELECT ProjectID from DebugItemProject where ProjectName = '{0}'", pDebugItemProjectName);
            SqlCommand sqlCommand = new SqlCommand(sqlStr,sqlConnection);
            sqlCommand.CommandTimeout = 30;
            SqlDataReader reader = sqlCommand.ExecuteReader();

            try
            { 
                if (reader.HasRows)
                {
                    reader.Read();
                    debugItemProjectID = Convert.ToInt32(reader["ProjectID"]);
                }
                reader.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return debugItemProjectID;
        }

        /// <summary>
        /// 删除数据库中对应DebugItemProjectDetails的DebugItemProject数据
        /// </summary>
        public void DeleteDebugItemProjectDetailsData(int pDebugItemProjectID)
        {
            string sqlStr = string.Format("delete from DebugItemProjectDetails where ProjectID = {0}", pDebugItemProjectID);
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            sqlCommand.CommandTimeout = 30;
            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                sqlCommand.Dispose();
            }
        }

        public bool IsSameDebugItemProject(string pProjectName)
        {
            bool rc = true;
            string sqlStr = string.Format("select count(*) from DebugItemProject where ProjectName = '{0}'", pProjectName);
            SqlCommand sqlCmd = new SqlCommand(sqlStr,sqlConnection);
            SqlDataReader reader = sqlCmd.ExecuteReader();
            try
            {
                reader.Read();
                int count = (int)reader.GetValue(0);

                if (count != 0)
                {
                    rc = true;
                }
                else
                {
                    rc = false;
                }
                reader.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                rc = false;
            }
            return rc;
        }

        public bool IsSameDebugItemProject(string pProjectName,int pProjectID)
        {
            bool rc = true;
            string sqlStr = string.Format("select count(*) from DebugItemProject where ProjectName = '{0}' and ProjectID != {1}", pProjectName,pProjectID);
            SqlCommand sqlCmd = new SqlCommand(sqlStr, sqlConnection);
            SqlDataReader reader = sqlCmd.ExecuteReader();
            try
            {
                reader.Read();
                int count = (int)reader.GetValue(0);

                if (count != 0)
                {
                    rc = true;
                }
                else
                {
                    rc = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                rc = false;
            }
            return rc;
        }

        public void GetUserPopedomData(DataSet ds)
        {
            string sqlStr = string.Format("SELECT PopedomID,PopedomName,Description FROM UserPopedom");
            SqlCommand cmd = new SqlCommand(sqlStr,sqlConnection);
            cmd.CommandTimeout = 30;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(ds, "UserPopedom");
        }

        /// <summary>
        /// 判断工序信息表中是否存在该工序方案记录
        /// </summary>
        /// <param name="pDebugItemProjectID"></param>
        public bool IsDebugItemProjectIDInT_DebugInfo(int pDebugItemProjectID)
        {
            bool rc = true;
            string sqlStr = string.Format("select count(*) from T_DebugInfo where DebugItemProjectID = {0}", pDebugItemProjectID);
            SqlCommand cmd = new SqlCommand(sqlStr,sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();

            try
            {
                reader.Read();
                int count = (int)reader.GetValue(0);
                if (count != 0)
                {
                    rc = true;
                }
                else
                {
                    rc = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                rc = false;
            }
            return rc;
        }

        /// <summary>
        /// 删除数据库中DebugItemProject表相应的记录
        /// </summary>
        /// <param name="pDebugItemProjectID"></param>
        public void DeleteDebugItemProjectData(int pDebugItemProjectID)
        {
            string sqlStr = string.Format("delete from DebugItemProject where ProjectID = {0}", pDebugItemProjectID);
            SqlCommand cmd = new SqlCommand(sqlStr,sqlConnection);
            cmd.CommandTimeout = 30;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                cmd.Dispose();
            }
        }

        /// <summary>
        /// 获取调试项目信息
        /// </summary>
        /// <param name="ds"></param>
        public void GetDebugItemViewData(DataSet ds)
        {
            string sqlStr = string.Format("SELECT DebugItemID,DebugItemName,StandardValue,ItemTypeName,Description,ItemTypeID FROM DebugItemView");
            SqlCommand sqlCommand = new SqlCommand(sqlStr,sqlConnection);
            sqlCommand.CommandTimeout = 30;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = sqlCommand;

            adapter.Fill(ds, "DebugItemView");
        }

        /// <summary>
        /// 获取调试命令信息
        /// </summary>
        /// <param name="ds"></param>
        public void GetDebugItemDetailsViewData(DataSet ds)
        {
            string sqlStr = string.Format("SELECT CmdSerialNo,CmdID,CmdName,Description,DebugItemID,DebugItemName FROM DebugCmdItemDetailsView ORDER BY CmdSerialNo ASC");
            SqlCommand sqlCommand = new SqlCommand(sqlStr,sqlConnection);
            sqlCommand.CommandTimeout = 30;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = sqlCommand;

            adapter.Fill(ds, "DebugItemDetailsView");
        }

        /// <summary>
        /// 获取数据库中CmdItemDetailsView数据
        /// </summary>
        /// <param name="ds"></param>
        public void GetCmdItemDetailsViewData(DataSet ds)
        {
            string sqlStr = string.Format("SELECT CmdID,CmdName,Description,CommBof,CommCtrl,CommAddress,CommDataType,CommVSQ,CommCOT,CommIdentify,CommStandby,CommEof,ProtocalName,CommPortName,CommDelayTime,ProtocalID,CommPortID,Expara,ExWaitTime FROM CmdItemDetailsView");
            SqlCommand sqlCommand = new SqlCommand(sqlStr,sqlConnection);
            sqlCommand.CommandTimeout = 30;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;
            sqlDataAdapter.Fill(ds, "CmdItemDetailsView");
        }

        /// <summary>
        /// 获取数据库中Protocal数据
        /// </summary>
        /// <param name="ds"></param>
        public void GetProtocalData(DataSet ds)
        {
            string sqlStr = string.Format("SELECT ProtocalID,ProtocalName,Description FROM Protocal");
            SqlCommand sqlCommand = new SqlCommand(sqlStr,sqlConnection);
            sqlCommand.CommandTimeout = 30;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;
            sqlDataAdapter.Fill(ds, "Protocal");
        }

        public void GetCommPortData(DataSet ds)
        {
            string sqlStr = string.Format("SELECT CommPortID,CommPortName,Description FROM CommPort");
            SqlCommand sqlCommand = new SqlCommand(sqlStr,sqlConnection);
            sqlCommand.CommandTimeout = 30;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;
            sqlDataAdapter.Fill(ds, "CommPort");
        }

        public void GetFlowDetailsData(DataSet ds)
        {
            string sqlStr = string.Format("SELECT FlowID,FlowName,Description FROM Flow");
            SqlCommand sqlCommand = new SqlCommand(sqlStr,sqlConnection);
            sqlCommand.CommandTimeout = 30;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;
            sqlDataAdapter.Fill(ds, "FlowDetails");
        }

        public void GetFlowSubData(DataSet ds)
        {
            string sqlStr = string.Format("SELECT FlowSubID, FlowSubName, FlowID FROM FlowSub");
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            sqlCommand.CommandTimeout = 30;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;
            sqlDataAdapter.Fill(ds, "FlowSub");
        }

        //获取用户组明细
        public void GetUserGrpDetailsViewData(DataSet ds)
        {
            string sqlStr = "SELECT UserGrpID,UserGrpName,RoleName,PopedomName,Description,RoleID,PopedomID FROM UserGrpDetailsView"
                + " ORDER BY UserGrpID ASC";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            sqlCommand.CommandTimeout = 30;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;

            sqlDataAdapter.Fill(ds, "UserGrpDetailsView");
        }

        //获取用户明细
        public void GetUserInfoDetailsViewData(DataSet ds)
        {
            string sqlStr = "SELECT UserID,BarCode,UserName,Password,Sex,Birthday,Telephone,Description,UserGrpID,UserGrpName,Status FROM UserInfoDetailsView"
                + " ORDER BY UserID ASC";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            sqlCommand.CommandTimeout = 30;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;

            sqlDataAdapter.Fill(ds, "UserInfoDetailsView");
        }

        //获取角色信息
        public void GetRoleData(DataSet ds)
        {
            string sqlStr = "SELECT RoleID,RoleName,Description FROM Role";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            sqlCommand.CommandTimeout = 30;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;

            sqlDataAdapter.Fill(ds, "Role");
        }

        //获取用户组信息
        public void GetUserGrpData(DataSet ds, string pUserGrpName)
        {
            string sqlStr = "SELECT UserGrpID,UserGrpName,Description FROM UserGrp"
                + " where UserGrpName = '" + pUserGrpName + "'";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            sqlCommand.CommandTimeout = 30;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;

            sqlDataAdapter.Fill(ds, "UserGrp");
        }

        //获取用户组ID
        public int GetUserGrpID(string pUserGrpName)
        {
            int userGrpID = 0;
            string sqlStr = "SELECT UserGrpID from UserGrp where UserGrpName = '" + pUserGrpName + "'";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    userGrpID = Convert.ToInt16(reader["UserGrpID"]);
                }
                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
            }

            return userGrpID;
        }

        //判断是否为重复用户组名称
        public bool IsSameUserGrpName(string pUserGrpName)
        {
            bool rc = true;
            string sqlStr = "select count(*) from UserGrp where UserGrpName = '" + pUserGrpName + "'";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                reader.Read();
                int count;
                count = (int)reader.GetValue(0);
                if (count != 0)
                    rc = true;
                else
                    rc = false;
                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
                rc = false;   //出错时返回，不往下执行
            }
            return rc;
        }

        //判断是否为重复用户组名称(排出当前用户组ID)
        public bool IsSameUserGrpName(string pUserGrpName, int pUserGrpID)
        {
            bool rc = true;
            string sqlStr = "select count(*) from UserGrp where UserGrpName = '" + pUserGrpName + "'"
                + " and UserGrpID != " + pUserGrpID;
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                reader.Read();
                int count;
                count = (int)reader.GetValue(0);
                if (count != 0)
                    rc = true;
                else
                    rc = false;
                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
                rc = false;   //出错时返回，不往下执行
            }
            return rc;
        }

        //判断是否为重复权限名称
        public bool IsSamePopedom(string pPopedomName)
        {
            bool rc = true;
            string sqlStr = "select count(*) from UserPopedom where PopedomName = '" + pPopedomName + "'";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                reader.Read();
                int count;
                count = (int)reader.GetValue(0);
                if (count != 0)
                    rc = true;
                else
                    rc = false;
                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
                rc = false;   //出错时返回，不往下执行
            }
            return rc;
        }

        //判断是否为重复用户组名称(排出当前用户组ID)
        public bool IsSamePopedom(string pPopedomName, int pPopedomID)
        {
            bool rc = true;
            string sqlStr = "select count(*) from UserPopedom where PopedomName = '" + pPopedomName + "'"
                + " and PopedomID != " + pPopedomID;
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                reader.Read();
                int count;
                count = (int)reader.GetValue(0);
                if (count != 0)
                    rc = true;
                else
                    rc = false;
                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
                rc = false;   //出错时返回，不往下执行
            }
            return rc;
        }

        //判断权限方案是否存在于用户组中
        public bool IsPopedomInUserGrp(string pPopedomName)
        {
            bool rc = true;
            string sqlStr = "select count(*) from UserGrpDetailsView where PopedomName = '" + pPopedomName + "'";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                reader.Read();
                int count;
                count = (int)reader.GetValue(0);
                if (count != 0)
                    rc = true;
                else
                    rc = false;
                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
                rc = false;   //出错时返回，不往下执行
            }
            return rc;
        }

        //获取UserPopedomID
        public int GetUserPopedomID(string pUserPopedom)
        {
            int userPopedomID = 0;
            string sqlStr = "SELECT PopedomID from UserPopedom where PopedomName = '" + pUserPopedom + "'";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    userPopedomID = Convert.ToInt16(reader["PopedomID"]);
                }
                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
            }

            return userPopedomID;
        }

        //根据工序名称返回工序ID
        public int GetFlowProjectID(string pFlowProjectName)
        {
            int flowProjectID = 0;
            string sqlStr = "SELECT FlowProjectID from FlowProject where FlowProjectName = '" + pFlowProjectName + "'";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    flowProjectID = Convert.ToInt16(reader["FlowProjectID"]);
                }
                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
            }

            return flowProjectID;
        }

        //根据CmdItem名称返回CmdItemID
        public int GetCmdItemID(string pCmdItemName)
        {
            int cmdItemID = 0;
            string sqlStr = "SELECT CmdID from CmdItem where CmdName = '" + pCmdItemName + "'";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    cmdItemID = Convert.ToInt16(reader["CmdID"]);
                }
                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
            }

            return cmdItemID;
        }

        public int GetDebugItemID(string pDebugItemName)
        {
            int debugItemID = 0;
            string sqlStr = "SELECT DebugItemID from DebugItem where DebugItemName = '" + pDebugItemName + "'";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    debugItemID = Convert.ToInt16(reader["DebugItemID"]);
                }
                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
            }

            return debugItemID;
        }

        /// <summary>
        /// 获取对应的ModuleClassID数据
        /// </summary>
        public int GetModuleClassID(string pModuleClass)
        {
            int moduleClassID = 0;
            string sqlStr = "SELECT ClassID from ModuleClass where ClassName = '" + pModuleClass + "'";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    moduleClassID = Convert.ToInt16(reader["ClassID"]);
                }
                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
            }

            return moduleClassID;
        }

        /// <summary>
        /// 根据Project名称返回ProjectID
        /// </summary>
        public int GetT_TestRunProjectID(string pProjectName)
        {
            int projectID = 0;
            string sqlStr = "SELECT ProjectID from T_TestRunProject where ProjectName = '" + pProjectName + "'";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    projectID = Convert.ToInt16(reader["ProjectID"]);
                }
                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
            }

            return projectID;
        }

        public int GetVerifyModuleID(string pVerifyModuleName)
        {
            int verifyModuleID = 0;
            string sqlStr = "SELECT VerifyModuleID from VerifyModule where VerifyModuleName = '" + pVerifyModuleName + "'";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    verifyModuleID = Convert.ToInt16(reader["VerifyModuleID"]);
                }
                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
            }

            return verifyModuleID;
        }

        /// <summary>
        /// 获取对应的VerifyPopedomID数据
        /// </summary>
        public int GetVerifyPopedomID(string pUserPopedom)
        {
            int userPopedomID = 0;
            string sqlStr = "SELECT PopedomID from VerifyPopedom where PopedomName = '" + pUserPopedom + "'";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    userPopedomID = Convert.ToInt16(reader["PopedomID"]);
                }
                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
            }

            return userPopedomID;
        }

        //判断是否为重复用户条码
        public bool IsSameUserInfoBarCode(string barcode)
        {
            bool rc = true;
            string sqlStr = "select count(*) from UserInfo where BarCode = '" + barcode + "'";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                reader.Read();
                int count;
                count = (int)reader.GetValue(0);
                if (count != 0)
                    rc = true;
                else
                    rc = false;
                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
                rc = false;   //出错时返回，不往下执行
            }
            return rc;
        }

        //获取用户最大条码
        public int GetUserInfoMaxBarCode()
        {
            int maxBarCode = 0;
            string sqlStr = string.Format("SELECT max(BarCode) AS BarCode FROM UserInfo");
            SqlCommand cmd = new SqlCommand(sqlStr,sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();
            try
            { 
                if(reader.HasRows)
                {
                    reader.Read();
                    maxBarCode = Convert.ToInt32(reader["BarCode"]);
                }
                reader.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }

            return maxBarCode;
        }

        //判断用户操作中是否存在用户操作记录
        public bool IsUserInUserOperate(string pBarCode)
        {
            bool rc = true;
            string sqlStr = "select count(*) from UserOperateDetailsView where BarCode = '" + pBarCode + "'";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                reader.Read();
                int count;
                count = (int)reader.GetValue(0);
                if (count != 0)
                    rc = true;
                else
                    rc = false;
                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
                rc = false;   //出错时返回，不往下执行
            }
            return rc;
        }

        /// <summary>
        /// 删除数据库中对应UserInfo的User数据,
        /// </summary>
        public void DeleteUserInfoData(string pBarCode)
        {
            //string sqlStr = "delete from UserInfo where BarCode = '" + pBarCode + "'";
            string sqlStr = string.Format("UPDATE UserInfo SET UserGrpID = '999' WHERE BarCode = {0}", pBarCode);
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            sqlCommand.CommandTimeout = 30;
            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
                sqlCommand.Dispose();
            }
        }

        /// <summary>
        /// //获取数据库中Flow数据
        /// </summary>
        public void GetFlowProjectData(DataSet ds)
        {
            string sqlStr = "SELECT FlowProjectID,FlowProjectName,Description FROM FlowProject";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            sqlCommand.CommandTimeout = 30;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;

            sqlDataAdapter.Fill(ds, "FlowProject");
        }

        /// <summary>
        /// //获取数据库中FlowType数据
        /// </summary>
        public void GetFlowTypeData(DataSet ds)
        {
            string sqlStr = "SELECT FlowTypeID, FlowTypeName, Description FROM FlowType";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            sqlCommand.CommandTimeout = 30;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;

            sqlDataAdapter.Fill(ds, "FlowType");
        }

        //根据工艺类型名称获取工艺类型ID
        //public int GetFlowTypeID(string FlowTypeName)
        //{
        //    int flowTypeID = 0;
        //    string sqlStr = string.Format("SELECT FlowTypeID, FlowTypeName, Description FROM FlowType WHERE FlowTypeName = '{0}'", FlowTypeName);
        //    SqlCommand cmd = new SqlCommand(sqlStr,sqlConnection);
        //    SqlDataReader reader = cmd.ExecuteReader();
        //    try
        //    {
        //        if(reader.HasRows)
        //        {
        //            reader.Read();
        //            flowTypeID = (int)reader.GetValue(0);
        //            reader.Close();
        //        }
        //    }
        //    catch(Exception e)
        //    {
        //        MessageBox.Show(e.Message.ToString());
        //    }
        //    return flowTypeID;
        //}

        /// <summary>
        /// //获取数据库中FlowProjectDetailsView数据
        /// </summary>
        public void GetFlowProjectDetailsViewData(DataSet ds)
        {
            string sqlStr = "SELECT FlowSerialNo,FlowID,FlowName,PreFlowName,BackFlowName,BackFlowID,PreFlowID,FlowProjectID,IsParent,OwnerID,FlowProjectName FROM FlowProjectDetailsView"
            + " ORDER BY FlowSerialNo ASC";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            sqlCommand.CommandTimeout = 30;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;

            sqlDataAdapter.Fill(ds, "FlowProjectDetailsView");
        }

        /// <summary>
        /// //获取数据库中FlowProjectDetailsView数据
        /// </summary>
        public void GetFlowProjectDetailsViewData(DataSet ds, string pFlowProjectName)
        {
            string sqlStr = "SELECT FlowID,FlowName,PreFlowName,BackFlowName,BackFlowID,PreFlowID,FlowProjectID,FlowProjectName,FlowSerialNo FROM FlowProjectDetailsView"
                + " WHERE FlowProjectName = '" + pFlowProjectName + "'" + " ORDER BY FlowSerialNo ASC";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            sqlCommand.CommandTimeout = 30;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;

            sqlDataAdapter.Fill(ds, "OneFlowProjectDetailsView");
        }

        /// <summary>
        /// //通过FlowProjectID获取数据库中FlowProjectDetailsView数据
        /// </summary>
        public void GetFlowProjectDetailsDataWithFlowProjectID(DataSet ds, int pFlowProjectID)
        {
            string sqlStr = string.Format("SELECT FlowID,FlowSerialNo,FlowName,(CASE IsParent WHEN 0 THEN '是' ELSE '否' END) IsParent ,FlowProjectName FROM FlowProjectDetailsView WHERE FlowProjectID = {0} ORDER BY FlowSerialNo", pFlowProjectID);
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            sqlCommand.CommandTimeout = 30;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;

            sqlDataAdapter.Fill(ds, "FlowProjectDetailsDataWithFlowProjectID");
        }

        /// <summary>
        /// //按照DebugProjectID获取数据库中DebugItemWithDebugProjectID数据
        /// </summary>
        public void GetDebugItemWithDebugProjectID(DataSet ds, int pDebugProjectID)
        {
            string sqlStr = string.Format("SELECT DebugItemID,ItemSerialNo,DebugItemName,StandardValue,ItemTypeID,ProjectName FROM DebugItemProjectDetailsView WHERE ProjectID = {0} ORDER BY ItemSerialNo", pDebugProjectID);
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            sqlCommand.CommandTimeout = 30;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;

            sqlDataAdapter.Fill(ds, "DebugItemWithDebugProjectID");
        }

        //获取订单信息
        public void GetProductionOrderData(DataSet ds, string pOrderCode)
        {
            string strSql = string.Format("SELECT OrderID,OrderCode,Type,Specification,BarCode,Customer,Number,Workshop,ProductName,PackingNumber,Voltage,jingdu FROM ProductionOrder WHERE OrderCode = '{0}'", pOrderCode);
            SqlCommand cmd = new SqlCommand(strSql,sqlConnection);
            cmd.CommandTimeout = 30;
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            sda.Fill(ds, "ProductionOrder");
        }

        public void GetProductOrderFlowDebugProjectData(DataSet ds ,int pOrderID)
        {
            string strSql = string.Format("SELECT OrderID,FlowProjectID,DebugProjectID FROM OrderSchemeRS WHERE OrderID = {0}",pOrderID);
            SqlCommand cmd = new SqlCommand(strSql,sqlConnection);
            cmd.CommandTimeout = 30;
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            sda.Fill(ds, "ProductOrderFlowDebugProject");
        }

        //判断是否为重复工序方案名称
        public bool IsSameFlowProject(string pFlowProjectName)
        {
            bool rc = true;
            string sqlStr = "select count(*) from FlowProject where FlowProjectName = '" + pFlowProjectName + "'";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                reader.Read();
                int count;
                count = (int)reader.GetValue(0);
                if (count != 0)
                    rc = true;
                else
                    rc = false;
                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
                rc = false;   //出错时返回，不往下执行
            }
            return rc;
        }

        //判断是否为重复工序名称(排出当前工序ID)
        public bool IsSameFlowProject(string pFlowProjectName, int pFlowProjectID)
        {
            bool rc = true;
            string sqlStr = "select count(*) from FlowProject where FlowProjectName = '" + pFlowProjectName + "'"
                + " and FlowProjectID != " + pFlowProjectID;
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                reader.Read();
                int count;
                count = (int)reader.GetValue(0);
                if (count != 0)
                    rc = true;
                else
                    rc = false;
                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
                rc = false;   //出错时返回，不往下执行
            }
            return rc;
        }

        //判断工序信息表中是否存在该工序方案记录
        public bool IsFlowProjectInT_FlowInfo(string pFlowProjectName)
        {
            bool rc = true;
            string sqlStr = "select count(*) from T_FlowInfoView where FlowProjectName = '" + pFlowProjectName + "'";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                reader.Read();
                int count;
                count = (int)reader.GetValue(0);
                if (count != 0)
                    rc = true;
                else
                    rc = false;
                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
                rc = false;   //出错时返回，不往下执行
            }
            return rc;
        }

        /// <summary>
        /// 删除数据库中对应FlowProjectName的FlowProject数据,
        /// </summary>
        public void DeleteFlowProjectData(string pFlowProjectName)
        {
            string sqlStr = "delete from FlowProject where FlowProjectName = '" + pFlowProjectName + "'";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            sqlCommand.CommandTimeout = 30;
            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
                sqlCommand.Dispose();
            }
        }

        /// <summary>
        /// 删除数据库中对应FlowProjectID的FlowProjectDetails数据,
        /// </summary>
        public void DeleteFlowProjectDetailsData(int pFlowProjectID)
        {
            string sqlStr = "delete from FlowProjectDetails where FlowProjectID = " + pFlowProjectID;
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            sqlCommand.CommandTimeout = 30;
            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
                sqlCommand.Dispose();
            }
        }

        /// <summary>
        /// //获取数据库中Flow数据
        /// </summary>
        public void GetFlowData(DataSet ds)
        {
            //			string sqlStr="SELECT FlowID,FlowName,Description FROM Flow where FlowID >= 0";
            string sqlStr = "SELECT FlowID,FlowName,Description FROM Flow";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            sqlCommand.CommandTimeout = 30;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;

            sqlDataAdapter.Fill(ds, "Flow");
        }

        /// <summary>
        /// 根据工序名称返回工序ID
        /// </summary>
        public int GetFlowID(string pFlowName)
        {
            int flowID = 0;
            string sqlStr = "SELECT FlowID from Flow where FlowName = '" + pFlowName + "'";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    flowID = Convert.ToInt16(reader["FlowID"]);
                }
                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
            }

            return flowID;
        }

        //判断是否为重复工序名称
        public bool IsSameFlow(string pFlowName)
        {
            bool rc = true;
            string sqlStr = "select count(*) from Flow where FlowName = '" + pFlowName + "'";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                reader.Read();
                int count;
                count = (int)reader.GetValue(0);
                if (count != 0)
                    rc = true;
                else
                    rc = false;
                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
                rc = false;   //出错时返回，不往下执行
            }
            return rc;
        }

        //判断是否为重复工序名称(排出当前工序ID)
        public bool IsSameFlow(string pFlowName, int pFlowID)
        {
            bool rc = true;
            string sqlStr = "select count(*) from Flow where FlowName = '" + pFlowName + "'"
                + " and FlowID != " + pFlowID;
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                reader.Read();
                int count;
                count = (int)reader.GetValue(0);
                if (count != 0)
                    rc = true;
                else
                    rc = false;
                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
                rc = false;   //出错时返回，不往下执行
            }
            return rc;
        }

        //判断工序方案中是否存在工序记录
        public bool IsFlowInFlowProjectDetails(string pFlowName)
        {
            bool rc = true;
            string sqlStr = "select count(*) from FlowProjectDetailsView where FlowName = '" + pFlowName + "'";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                reader.Read();
                int count;
                count = (int)reader.GetValue(0);
                if (count != 0)
                    rc = true;
                else
                    rc = false;
                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
                rc = false;   //出错时返回，不往下执行
            }
            return rc;
        }

        //判断用户权限中是否存在该工序记录
        public bool IsFlowInUserPopedomDetailsView(string pFlowName)
        {
            bool rc = true;
            string sqlStr = "select count(*) from UserPopedomDetailsView where FlowName = '" + pFlowName + "'";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                reader.Read();
                int count;
                count = (int)reader.GetValue(0);
                if (count != 0)
                    rc = true;
                else
                    rc = false;
                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
                rc = false;   //出错时返回，不往下执行
            }
            return rc;
        }

        /// <summary>
        /// 删除数据库中对应Flow的Flow数据,
        /// </summary>
        public void DeleteFlowData(string pFlowName)
        {
            string sqlStr = "delete from Flow where FlowName = '" + pFlowName + "'";
            SqlCommand sqlCommand = new SqlCommand(sqlStr, sqlConnection);
            sqlCommand.CommandTimeout = 30;
            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
                sqlCommand.Dispose();
            }
        }

        //获取用户最大条码
        public int GetMaxFlowID()
        {
            int maxFlowID = 0;
            string sqlStr = string.Format("SELECT MAX(FlowID) AS FlowID FROM Flow");
            SqlCommand cmd = new SqlCommand(sqlStr, sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    maxFlowID = Convert.ToInt32(reader[0]);
                }
                reader.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }

            return maxFlowID;
        }
    }
}
