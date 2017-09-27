using Microsoft.VisualStudio.TestTools.UnitTesting;
using CheckRegister.Models;
using CheckRegister;
using System.Collections.Generic;
using System.IO;

namespace CheckRegisterTests
{
  /*
   * THOUGHTS ON IMPROVEMENTS
   *  PERMANENT DATA STORE = Database, Blob storage in cloud, etc.
   *  ENCRYPTION = Users should have passwords encrypted with basic encryption most likely.
   *  AMENDING AN OLDER TRANSACTION = Ability to go back when doing a reconciliation and alter an existing transaction
   *  CHECK A BALANCE ON AN EARLIER DATE = See how your trend of spending was working by seeing where you started versus where you are at now.
   *  MAYBE NOT USE STATIC CLASSES = For mockability you would probably interface more things and use MOQ or similar for unit test mocks.
   *  IS THE APP HOSTED IN MULTIPLE TIMEZONES? = Store dates in UTC rather than local timezone and use client to convert
   *  CONCURRENCY = Example I created assumes a single user on a local machine.  You may want to make a hosted app or web page that many people could hit at the same time.  
   *    Potentially using a concurrency object like Concurrent Bag etc to handle if two people were using the same account at once they would not hijack the other.
   *    Depends on how large you want to get with it.  There is probably more this just went off the top of my head.  :)
   */

  [TestClass]
  public class CheckRegister
  {
    private User _user;
    private User _user2;
    private const string _xmlFileLocation = "RegisteredUsers.xml";

    [TestInitialize]
    public void TestInitialize()
    {
      _user = new User("Test", "password", new Transaction(TransactionType.Credit, 100), new Transaction(TransactionType.Debit, 50));
      _user2 = new User("Test2", "password2", new Transaction(TransactionType.Credit, 200), new Transaction(TransactionType.Debit, 100));
    }

    [TestMethod]
    public void BeAbleToSerializeAndDeserializeCollectionToAndFromDisk()
    {
      //Assign 
      RegisteredUsers.Users = new List<User> { _user, _user2 };

      //Act
      var xml = RegisteredUsers.Users.SerializeToXml();
      using (var sw = new StreamWriter(_xmlFileLocation))
      {
        sw.Write(xml);
      }

      bool exists = false;
      using (var sr = new StreamReader(_xmlFileLocation))
      {
        exists = !string.IsNullOrEmpty(sr.ReadToEnd());
      }
      
      //Assert
      Assert.IsTrue(exists, "Expected valid xml");
    }
  }
}
