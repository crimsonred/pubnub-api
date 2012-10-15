using System;
using PubNubLib;
using NUnit.Framework;
using System.ComponentModel;
using System.Collections.Generic;

namespace PubNubTest
{
	[TestFixture]
    public class WhenAClientIsPresented
    {
        [Test]
        public void ThenItShouldReturnReceivedMessage()
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

            pubnub.presence(channel);
        }

        static void Pubnub_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Dictionary<string, object> _message = (Dictionary<string, object>)(((Pubnub)sender).ReturnMessage);
            if (e.PropertyName != "Here_Now")
            {
				Console.WriteLine(_message["text"]);
                Assert.IsNotNull(_message["text"]);
            }
            else
            {
				Console.WriteLine(_message["uuid"]);
                Assert.AreEqual("NN", _message["uuid"]);
            }
        }

        [Test]
        public void IfHereNowIsCalledThenItShouldReturnInfo()
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

            pubnub.here_now(channel);
        }
    }
}

