using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMVCCheckRegister.Models
{
  public class User
  {
    [Required]
    [Display(Name = "User name")]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Display(Name = "Remember on this computer")]
    public bool RememberMe { get; set; }

    public bool IsValid(string _username, string _password)
    {
      return true;
    }
  }
}