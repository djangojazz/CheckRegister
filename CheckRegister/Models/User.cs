using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CheckRegister.Models
{
  [Serializable]
  public sealed class User
  {
    [XmlAttribute]
    public string UserName { get; set; }
    [XmlAttribute]
    public string Password { get; set; }
    public List<Transaction> Transactions { get; set; }
  }
}
