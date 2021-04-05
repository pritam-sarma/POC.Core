using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GMS.BackEnd.APICore.POC.Controllers
{
    
    [Route("api/PushData")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PushDataController : ControllerBase
    {
        [HttpGet]    
        public string GetPushData()
        {
            string token = Request.Headers["Authorization"];
            token = token.Replace("Bearer ", "");
            if(TokenManager.ValidateToken(token))
            {
                return "Push data success";
            }
            else
            {
                Response.StatusCode = 401;
                return "Unauthorized";
            }

            
        }
    }
}

