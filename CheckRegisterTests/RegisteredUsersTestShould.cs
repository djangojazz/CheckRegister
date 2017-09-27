using Microsoft.VisualStudio.TestTools.UnitTesting;
using CheckRegister.Models;
using CheckRegister;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CheckRegisterTests
{
  /*
   * THOUGHTS ON IMPROVEMENTS BUT WOULD TAKE MORE CODING
   *  PERMANENT DATA STORE = Database, Blob storage in cloud, etc.
   *  ENCRYPTION = Users should have passwords encrypted with basic encryption most likely.
   *  SECURITY = Multiple Users could see other users, a database and pattern would fix this.
   *  DESTRUCTORS AND DISPOSING = Probably overkill for an example but when things get larger and resources may become unmanaged with third parties could be an issue.
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
      _user = new User("Test", "password", true, new Transaction(TransactionType.Credit, 100), new Transaction(TransactionType.Debit, 50));
      _user2 = new User("Test2", "password2", false, new Transaction(TransactionType.Credit, 200), new Transaction(TransactionType.Debit, 100));
    }

    [TestMethod]
    public void BeAbleToSerializeAndDeserializeCollectionToAndFromDiskAndCheckUsers()
    {
      //Assign 
      RegisteredUsers.Users = new List<User> { _user, _user2 };

      //Act
      var xml = RegisteredUsers.Users.SerializeToXml();
      RegisteredUsers.Users = null;
      var usersAfterFileSave = RegisteredUsers.Users;

      using (var sw = new StreamWriter(_xmlFileLocation)) { sw.Write(xml); }

      var exists = new FileInfo(_xmlFileLocation).Exists;
      Assert.IsTrue(exists, "Expected valid xml");

      var user1 = _xmlFileLocation.GetCurrentUserIfTheyExist("Test");
      user1.AuthenticateUser("password");
      var user2 = _xmlFileLocation.GetCurrentUserIfTheyExist("Testt");
      File.Delete(_xmlFileLocation);

      //Assert
      Assert.IsNull(usersAfterFileSave, "Expected to be null");
      Assert.IsNotNull(user1, "Expected to be not null");
      Assert.IsNull(user2, "Expected to be null");
      Assert.AreEqual(2, RegisteredUsers.Users.Count, "Should be two users");
      Assert.AreEqual(true, RegisteredUsers.Users[0].IsAuthenticated, "First User should be authenticated");
      Assert.AreEqual(false, RegisteredUsers.Users[1].IsAuthenticated, "Second User should be unAuthenticated");
    }
    
    [TestMethod]
    public void BeAbleToAddNewEntriesAndDetermineBalance()
    {
      //Assign
      _user.AddTransaction(TransactionType.Debit, 20);

      //Act
      var balance1 = _user.GetCurrentBalance();
      _user.AddTransaction(TransactionType.Debit, 10);
      var balance2 = _user.GetCurrentBalance();

      //Assert
      Assert.AreEqual(30, balance1, "Balance Should be 30");
      Assert.AreEqual(20, balance2, "Balance Should be 20");
    }
  }
}
