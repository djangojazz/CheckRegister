using CheckRegister.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMVCCheckRegister.Models
{
  public class WebTransaction
  {
    public WebTransaction() {}

    public WebTransaction(string userName)
    {
      UserName = userName;
    }

    [Required]
    public string UserName { get; set; }
    [Required]
    public TransactionType SelectedTransactionType { get; set; }
    [Required]
    public double Amount { get; set; }
  }
}