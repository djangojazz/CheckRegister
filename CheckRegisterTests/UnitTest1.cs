using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CheckRegisterTests
{
  /*
   * THOUGHTS ON IMPROVEMENTS
   *  PERMANENT DATA STORE = Database, Blob storage in cloud, etc.
   *  ENCRYPTION = Users should have passwords encrypted with basic encryption most likely.
   *  AMENDING AN OLDER TRANSACTION = Ability to go back when doing a reconciliation and alter an existing transaction
   *  CHECK A BALANCE ON AN EARLIER DATE = See how your trend of spending was working by seeing where you started versus where you are at now.
   *  IS THE APP HOSTED IN MULTIPLE TIMEZONES? = Store dates in UTC rather than local timezone and use client to convert
   *  CONCURRENCY = Example I created assumes a single user on a local machine.  You may want to make a hosted app or web page that many people could hit at the same time.  
   *    Potentially using a concurrency object like Concurrent Bag etc to handle if two people were using the same account at once they would not hijack the other.
   *    Depends on how large you want to get with it.  There is probably more this just went off the top of my head.  :)
   */

  [TestClass]
  public class CheckRegister
  {
    [TestMethod]
    public void TestMethod1()
    {
    }
  }
}
