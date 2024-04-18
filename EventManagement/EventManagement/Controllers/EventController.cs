using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Web;
using System.Web.Http;
using BL.AdminManagement;
using BL.EventMangement;
using LIBRARY;
using MODEL.Event;
using MODEL.UserEntity;
using Newtonsoft.Json;

namespace EventManagement.Controllers
{
    public class EventController : ApiController     // inherit the base api controller
    {
        EventServices eventService = new EventServices();    // instance for the event services


       /// <summary>
       /// Date: 2024-04-06
       /// Name: Anand Sharma
       /// in this route we create the  get the event controller
       /// </summary>
       /// <param name="Event"></param>
       /// <returns></returns>
        [HttpPost]
        [Route("api/Event/GetEvent")]
     
        public HttpResponseMessage ManageEventGet(EventEntity Event)
        {
            InsertLog.WriteErrrorLog("EventManagement=>GetEvent=>Started" + JsonConvert.SerializeObject(Event));
            SerializeResponse<EventEntity> Response = new SerializeResponse<EventEntity>();           // create the instace of the serialize response of the event entity
            try
            {

                #region CallingMethod
                Response = eventService.EventManage(Event);         // call the mehtod for event mangement
                #endregion

                #region ConvertBase64


                foreach (var item in Response.ArrayOfResponse)
                {

                    string ImagePath = item.EventImage;                      // gettting the image path
                    byte[] ImgaeBytes = File.ReadAllBytes(ImagePath);     // convert the byte array of the given image path
                    item.EventImage = Convert.ToBase64String(ImgaeBytes);       // convert the byte array into the base 64
                }
                #endregion



            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("EventManagement=>ManageEventGet=>Exception" + ex.ToString() + ex.StackTrace);
                Response.Message = "500||Internal server error! ";
            }

            return this.Request.CreateResponse(HttpStatusCode.OK, Response);
        }


        /// <summary>
        /// Date: 2024-04-06
        /// Name: Anand Sharma
        /// in this we create the event add controller
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Event/AddEvent")]
        //created by anand on 2024/04/06
        public HttpResponseMessage AddEvent()        // route for add the event in the event management
        {
        InsertLog.WriteErrrorLog("EventManagement=>AddEvent=>Started");   //error log of this method
            SerializeResponse<EventEntity> Response = new SerializeResponse<EventEntity>();
            EventEntity eventEntity = new EventEntity();     // instance of the event entity 

            try
            {


                #region HandlingTheFormData

                eventEntity.Flag = HttpContext.Current.Request.Params["Flag"];
                eventEntity.EventName = HttpContext.Current.Request.Params["EventName"];
                eventEntity.EventDescription = HttpContext.Current.Request.Params["EventDescription"];
                eventEntity.EventStartDate = DateTime.Parse(HttpContext.Current.Request.Params["EventStartDate"]);
                eventEntity.EventEndDate = DateTime.Parse(HttpContext.Current.Request.Params["EventEndDate"]);
                eventEntity.AdminEmail = HttpContext.Current.Request.Params["AdminEmail"];
                var image = HttpContext.Current.Request.Files[0];
                var fileName = Path.GetFileName(image.FileName);
                string Filename = Path.GetFileNameWithoutExtension(fileName.ToString());
                string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\" + Filename + "\\" + DateTime.Now.ToString().Replace(":", "-") + ".jpg";
                eventEntity.EventImage = path;
                #endregion

                #region CreatePathForImage

                if (!Directory.Exists(path.Substring(0, path.LastIndexOf('\\'))))
                {
                    Directory.CreateDirectory(path.Substring(0, path.LastIndexOf('\\')));
                }
                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                }
                #endregion

                #region SaveImageOnThePath

                Response = eventService.EventManage(eventEntity);
                if (!(Response.Code == "205" || Response.Code == "500"))
                {
                    image.SaveAs(path);   // save the image on the particular location
                }
                #endregion
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("EventManagement=>AddEvent=>Exception" + ex.ToString() + ex.StackTrace);
                Response.Message = "500||Internal server error! ";
            }

            return this.Request.CreateResponse(HttpStatusCode.OK, Response);
        }

        /// <summary>
        /// Date:2024-04-07
        /// Name: Anand Sharma
        /// in this we create the controller to get the event name
        /// </summary>
        /// <param name="Event"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Event/GetEventName")]
        public HttpResponseMessage GetEventName(EventEntity Event)
        {
            InsertLog.WriteErrrorLog("EventManagement=>GetEventName=>Started" + JsonConvert.SerializeObject(Event));   // error log
            SerializeResponse<EventEntity> Response = new SerializeResponse<EventEntity>();   // serialaize response of event entity
            try
            {
                #region CallingMethod
                Response = eventService.EventManage(Event);
                #endregion

            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("EventManagement=>GetEventName=>Exception" + ex.ToString() + ex.StackTrace);   // error log of any exception
                Response.Message = "500||Internal server error! ";
            }

            return this.Request.CreateResponse(HttpStatusCode.OK, Response);
        }

        /// <summary>
        /// Date:2024-04-07
        /// Name: Anand Sharma
        /// in this we create the controller to get the event date
        /// </summary>
        /// <param name="Event"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Event/GetEventDate")]
        public HttpResponseMessage GetEventDate(EventEntity Event)
        {
            InsertLog.WriteErrrorLog("EventManagement=>GetEventName=>Started" + JsonConvert.SerializeObject(Event));
            SerializeResponse<EventEntity> Response = new SerializeResponse<EventEntity>();   // serialaize response of event entity

            try
            {
                #region CallingMethod

                Response = eventService.EventManage(Event);
                #endregion

            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("EventManagement=>GetEventDate=>Exception" + ex.ToString() + ex.StackTrace);  // erroe log for any exception
                Response.Message = "500||Internal server error! ";
            }

            return this.Request.CreateResponse(HttpStatusCode.OK, Response);
        }

        /// <summary>
        /// Date:2024-04-07
        /// Name: Anand Sharma
        /// in this we create the controller to publish the event
        /// </summary>
        /// <param name="Event"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Event/PublishEvent")]
        public HttpResponseMessage PublishEvent(EventEntity Event)
        {
            InsertLog.WriteErrrorLog("EventManagement=>PublishEvent=>Started" + JsonConvert.SerializeObject(Event));
            SerializeResponse<EventEntity> Response = new SerializeResponse<EventEntity>();   // serialaize response of event entity

            try
            {
                #region CallingMethod

                Response = eventService.EventManage(Event);
                #endregion

            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("EventManagement=>PublishEvent=>Exception" + ex.ToString() + ex.StackTrace);
                Response.Message = "500||Internal server error! ";
            }

            return this.Request.CreateResponse(HttpStatusCode.OK, Response);
        }


    }
}
