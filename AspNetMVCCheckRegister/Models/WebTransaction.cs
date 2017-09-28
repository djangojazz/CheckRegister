using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMVCCheckRegister.Models
{
  public class WebTransaction
  {
    [Required]
    public int TransactionTypeId { get; set; }
    [Required]
    public double Amount { get; set; }
  }
}