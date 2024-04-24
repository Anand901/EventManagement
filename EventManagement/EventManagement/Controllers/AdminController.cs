using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using BL.UserManagemernt;
using LIBRARY;
using MODEL.Admin;
using Newtonsoft.Json;

namespace EventManagement.Controllers
{
    public class AdminController : ApiController    // inherit the base api controller
    {
        #region Instances
        AdminEntity Admin = new AdminEntity();  // instace of the admin entity
        AdminInfoBL AdminManage = new AdminInfoBL();  // instance of the admina management
        #endregion

        /// <summary>
        /// Date: 2024-04-05
        /// Name: Anand Sharma
        /// in this we create the controller to register the admin
        /// </summary>
        /// <param name="Admin"></param>
        /// <returns></returns>
        [HttpPost] 
        [Route("api/Admin/RegisterAdmin")]
        //created by anand on 2024/04/05

        public HttpResponseMessage RegisterAdmin(AdminEntity Admin)   // register admin route
        {
            SerializeResponse<AdminEntity> Response = new SerializeResponse<AdminEntity>();  // serialize response instance for admin entity
            try
            {
                InsertLog.WriteErrrorLog("EventManagement=>RegisterAdmin=>Started" + JsonConvert.SerializeObject(Admin));   // error log 

                Response = AdminManage.AdminManagement(Admin);    // call the method to register the admin
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("EventManagement=>RegisterAdmin=>Exception" + ex.ToString() + ex.StackTrace);
                Response.Message = "500||Internal server error! ";
            }

            return this.Request.CreateResponse(HttpStatusCode.OK, Response);
        }

        /// <summary>
        /// Date:2024-04-05
        /// Name: Anand Sharma
        /// in this we create the controller to log in the admin
        /// </summary>
        /// <param name="Admin"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Admin/LogInAdmin")]
        public HttpResponseMessage LogInAdmin(AdminEntity Admin)
        {
            SerializeResponse<AdminEntity> Response = new SerializeResponse<AdminEntity>();    // careate the  serialize response for admin entity
            try
            {
                InsertLog.WriteErrrorLog("EventManagement=>RegisterAdmin=>Started" + JsonConvert.SerializeObject(Admin));   // error log

                Response = AdminManage.AdminManagement(Admin);    // calling the method for admin login
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("EventManagement=>RegisterAdmin=>Exception" + ex.ToString() + ex.StackTrace);
                Response.Message = "500||Internal server error! ";
            }

            return this.Request.CreateResponse(HttpStatusCode.OK, Response);
        }

    }
}
