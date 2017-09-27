using CheckRegister;
using CheckRegister.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineCheckRegister
{
  class Program
  {
    private const string _xmlFileLocation = "RegisteredUsers.xml";
    private const int _headerLength = 64;
    private const string _decorator = "-";
    private const int _paddingLength = 4;
    private static User _user;

    static void Main(string[] args)
    {
      Console.WriteLine(CreateHeader("Who are you?"));
      _user = DetermineUser();
      Console.WriteLine(CreateHeader($"Welcome {_user.UserName}, please login to continue."));
      Login();
      Console.WriteLine(CreateHeader($"What would like to do.", "\t1. Make a Deposit", "\t2. Make a Withdrawal", "\t3. Check your balance", "\t4. See transactions"));
      DetermineAction();

      Console.ReadLine();
    }

    private static User DetermineUser()
    {
      var userInput = Console.ReadLine();
      var user = _xmlFileLocation.GetCurrentUserIfTheyExist(userInput);
      
      if(user == null)
      {
        Console.WriteLine("You are currently not in the system would you like to be added? (Y/N)");
        var response = Console.ReadLine();

        if(response?.Length > 0 && response?.Substring(0, 1).ToLower() == "y") 
        {
          Console.WriteLine("What would you like your username to be?");
          var newUserName = Console.ReadLine();
          Console.WriteLine("What would you like your password to be?");
          var newPasword = Console.ReadLine();
          RegisteredUsers.Adduser(newUserName, newPasword);

          CreateFileOrAppendToIt();
          return RegisteredUsers.Users.Single(x => x.UserName == newUserName);
        }
        else { DetermineUser(); }
      }

      return user;
    }

    private static void Login(int i = 0)
    {
      var password = Console.ReadLine();
      while(!_user.AuthenticateUser(password) && i <= 2)
      {
        if(i == 2)
        {
          Console.WriteLine("Failed three attempts exiting program for security");
          Console.ReadLine();
          Environment.Exit(-1);
        }

        Console.WriteLine($"Your password is incorrect please try again ({i}/3)");
        i++;
        Login(i);
      }
    }

    private static void DetermineAction()
    {
      var acceptable = new List<string> { "1", "2", "3", "4" };
      var input = Console.ReadLine();
      if(!acceptable.Contains(input))
      {
        Console.WriteLine("You chose an invalid option, please select either 1: Deposit, 2: Withdrawal, 3. Balance, or 4. Transactions");
        DetermineAction();
      }
      

    }

    private static string CreateHeader(string text, params string[] extraText)
    {
      StringBuilder sb = new StringBuilder();
      Action<int, string, bool> AppendTextForLoop = (l, t, nl) => 
      {
        if (!nl) { for (int i = 0; i < l; i++) { sb.Append(t); } }
        else { for (int i = 0; i <= l; i++) { sb.Append((i == _headerLength) ? Environment.NewLine : _decorator); } }
      };
      
      Action<string> AddLineWithLength = (txt) =>
      {
        var length = _headerLength - (text.Length + (_paddingLength * 2));

        if (length > 0)
        {
          var padding = length / 2;
          AppendTextForLoop(_paddingLength, _decorator, false);
          AppendTextForLoop(padding, " ", false);
          sb.Append(text);

          if (length % 2 == 0) { AppendTextForLoop(padding, " ", false); }
          else { AppendTextForLoop(padding + 1, " ", false); }

          AppendTextForLoop(_paddingLength, _decorator, false);
        }
        else { sb.Append(text);  }

        sb.Append(Environment.NewLine);
      };

      AppendTextForLoop(_headerLength, _decorator, true);
      AddLineWithLength(text);
      foreach(var t in extraText)
      {
        AddLineWithLength(t);
      }
      AppendTextForLoop(_headerLength, _decorator, true);

      return sb.ToString();
    }
    
    private static void CreateFileOrAppendToIt() 
    {
      using (var sw = new StreamWriter(_xmlFileLocation)) { sw.Write(RegisteredUsers.Users.SerializeToXml()); }
    }
  }
}
