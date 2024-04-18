using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIBRARY;
using MODEL.Admin;
using System.Xml.Linq;
using MODEL.UserEntity;

namespace BL.UserManagemernt
{
    public class AdminServices
    {
        /// <summary>
        /// Date: 2024-04-05
        /// Name: Anand Sharma
        /// in this method we check the admin realated services like register and login
        /// we got the response in database and send back to controller
        /// </summary>
        /// <param name="AdminEntity"></param>
        /// <returns></returns>
        public SerializeResponse<AdminEntity> AdminManagement(AdminEntity AdminEntity)  // Admin mangemnt Method
        {
            {
                InsertLog.WriteErrrorLog("EventManagement=>AdminManagement=>Started");  // error log

                #region CreateInstances
                ConvertDataTable bl = new ConvertDataTable();
                SerializeResponse<AdminEntity> AdminResponsemessage = new SerializeResponse<AdminEntity>();    // instance of the serialize response with expense enity
                DataSet ds = new DataSet();
                SqlDataProvider objSDP = new SqlDataProvider();
                #endregion

                #region Query
                string query = "_SP_AdminManagemnt";      // provide the sp name
                #endregion 

                try
                {   
                    #region ConnectionString
                    string Con_str = DBConnection.ConnectionString;      // Connection string
                    #endregion 

                    #region SP_Parameters
                    // SP parameters
                    SqlParameter prm1 = objSDP.CreateInitializedParameter("@Flag", DbType.String, AdminEntity.Flag);
                    SqlParameter prm2 = objSDP.CreateInitializedParameter("@Name", DbType.String, AdminEntity.AdminName);
                    SqlParameter prm3 = objSDP.CreateInitializedParameter("@Email", DbType.String, AdminEntity.AdminEmail);
                    SqlParameter prm4 = objSDP.CreateInitializedParameter("@PhoneNo", DbType.String, AdminEntity.AdminPhoneNo);
                    SqlParameter prm5 = objSDP.CreateInitializedParameter("@Adress", DbType.String, AdminEntity.AdminAdress);
                    SqlParameter prm6 = objSDP.CreateInitializedParameter("@Password", DbType.String, AdminEntity.AdminPassword);
                    #endregion

                    #region ParametersArray
                    SqlParameter[] Sqlpara = { prm1, prm2, prm3, prm4, prm5, prm6 };
                    #endregion

                    #region ExecuteDataset
                    ds = SqlHelper.ExecuteDataset(Con_str, query, Sqlpara);
                    #endregion

                    #region RegisterAdmin
                    if (AdminEntity.Flag == "Register" && ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)     // if flag is Register , then perform the peration
                    {
                        AdminResponsemessage.Code = Convert.ToString(ds.Tables[0].Rows[0]["Code"]);
                        AdminResponsemessage.Message = Convert.ToString(ds.Tables[0].Rows[0]["ResponseMessage"]);
                    }
                    #endregion

                    #region LogIn 
                    else if (AdminEntity.Flag == "LogIn" && ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)       // if flag is LogIn , then perform the peration
                    {
                        if (ds.Tables[0].Columns.Count != 2)
                        {
                            AdminResponsemessage.ArrayOfResponse = bl.ListConvertDataTable<AdminEntity>(ds.Tables[0]);
                            AdminResponsemessage.Message = "Log In Successfull";
                            AdminResponsemessage.Code = "200";
                        }
                        else
                        {
                            AdminResponsemessage.Message = Convert.ToString(ds.Tables[0].Rows[0]["ResponseMessage"]);
                            AdminResponsemessage.Code = Convert.ToString(ds.Tables[0].Rows[0]["Code"]);
                        }
                    }
                    else if (AdminEntity.Flag == "LogIn" && ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count == 0)
                    {
                        AdminResponsemessage.Message = "UserName or Password is incorrect";
                        AdminResponsemessage.Code = "401";
                    }
                    #endregion

                    #region FlagInvalid
                    else
                    {
                        AdminResponsemessage.Message = "Flag is Invalid";
                        AdminResponsemessage.Code = "200";
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    AdminResponsemessage.Message = "500|Exception Occurred";
                    InsertLog.WriteErrrorLog("AdminManagemment=>AdminManagement=>Exception" + ex.Message + ex.StackTrace);
                }
                return AdminResponsemessage;
            }
        }
    }
}