using System;
using PubNubLib;
using NUnit.Framework;
using System.ComponentModel;
using System.Collections.Generic;

namespace PubNubTest
{
	[TestFixture]
    public class WhenGetRequestServerTime
    {
        [Test]
        public void ThenItShouldReturnTimeStamp()
        {
            Pubnub pubnub = new Pubnub(
                "demo",
                "demo",
                "",
                "",
                false
            );
            
            pubnub.PropertyChanged += new PropertyChangedEventHandler(Pubnub_PropertyChanged);

            pubnub.time();
        }

        static void Pubnub_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Assert.AreNotEqual("0", ((Pubnub)sender).Time[0].ToString());
        }
    }
}

