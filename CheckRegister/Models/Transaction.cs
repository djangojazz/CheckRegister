using System;
using System.Security.Principal;
using System.Xml.Serialization;

namespace CheckRegister.Models
{
  [Serializable]
  public sealed class Transaction
  {
    //Serialization needs a default constructor
    public Transaction()
    {
      Created = DateTime.Now;
      CreatedBy = WindowsIdentity.GetCurrent().Name;
    }

    public Transaction(TransactionType transactionType, double amount)
    {
      Created = DateTime.Now;
      CreatedBy = WindowsIdentity.GetCurrent().Name;
      TransactionType = transactionType;
      Amount = amount;
    }
    
    [XmlAttribute]
    public TransactionType TransactionType { get; set; }
    [XmlAttribute]
    public double Amount { get; set; }
    [XmlAttribute]
    public DateTime Created { get; set; }
    [XmlAttribute]
    public string CreatedBy { get; set; }
  }
}
