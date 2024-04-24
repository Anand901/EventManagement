using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using LIBRARY;
using MODEL.Event;
using MODEL.EventActivitY;
using System.Diagnostics;
using System.Xml.Linq;

namespace BL.EventActivityMangement
{
    public class EventActivityInfoBL
    {
        /// <summary>
        /// Date: 2024-04-06
        /// Name: Anand Sharma
        /// in this method we do the event activities like get,add activities
        /// here we got the respone in the dataset and send back to controller
        /// </summary>
        /// <param name="eventActivityEntity"></param>
        /// <returns></returns>
        public SerializeResponse<EventActivityEntity> EventActivityManage(EventActivityEntity eventActivityEntity)
        {
            InsertLog.WriteErrrorLog("EventActivityServices=>EventActivitymanage=>Started"); //error log

            #region CreateTheInstances
            ConvertDataTable bl = new ConvertDataTable();
            SerializeResponse<EventActivityEntity> EventActivityResponseMessage = new SerializeResponse<EventActivityEntity>();   // create the serialize response of the event activity 
            DataSet ds = new DataSet();                // crate the einstance of the dataset
            SqlDataProvider objSDP = new SqlDataProvider();         // sql data provider instance
            #endregion

            #region Query
            string query = "_SP_EventActivities";    // sp name for excecution
            #endregion
            try 
            {
                #region CheckDateNullOrNot

                if (eventActivityEntity.ActivityStartTime == new DateTime()) // check thata the date is null or not 
                {
                    eventActivityEntity.ActivityStartTime = DateTime.Now; // set the date now for not null
                }
                if (eventActivityEntity.ActivityEndTime == new DateTime())   
                {
                    eventActivityEntity.ActivityEndTime = DateTime.Now;
                }
                #endregion

                #region ConnectionString
                string Con_str = DBConnection.ConnectionString;        // connection string
                #endregion

                #region SP_Parameteres
                // sp parameters
                SqlParameter prm1 = objSDP.CreateInitializedParameter("@Flag", DbType.String, eventActivityEntity.Flag);
                SqlParameter prm2 = objSDP.CreateInitializedParameter("@Name", DbType.String, eventActivityEntity.ActivityName);
                SqlParameter prm3 = objSDP.CreateInitializedParameter("@Description", DbType.String, eventActivityEntity.ActivityDescription);
                SqlParameter prm4 = objSDP.CreateInitializedParameter("@StartDateTime", DbType.DateTime, eventActivityEntity.ActivityStartTime);
                SqlParameter prm5 = objSDP.CreateInitializedParameter("@EndDateTime", DbType.DateTime, eventActivityEntity.ActivityEndTime);
                SqlParameter prm6 = objSDP.CreateInitializedParameter("@Price", DbType.Int64, eventActivityEntity.ActivityPrice);
                SqlParameter prm7 = objSDP.CreateInitializedParameter("@EventName", DbType.String, eventActivityEntity.EventName);
                SqlParameter prm8 = objSDP.CreateInitializedParameter("@Id", DbType.Int64, eventActivityEntity.EventId);
                #endregion

                #region ParametersArray
                SqlParameter[] Sqlpara = { prm1, prm2, prm3, prm4, prm5, prm6, prm7, prm8 };
                #endregion

                #region ExecuteDataSet
                ds = SqlHelper.ExecuteDataset(Con_str, query, Sqlpara);   // call the spl helper method excecute dataset
                #endregion

                #region InsertIntoTable
                if (eventActivityEntity.Flag == "Insert" && ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)    // if flag is insert , then perform the peration
                {
                    EventActivityResponseMessage.Code = Convert.ToString(ds.Tables[0].Rows[0]["Code"]);
                    EventActivityResponseMessage.Message = Convert.ToString(ds.Tables[0].Rows[0]["ResponseMessage"]);
                }
                #endregion

                #region GetDataFormDB 
                else if (eventActivityEntity.Flag == "Get" && ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)     // if flag is get , then perform the operation
                {
                    EventActivityResponseMessage.ArrayOfResponse = bl.ListConvertDataTable<EventActivityEntity>(ds.Tables[0]);
                    EventActivityResponseMessage.Message = "200|Data Found";
                    EventActivityResponseMessage.Code = "200";

                }
                #endregion

                #region UpdateData
                else if (eventActivityEntity.Flag == "Update" && ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)     // if flag is update , then perform the peration
                {
                    EventActivityResponseMessage.Code = Convert.ToString(ds.Tables[0].Rows[0]["Code"]);
                    EventActivityResponseMessage.Message = Convert.ToString(ds.Tables[0].Rows[0]["ResponseMessage"]);
                }
                #endregion

                #region GetEventActivities
                else if (eventActivityEntity.Flag == "GetEventActivities" && ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)    // if flag is getEventActivities , then perform the peration
                {
                    EventActivityResponseMessage.ArrayOfResponse = bl.ListConvertDataTable<EventActivityEntity>(ds.Tables[0]);
                    EventActivityResponseMessage.Message = "200|Data Found";
                    EventActivityResponseMessage.Code = "200";
                }
                else if (eventActivityEntity.Flag == "GetEventActivities" && ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count == 0)
                {
                    EventActivityResponseMessage.ArrayOfResponse = bl.ListConvertDataTable<EventActivityEntity>(ds.Tables[0]);
                    EventActivityResponseMessage.Message = "There is no activities in this event";
                    EventActivityResponseMessage.Code = "400";

                }
                #endregion

                #region FlagInvalid
                else
                {
                    EventActivityResponseMessage.Message = "Flag is Invalid";
                    EventActivityResponseMessage.Code = "200";
                }
                #endregion
            }
            catch (Exception ex)
            {
                EventActivityResponseMessage.Message = "500|Exception Occurred";
                InsertLog.WriteErrrorLog("AdminManagemment=>AdminManagement=>Exception" + ex.Message + ex.StackTrace);
            }
            return EventActivityResponseMessage;
        }
    }
}

