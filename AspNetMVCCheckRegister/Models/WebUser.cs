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
    private const string _xmlFileLocation = "RegisteredUsers.xml";
    private User _user;

    public WebUser() {}

    public WebUser(string username, string password)
    {
      UserName = username;
      Password = password;
      _user = RegisteredUsers.Users.Single(x => x.UserName == username);
    }

    [Required, Display(Name = "User name")]
    public string UserName { get; set; }

    [Required, DataType(DataType.Password), Display(Name = "Password")]
    public string Password { get; set; }
    public bool Authenticated { get =>  RegisteredUsers.Users.SingleOrDefault(x => x.UserName == UserName)?.AuthenticateUser(Password) ?? false; }
    public bool Exists { get => RegisteredUsers.Users.Exists(x => x.UserName == UserName); }
    public List<Transaction> Transactions { get => _user?.Transactions; }

    //private bool IsValid(string username, string password) => Authenticated = RegisteredUsers.Users.SingleOrDefault(x => x.UserName == username)?.AuthenticateUser(password) ?? false;
  }
}