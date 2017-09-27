using CheckRegister.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CheckRegister
{
  [Serializable]
  public static class RegisteredUsers
  {
    public static List<User> Users { get; set; } = new List<User>();

    public static void Adduser(string userName, string password) => Users.Add(new User(userName, password));

    public static void AuthenticateUser(this User user, string password) => user.IsAuthenticated = (user.Password == password);

    public static User GetCurrentUserIfTheyExist(this string location, string userName)
    {
      var exists = new FileInfo(location).Exists;
      if (!exists) { return null; }

      using (var sr = new StreamReader(location))
      {
        var text = sr.ReadToEnd()?.ToString();
        if (exists && (!Users?.Any() ?? true)) { Users = text.DeserializeXml<List<User>>(); }
        return Users.SingleOrDefault(x => x.UserName == userName) ?? null;
      }
    }
  }
}
