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
    public User(string userName, string password, bool authenticated = false, params Transaction[] transactions)
    {
      UserName = userName;
      Password = password;
      IsAuthenticated = authenticated;
      if(transactions.Any()) { Transactions = transactions.ToList(); }
    }

    [XmlAttribute]
    public string UserName { get; set; }
    [XmlAttribute]
    public string Password { get; set; }
    [XmlIgnore]
    public bool IsAuthenticated { get; set; }
    public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    
    public void AddTransaction(TransactionType type, double amount) => Transactions.Add(new Transaction(type, amount));
    public double GetTotalOfTransactionType(TransactionType type) => (!Transactions?.Any(x => x.TransactionType == type) ?? true) ? 0 : Transactions.Where(x => x.TransactionType == type)?.Select(x => x.Amount)?.Sum() ?? 0;
    public double GetCurrentBalance() => GetTotalOfTransactionType(TransactionType.Credit) - GetTotalOfTransactionType(TransactionType.Debit);
  }
}
