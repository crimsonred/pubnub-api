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

            bool responseStatus = false;
            List<object> lstHistory = null;

            pubnub.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e) {

                if (e.PropertyName == "Presence")
                {
                    lstHistory = ((Pubnub)sender).Presence;

                    responseStatus = true;
                }
            };

            pubnub.presence(channel);
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
                    //Assert.IsNotEmpty(strResponse);
                }
                Assert.AreEqual(lstHistory[2], "hello_world-pnpres");
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

            bool responseStatus = false;
            List<object> lstHistory = null;

            pubnub.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e) {

                if (e.PropertyName == "Here_Now")
                {
                    lstHistory = ((Pubnub)sender).Here_Now;

                    responseStatus = true;
                }
            };

            pubnub.here_now(channel);
            while (!responseStatus) ;

            string strResponse = "";
            if (lstHistory.Equals (null)) {
                Assert.Fail("Null response");
            }
            else
            {
                //Console.WriteLine(_message["text"]);
                foreach(object lst in lstHistory)
                {
                    strResponse = lst.ToString();
                    Console.WriteLine(strResponse);
                    Assert.IsNotEmpty(strResponse);
                }
                Dictionary<string, object> message = (Dictionary<string, object>)lstHistory[0];
                foreach(KeyValuePair<String, object> entry in message)
                {
                    Console.WriteLine("value:" + entry.Value + "  " + "key:" + entry.Key);
                }

                object[] objUuid = (object[])message["uuids"];
                foreach (object obj in objUuid)
                {
                    Console.WriteLine(obj.ToString()); 
                }
                //Assert.AreNotEqual(0, message["occupancy"]);
            }

        }
    }
}

