using System;
using PubNubLib;
using NUnit.Framework;
using System.ComponentModel;
using System.Collections.Generic;

namespace PubNubTest
{
    [TestFixture]
    public class WhenDetailedHistoryIsRequested
    {
        [Test]
        public void ItShouldReturnDetailedHistory()
        {
            Pubnub pubnub = new Pubnub(
                "demo",
                "demo",
                "",
                "",
                false
            );
            string channel = "hello_world";

            //pubnub.PropertyChanged += new PropertyChangedEventHandler(Pubnub_PropertyChanged);

            //publish a test message. 
            pubnub.publish (channel, "Test message");
            bool responseStatus = false;

            object objHistory = null;

            pubnub.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e) {

                if (e.PropertyName == "DetailedHistory")
                {
                    objHistory = ((Pubnub)sender).DetailedHistory;

                    responseStatus = true;
                }
            };

            pubnub.detailedHistory(channel, 1);

            while (!responseStatus) ;

            string strResponse = "";
            if (objHistory.Equals (null)) {
                Assert.Fail("Null response");
            }
            else
            {
                List<object> fields = (List<object>)objHistory;

                foreach(object lst in fields)
                {
                    strResponse = lst.ToString();
                    Console.WriteLine(strResponse);
                    Assert.IsNotEmpty(strResponse);
                }
            }          
        }
    }
}

