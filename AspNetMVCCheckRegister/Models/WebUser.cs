using CheckRegister;
using CheckRegister.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMVCCheckRegister.Models
{
  public class WebUser
  {
    public WebUser() {}

    public WebUser(string username, string password)
    {
      UserName = username;
      Password = password;
    }

    [Required, Display(Name = "User name")]
    public string UserName { get; set; }

    [Required, DataType(DataType.Password), Display(Name = "Password")]
    public string Password { get; set; }
    public bool Authenticated { get; set; }
    public WebTransaction TransactionRequest { get; set; }
    public List<Transaction> Transactions { get; set; } = new List<Transaction>();
  }
}