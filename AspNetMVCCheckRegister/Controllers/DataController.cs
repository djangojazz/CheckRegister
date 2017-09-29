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
    
    public List<Transaction> Get(string userName)
    {
      var user = RegisteredUsers.GetCurrentUserIfTheyExist(_xmlFileLocation, userName);
      if (user == null) { return null; }

      return user?.Transactions.ToList();
        //.Select(x => new { TransactionType = x.TransactionType, Amount = x.Amount, Created = x.Created });
    }
    
    [HttpGet, Route("api/checkRegister/exists/{userName}")]
    public bool Exists(string userName)
    {
      var user = RegisteredUsers.GetCurrentUserIfTheyExist(_xmlFileLocation, userName);
      return user != null;
    }

    [HttpGet, Route("api/checkRegister/authenticate/{userName}/{password}")]
    public bool Authenticate(string userName, string password)
    {
      var user = RegisteredUsers.GetCurrentUserIfTheyExist(_xmlFileLocation, userName);
      return user?.AuthenticateUser(password) ?? false;
    }
    
    public IHttpActionResult Post([FromBody]WebUser value)
    {
      var user = RegisteredUsers.GetCurrentUserIfTheyExist(_xmlFileLocation, value.UserName);
      if (user == null) { return BadRequest("No data exists for this user and they may not be updated"); }

      value.Transactions.ForEach(x => user.Transactions.Add(new Transaction((TransactionType)x.TransactionTypeId, x.Amount)));
      CreateFileOrAppendToIt();
      return Ok();
    }

    [HttpGet, Route("api/checkRegister/createUser/{userName}/{password}")]
    public IHttpActionResult CreateUser([FromBody]WebUser value)
    {
      var user = RegisteredUsers.GetCurrentUserIfTheyExist(_xmlFileLocation, value.UserName);
      if (user != null) { return BadRequest("User already exists could not update"); }

      RegisteredUsers.Adduser(value.UserName, value.Password);
      CreateFileOrAppendToIt();
      return Ok();
    }


    private static void CreateFileOrAppendToIt()
    {
      using (var sw = new StreamWriter(_xmlFileLocation)) { sw.Write(RegisteredUsers.Users.SerializeToXml()); }
    }
  }
}
