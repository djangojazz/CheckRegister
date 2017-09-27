using CheckRegister.Models;
using System;
using System.Collections.Generic;

namespace CheckRegister
{
  [Serializable]
  public static class RegisteredUsers
  {
    public static List<User> Users { get; set; }
  }
}
