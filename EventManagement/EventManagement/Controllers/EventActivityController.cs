using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BL.EventActivityMangement;
using BL.EventMangement;
using LIBRARY;
using MODEL.Event;
using MODEL.EventActivitY;
using Newtonsoft.Json;

namespace EventManagement.Controllers
{
    public class EventActivityController : ApiController
    {
        #region Instance
        EventActivityInfoBL EventActivity = new EventActivityInfoBL();    // instance of the event activity management 
        #endregion

        /// <summary>
        /// Date:2024-04-05
        /// Name: anand Sharma
        /// in this we create the controller to add the event activity
        /// </summary>
        /// <param name="eventActivity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/EventActivity/AddActivity")]
        public HttpResponseMessage EventActivityAdd(EventActivityEntity eventActivity)   // Add Activity route
        { 
            SerializeResponse<EventActivityEntity> Response = new SerializeResponse<EventActivityEntity>();   // instance of the serialize response for event activity entity
            try
            {
                InsertLog.WriteErrrorLog("EventActivity=>EventActivityAdd=>Started" + JsonConvert.SerializeObject(eventActivity));  // erro log

                Response = EventActivity.EventActivityManage(eventActivity);    // call the method for add event activity
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("EventActivity=>AddActivity=>Exception" + ex.ToString() + ex.StackTrace);
                Response.Message = "500||Internal server error! ";
            }

            return this.Request.CreateResponse(HttpStatusCode.OK, Response);
        }

        /// <summary>
        /// Date: 2023-04-06
        /// Name: Anand Sharma
        /// in this we create the controller to get the evnet activity
        /// </summary>
        /// <param name="eventActivity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/EventActivity/GetActivity")]
        public HttpResponseMessage EventActivityGet(EventActivityEntity eventActivity)
        {
            SerializeResponse<EventActivityEntity> Response = new SerializeResponse<EventActivityEntity>(); // instance of the serialize response for event activity entity
            
            try
            {
                InsertLog.WriteErrrorLog("EventActivity=>EventActivityGet=>Started" + JsonConvert.SerializeObject(eventActivity));  // error log

                Response = EventActivity.EventActivityManage(eventActivity);     // calling the method for get event activity
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("EventActivity=>EventActivityGet=>Exception" + ex.ToString() + ex.StackTrace);
                Response.Message = "500||Internal server error! ";
            }

            return this.Request.CreateResponse(HttpStatusCode.OK, Response);
        }
    }
}
