using AspNetMVCCheckRegister.Models;
using CheckRegister;
using CheckRegister.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace AspNetMVCCheckRegister.Controllers
{
  [Route("api/checkRegister")]
  public class DataController : ApiController
  {
    private static string _xmlFileLocation = HttpContext.Current.Server.MapPath("~/App_Data/RegisteredUsers.xml");
    
    public IHttpActionResult Get(string userName)
    {
      var user = RegisteredUsers.GetCurrentUserIfTheyExist(_xmlFileLocation, userName);

      if (user == null) { return BadRequest("No data exists for this user"); }

      return Ok(user?.Transactions.Select(x => new { TransactionType = x.TransactionType, Amount = x.Amount, Created = x.Created }));
    }
    
    [HttpGet, Route("api/checkRegister/exists/{userName}")]
    public IHttpActionResult Exists(string userName)
    {
      var user = RegisteredUsers.GetCurrentUserIfTheyExist(_xmlFileLocation, userName);
      return Ok(user != null);
    }

    [HttpGet, Route("api/checkRegister/authenticate/{userName}/{password}")]
    public IHttpActionResult Authenticate(string userName, string password)
    {
      var user = RegisteredUsers.GetCurrentUserIfTheyExist(_xmlFileLocation, userName);
      return Ok(user?.AuthenticateUser(password) ?? false);
    }

    
    public IHttpActionResult Post([FromBody]WebUser value)
    {
      var user = RegisteredUsers.GetCurrentUserIfTheyExist(_xmlFileLocation, value.UserName);
      var item = value;
      return Ok();
    }
    

    private static void CreateFileOrAppendToIt()
    {
      using (var sw = new StreamWriter(_xmlFileLocation)) { sw.Write(RegisteredUsers.Users.SerializeToXml()); }
    }
  }
}
