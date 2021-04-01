using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GMS.BackEnd.APICore.POC.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GMS.BackEnd.APICore.POC.Controllers
{   
    [ApiController]
    [Route("api/Values")]
    public class ValuesController : ControllerBase
    {
        private IConfiguration Configuration { get; set; }

        public ValuesController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpPost]
        [Route("Login")]
        public ResponseVM Login(LoginModel loginModel)
        {
            ResponseVM response = new ResponseVM();

            var isValid = AuthUser(loginModel.UserName, loginModel.Password);
       
            if (!isValid) 
            {               
                response.Status = "Invalid";
                response.Message = "Invalid User.";              
                return response;
            }                 
            else return new ResponseVM
            {
                Status = "Success",
                Message = TokenManager.GenerateToken(loginModel.UserName)
            };
            
        }

        public string ValidateClientTokens(string token)
        {
            return TokenManager.ValidateToken(token);
        }

        public bool AuthUser(string userName, string password)
        {
            string strcon = Configuration.GetConnectionString("DefaultConnection");
            //SqlDataReader dr = null;
            string _userName = null;
            using (SqlConnection connection = new SqlConnection(strcon))
            {
             
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = $"select UserName from tblUser where UserName = '{userName}' and Password ='{password}' ";
                var dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    _userName = dr["UserName"].ToString();
                }
                dr.Close();
                return _userName == userName;
            }          
        }

    }
}
