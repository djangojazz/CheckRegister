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


    static void Main(string[] args)
    {
      DetermineUser();

      Console.ReadLine();
    }

    private static void DetermineUser()
    {
      Console.WriteLine(CreateHeader("What is your name?"));
      var user = Console.ReadLine();
      Console.WriteLine($"Hello {user}");
    }

    private static string CreateHeader(string text)
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
      AppendTextForLoop(_headerLength, _decorator, true);

      return sb.ToString();
    }
    
    private static void CreateFileOrAppendToIt()
    {
      var xml = RegisteredUsers.Users.SerializeToXml();
      using (var sw = new StreamWriter(_xmlFileLocation)) { sw.Write(xml); }
    }

    //private static User GetCurrentUserIfTheyExist()
    //{

    //}
  }
}
