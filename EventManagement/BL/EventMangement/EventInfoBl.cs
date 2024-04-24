using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LIBRARY;
using MODEL.Event;
/*using static System.Net.Mime.MediaTypeNames;*/

namespace BL.EventMangement
{
    public class EventInfoBl
    {
        /// <summary>
        /// Name: Anand Sharma
        /// Date: 06/04/2024
        /// in this method we mange the event related information like get,add
        /// here we got response in the dataset and pass to the controller
        /// </summary>
        /// <param name="Event"></param>
        /// <returns></returns>
        public SerializeResponse<EventEntity> EventManage(EventEntity eventEntity)   // Event Managemnt method
        {

            InsertLog.WriteErrrorLog("EventInfoBl=>EventMange=>Started");  // error log 


            #region CreateTheInstances

            ConvertDataTable bl = new ConvertDataTable();
            SerializeResponse<EventEntity> EventResponseMessage = new SerializeResponse<EventEntity>();
            DataSet ds = new DataSet();
            SqlDataProvider objSDP = new SqlDataProvider();
            #endregion

            #region Query
            string query = "_SP_EventManagement";
            #endregion

            try
            {
                #region HandleTheNullDate

                if(eventEntity.EventStartDate == new DateTime())  // check that the event start date is null or not
                {
                    eventEntity.EventStartDate = DateTime.Now;    // set the data if it is null
                }
               if(eventEntity.EventEndDate == new DateTime()) { 
                    eventEntity.EventEndDate = DateTime.Now;        
                }
                #endregion

                #region ConnectionString
                string Con_str = DBConnection.ConnectionString;   // provide the connection string to connect the database
                #endregion

                #region SP_Parmeteres
                // sp parameters
                SqlParameter prm1 = objSDP.CreateInitializedParameter("@Flag", DbType.String, eventEntity.Flag);
                SqlParameter prm2 = objSDP.CreateInitializedParameter("@Name", DbType.String, eventEntity.EventName);
                SqlParameter prm3 = objSDP.CreateInitializedParameter("@Description", DbType.String, eventEntity.EventDescription);
                SqlParameter prm4 = objSDP.CreateInitializedParameter("@StartDate", DbType.String, eventEntity.EventStartDate);
                SqlParameter prm5 = objSDP.CreateInitializedParameter("@EndDate", DbType.String, eventEntity.EventEndDate);
                SqlParameter prm6 = objSDP.CreateInitializedParameter("@AdminEmail", DbType.String, eventEntity.AdminEmail);
                SqlParameter prm7 = objSDP.CreateInitializedParameter("@Image", DbType.String, eventEntity.EventImage);
                SqlParameter prm8 = objSDP.CreateInitializedParameter("@Id", DbType.Int64, eventEntity.EventId);
                #endregion

                #region SqlParametersArray
                SqlParameter[] Sqlpara = { prm1, prm2, prm3, prm4, prm5, prm6, prm7, prm8 };   // array of sp parameters
                #endregion

                #region ExecuteDataSet
                ds = SqlHelper.ExecuteDataset(Con_str, query, Sqlpara);       // call the method from the sql heplper
                #endregion

                #region InsertIntoTable
                if (eventEntity.Flag == "Insert" && ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)     // if flag is insert , then perform the operation
                {
                    EventResponseMessage.Code = Convert.ToString(ds.Tables[0].Rows[0]["Code"]);
                    EventResponseMessage.Message = Convert.ToString(ds.Tables[0].Rows[0]["ResponseMessage"]);
                }
                #endregion

                #region GetDataFromTable
                 
                else if (eventEntity.Flag == "Get" && ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)   //  if flag is get , then perform the operation
                {
                    EventResponseMessage.ArrayOfResponse = bl.ListConvertDataTable<EventEntity>(ds.Tables[0]);
                    EventResponseMessage.Message = "200|Data Found";
                    EventResponseMessage.Code = "200";
                }
                else if (eventEntity.Flag == "Get" && ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count == 0)
                {   

                    EventResponseMessage.Message = "UserName or Password is incorrect";
                    EventResponseMessage.Code = "401";
                }
                #endregion

                #region GetEventName

                else if (eventEntity.Flag == "GetEventName" && ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)     // if flag is GetEventName , then perform the peration
                {
                    EventResponseMessage.ArrayOfResponse = bl.ListConvertDataTable<EventEntity>(ds.Tables[0]);
                    EventResponseMessage.Message = "200|Data Found";
                    EventResponseMessage.Code = "200";
                }
                #endregion

                #region GetEventDate

                else if (eventEntity.Flag == "GetEventDate" && ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)    // if flag is getEventDate , then perform the peration
                {
                    EventResponseMessage.ArrayOfResponse = bl.ListConvertDataTable<EventEntity>(ds.Tables[0]);
                    EventResponseMessage.Message = "200|Data Found";
                    EventResponseMessage.Code = "200";
                }
                #endregion

                #region UpdateTheEventPublishStatus


                else if (eventEntity.Flag == "Update" && ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)    // if flag is Update , then perform the peration
                {
                    EventResponseMessage.Code = Convert.ToString(ds.Tables[0].Rows[0]["Code"]);
                    EventResponseMessage.Message = Convert.ToString(ds.Tables[0].Rows[0]["ResposeMessage"]);
                }
                #endregion

                #region FlagIsInvalid

                else
                {
                    EventResponseMessage.Message = "Flag is Invalid";
                    EventResponseMessage.Code = "200";
                }
                #endregion
            }
            catch (Exception ex)
            {
                EventResponseMessage.Message = "500|Exception Occurred";
                InsertLog.WriteErrrorLog("EventInfoBl=>EventMange=>Exception" + ex.Message + ex.StackTrace);
            }
            return EventResponseMessage;
        }
    }

}

