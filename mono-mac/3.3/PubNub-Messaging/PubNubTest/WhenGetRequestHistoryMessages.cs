using System;
using PubNubLib;
using NUnit.Framework;
using System.ComponentModel;
using System.Collections.Generic;

namespace PubNubTest
{
    [TestFixture]
    public class WhenGetRequestHistoryMessages
    {

        [Test]
        public void ThenItShouldReturnHistoryMessages ()
        {
            Pubnub pubnub = new Pubnub (
                "demo",
                "demo",
                "",
                "",
                false
            );
            string channel = "hello_world";
            bool responseStatus = false;
            //publish a test message. 
            pubnub.publish (channel, "Test message");

            List<object> lstHistory = null;

            pubnub.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e) {
                if (e.PropertyName == "History")
                {
                    lstHistory = ((Pubnub)sender).History;

                    responseStatus = true;
                }
            };

            pubnub.history(channel, 1);

            while (!responseStatus) ;

            string strResponse = "";
            if (lstHistory.Equals (null)) {
                Assert.Fail("Null response");
            }
            else
            {
                foreach(object lst in lstHistory)
                {
                    strResponse = lst.ToString();
                    Console.WriteLine(strResponse);
                    Assert.IsNotEmpty(strResponse);
                }
            }
        }
    }
}

