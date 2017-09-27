using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace CheckRegister.Models
{
  [Serializable]
  public sealed class User
  {
    public User() {}
    public User(string userName, string password, params Transaction[] transactions)
    {
      UserName = userName;
      Password = password;
      if(transactions.Any()) { Transactions = transactions.ToList(); }
    }

    [XmlAttribute]
    public string UserName { get; set; }
    [XmlAttribute]
    public string Password { get; set; }
    public List<Transaction> Transactions { get; set; }
  }
}
