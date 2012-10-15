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

            pubnub.PropertyChanged += new PropertyChangedEventHandler(Pubnub_PropertyChanged);
            pubnub.detailedHistory(channel, 100);            
        }

        static void Pubnub_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Dictionary<string, object> _message = (Dictionary<string, object>)(((Pubnub)sender).ReturnMessage);
            if (e.PropertyName != "DetailedHistory")
            {
                Assert.IsNotNull(_message["text"]);
            }
            else
            {
                Assert.AreEqual("", _message["uuid"]);
            }
        }
    }
}

