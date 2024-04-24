using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Internal;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Web.Http;
using BL.AdminManagement;
using BL.UserManagemernt;
using LIBRARY;
using Microsoft.IdentityModel.Tokens;
using MODEL.Admin;
using MODEL.UserEntity;
using Newtonsoft.Json;

namespace EventManagement.Controllers
{
    public class UserController : ApiController
    {

        #region Inatance
        UserInfoBL UserManage = new UserInfoBL();
        #endregion

        /// <summary>
        /// Date:2024-04-05
        /// Name: Anand sharma
        /// In this we create controller to register the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/User/RegisterUser")]
        public HttpResponseMessage RegisterUser(UserEntity user)
        {
            SerializeResponse<UserEntity> Response = new SerializeResponse<UserEntity>();
            try
            {
                InsertLog.WriteErrrorLog("EventManagement=>RegisterUser=>Started" + JsonConvert.SerializeObject(user));

                Response = UserManage.UserManagement(user);
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("EventManagement=>RegisterUser=>Exception" + ex.ToString() + ex.StackTrace);
                Response.Message = "500||Internal server error! ";
            }

            return this.Request.CreateResponse(HttpStatusCode.OK, Response);
        }

        /// <summary>
        /// Date:2024-04-05
        /// name: Anand Sharma
        /// in this we createt the controller to log in the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/User/LogInUser")]
        public HttpResponseMessage LogInUser(UserEntity user)
        {
            SerializeResponse<UserEntity> Response = new SerializeResponse<UserEntity>();
            try
            {
                InsertLog.WriteErrrorLog("EventManagement=>RegisterUser=>Started" + JsonConvert.SerializeObject(user));

                Response = UserManage.UserManagement(user);
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("EventManagement=>RegisterUser=>Exception" + ex.ToString() + ex.StackTrace);
                Response.Message = "500||Internal server error! ";
            }

            return this.Request.CreateResponse(HttpStatusCode.OK, Response);
        }


       
    }
}
