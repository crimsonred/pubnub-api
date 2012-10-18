using System;
using PubNubLib;
using NUnit.Framework;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections;

namespace PubNubTest
{
    [TestFixture]
    public class WhenSubscribedToAChannel
    {
        [Test]
        public void ThenItShouldReturnReceivedMessage ()
        {
            string status = "";
            Pubnub pubnub = new Pubnub (
                   "demo",
                   "demo",
                   "",
                   "",
                   false);
            string channel = "hello_world";

            bool responseStatus = false;

            List<object> lstSubscribe = null;

            pubnub.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e) {
                if (e.PropertyName == "Subscribe") {
                    lstSubscribe = ((Pubnub)sender).Subscribe;

                    responseStatus = true;
                }
            };

            pubnub.subscribe (channel); 

            while (!responseStatus)
                ;

            string strResponse = "";
            Console.WriteLine (lstSubscribe);
            if (lstSubscribe.Equals (null)) {
                Assert.Fail("Null response");
            } else {
                foreach (object lst in lstSubscribe) {
                    strResponse = lst.ToString ();
                    Console.WriteLine (strResponse);
                    Assert.IsNotEmpty (strResponse);
                }
            }
        }
    }
}

