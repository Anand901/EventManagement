using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIBRARY;
using MODEL.Admin;
using MODEL.UserEntity;

namespace BL.AdminManagement
{
    public class UserServices
    {
        /// <summary>
        /// Date: 2024-04-05
        /// Name: Anand Sharma
        /// in this method we check the user realated services like register and login
        /// we got the response in database and send back to controller
        /// <param name="userEntity"></param>
        /// <returns></returns>
        public SerializeResponse<UserEntity> UserManagement(UserEntity userEntity)
        {

            InsertLog.WriteErrrorLog("EventManagement=>UserManagement=>Started"); // error log

            #region CreateInstances
            ConvertDataTable bl = new ConvertDataTable();
            SerializeResponse<UserEntity> UserResponsemessage = new SerializeResponse<UserEntity>();    // instance of the serialize response with expense enity

            DataSet ds = new DataSet();
            SqlDataProvider objSDP = new SqlDataProvider();    // instance of the sql data provider
            #endregion

            #region Query
            string query = "_SP_UserManagemnt";         // sp name
            #endregion

            try
            {
                #region ConnectionString
                string Con_str = DBConnection.ConnectionString;         // Connection string
                #endregion

                #region SP_Parameters
                // SP  parameters
                SqlParameter prm1 = objSDP.CreateInitializedParameter("@Flag", DbType.String, userEntity.Flag);
                SqlParameter prm2 = objSDP.CreateInitializedParameter("@Name", DbType.String, userEntity.UserName);
                SqlParameter prm3 = objSDP.CreateInitializedParameter("@Email", DbType.String, userEntity.UserEmail);
                SqlParameter prm4 = objSDP.CreateInitializedParameter("@PhoneNo", DbType.String, userEntity.UserPhoneNo);
                SqlParameter prm5 = objSDP.CreateInitializedParameter("@Adress", DbType.String, userEntity.UserAdress);
                SqlParameter prm6 = objSDP.CreateInitializedParameter("@Password", DbType.String, userEntity.UserPassword);
                #endregion

                #region ParametersArray
                SqlParameter[] Sqlpara = { prm1, prm2, prm3, prm4, prm5, prm6 };
                #endregion

                #region ExecuteDataset
                ds = SqlHelper.ExecuteDataset(Con_str, query, Sqlpara);
                #endregion

                #region Register(Insert) User
                if (userEntity.Flag == "Register" && ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)    // register the user cheek the flag for that and then convert the response from the db
                {
                    UserResponsemessage.Code = Convert.ToString(ds.Tables[0].Rows[0]["Code"]);
                    UserResponsemessage.Message = Convert.ToString(ds.Tables[0].Rows[0]["ResponseMessage"]);
                }
                #endregion

                #region LogInUser
                else if (userEntity.Flag == "LogIn" && ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)      // if flag is LogIn , then perform the peration
                {
                    UserResponsemessage.ArrayOfResponse = bl.ListConvertDataTable<UserEntity>(ds.Tables[0]);
                    UserResponsemessage.Message = "200|Data Found";
                    UserResponsemessage.Code = "200";
                }
                else if (userEntity.Flag == "LogIn" && ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count == 0)
                {
                    UserResponsemessage.Message = "UserName or Password is incorrect";
                    UserResponsemessage.Code = "401";
                }
                #endregion

                #region FlagInvalid
                else
                {
                    UserResponsemessage.Message = "Flag is Invalid";
                    UserResponsemessage.Code = "200";
                }
                #endregion
            }
            catch (Exception ex)
            {
                UserResponsemessage.Message = "500|Exception Occurred";
                InsertLog.WriteErrrorLog("AdminManagemment=>AdminManagement=>Exception" + ex.Message + ex.StackTrace);
            }
            return UserResponsemessage;
        }

    }
}
