using System;
using PubNubLib;
using NUnit.Framework;
using System.ComponentModel;
using System.Collections.Generic;

namespace PubNubTest
{
	[TestFixture]
    public class WhenSubscribedToAChannel
    {
        [Test]
        public void ThenItShouldReturnReceivedMessage()
        {
            string status = "";
            Pubnub pubnub = new Pubnub(
                   "demo",
                   "demo",
                   "",
                   "",
                   false);
            string channel = "my/channel";

            pubnub.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "ReturnMessage")
                {
                    Dictionary<string, object> _message = (Dictionary<string, object>)(((Pubnub)sender).ReturnMessage);
                    Console.WriteLine("Received Message -> '" + _message["text"] + "'");
                    status = _message["text"].ToString();
                    Assert.AreEqual("assert", status);
                }
            };
            pubnub.subscribe(channel);           
            
        }
    }
}

