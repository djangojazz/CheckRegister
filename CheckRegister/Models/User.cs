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
    public User(string userName, string password)
    {
      UserName = userName;
      Password = password;
    }

    [XmlAttribute]
    public string UserName { get; set; }
    [XmlAttribute]
    public string Password { get; set; }
    //[XmlIgnore]
    //public bool IsAuthenticated { get; set; }
    
    
    public List<Transaction> Transactions { get; set; } = new List<Transaction>();

    [XmlIgnore]
    public List<Transaction> TransactionsWithTotals
    {
      get
      {
        double runningTotal = 0;
        return Transactions
          .Select((x, i) => new Transaction(x, i+1, runningTotal += (x.TransactionType == TransactionType.Deposit) ? x.Amount : -x.Amount))
          .OrderBy(x => x.TransactionId)
          .ToList();
      }
    }

    public void AddTransaction(TransactionType type, double amount) => Transactions.Add(new Transaction(type, amount));
    public bool AuthenticateUser(string password) => (Password == password);
  }
}
