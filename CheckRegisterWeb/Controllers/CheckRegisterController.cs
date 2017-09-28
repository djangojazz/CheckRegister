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

namespace AspNetMVCCheckRegister.Controllers
{
  [Route("api/checkRegister")]
  public class CheckRegisterController : ApiController
  {
    private static string _xmlFileLocation = HttpContext.Current.Server.MapPath("~/App_Data/RegisteredUsers.xml");
    private User _user;

    // GET api/values/5
    public List<Transaction> Get(string userName)
    {
      RegisteredUsers.Adduser("b", "b");
      CreateFileOrAppendToIt();

      _user = RegisteredUsers.Users.SingleOrDefault(x => x.UserName == "b");
      _user.AddTransaction(TransactionType.Deposit, 1000);
      _user.AddTransaction(TransactionType.Withdrawal, 222);
      CreateFileOrAppendToIt();

      _user = RegisteredUsers.GetCurrentUserIfTheyExist(@"C:\Test\RegisteredUsers.xml", "b");
      return new List<Transaction>();
    }

    // POST api/values
    public void Post([FromBody]string value)
    {
    }

    // PUT api/values/5
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/values/5
    public void Delete(int id)
    {
    }

    private static void CreateFileOrAppendToIt()
    {
      using (var sw = new StreamWriter(_xmlFileLocation)) { sw.Write(RegisteredUsers.Users.SerializeToXml()); }
    }
  }
}
