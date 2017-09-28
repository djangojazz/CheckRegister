using CheckRegister;
using CheckRegister.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
      DetermineUser();
      Console.WriteLine(CreateHeader($"Welcome {_user.UserName}, please enter your password to continue."));
      Login();
      DetermineAction();

      Console.ReadLine();
    }

    private static void DetermineUser()
    {
      Console.WriteLine(CreateHeader("Who are you?"));
      var userInput = Console.ReadLine();
      _user = _xmlFileLocation.GetCurrentUserIfTheyExist(userInput);
      if (_user != null) { return; }
      
      while(_user == null)
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
          _user = RegisteredUsers.Users.Single(x => x.UserName == newUserName);
        }
        else
        {
          DetermineUser();
        }
      }
    }

    private static void Login(int i = 1)
    {
      var password = Console.ReadLine();
      
      var authenticated = _user.AuthenticateUser(password);
      if (authenticated) { return; }
      
      if (i == 3)
      {
        Console.WriteLine("Failed three attempts exiting program for security (3/3)");
        Console.ReadLine();
        Environment.Exit(-1);
      }
      else { Console.WriteLine($"Your password is incorrect please try again ({i}/3)"); }

      i++;
      Login(i);
    }

    private static void DetermineAction()
    {
      Console.WriteLine(CreateHeader($"What would you like to do.", "1 = Make a Deposit  ", "2 = Make a Withdrawal", "3 = Transactions and Balance   ", "4 = Logout and Exit  "));

      var acceptable = new List<string> { "1", "2", "3", "4" };
      var input = Console.ReadLine();
      if(!acceptable.Contains(input))
      {
        Console.WriteLine("You chose an invalid option, please select either 1 Deposit, 2 Withdrawal, 3 Transactions, or 4 Logout Exit");
        Console.WriteLine();
        DetermineAction();
      }

      double valueNumber = 0;

      if (input == "1" || input == "2")
      {
        Console.WriteLine("What amount is your deposit for?");
        var inputNumber = Console.ReadLine();

        var isANumber = double.TryParse(inputNumber, out valueNumber);
        if(!isANumber)
        {
          Console.WriteLine($"{inputNumber} is not a valid number, let's try again 1 Deposit, 2 Withdrawal, 3 Balance, 4 Transactions, or 5 Logout Exit");
          DetermineAction();
        }

        TransactionType type = (input == "1") ? TransactionType.Deposit : TransactionType.Withdrawal;
        _user.AddTransaction(type, valueNumber); 

        Console.WriteLine($"Accepted {type} for {valueNumber}");
        Console.WriteLine();
        DetermineAction();
      }
      else if (input == "3")
      {
        StringBuilder sb = new StringBuilder("Your current Transactions are");
        sb.AppendLine();
        sb.AppendLine();
       
        _user.TransactionsWithTotals.ForEach(x =>
        {
          var typeText = (x.TransactionType == TransactionType.Deposit) ? $"{TransactionType.Deposit.ToString()}    " : TransactionType.Withdrawal.ToString();
          sb.AppendLine($"\tId: {x.TransactionId}\tType: {typeText}\tAmount: {x.Amount}\tCreated: {x.Created.ToString("MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)}\tRunning Total: {x.RunningTotal}");
        });
        sb.AppendLine();
        sb.AppendLine($"Your current Balance is: {_user.TransactionsWithTotals?.Last()?.RunningTotal ?? 0}");

        Console.WriteLine(sb.ToString());
        Console.WriteLine();
        DetermineAction();
      }
      else if (input == "4")
      {
        Console.WriteLine("Logging out, goodbye");
        Console.ReadLine();
        Environment.Exit(0);
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
        var length = _headerLength - (txt.Length + (_paddingLength * 2));

        if (length > 0)
        {
          var padding = length / 2;
          AppendTextForLoop(_paddingLength, _decorator, false);
          AppendTextForLoop(padding, " ", false);
          sb.Append(txt);

          if (length % 2 == 0) { AppendTextForLoop(padding, " ", false); }
          else { AppendTextForLoop(padding + 1, " ", false); }

          AppendTextForLoop(_paddingLength, _decorator, false);
        }
        else { sb.Append(txt);  }

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
