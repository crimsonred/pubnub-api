using System;
using PubNubLib;
using NUnit.Framework;
using System.ComponentModel;
using System.Collections.Generic;

namespace PubNubTest
{
    [TestFixture]
    public class WhenAMessageIsPublished
    {
        
        public void NullMessage()
        {
            Pubnub pubnub = new Pubnub(
                "demo",
                "demo",
                "",
                "",
                false
            );
            string channel = "hello_world";
            string message = null;

            bool deliveryStatus = false;

            string strSent = "";
            string strOne = "";
            //Add a PropertyChanged event handler 
            pubnub.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Publish")
                {
                    strSent = ((Pubnub)sender).Publish[1].ToString();
                    strOne = ((Pubnub)sender).Publish[0].ToString();
                    Console.WriteLine(strSent);
                    Console.WriteLine(strOne);
                    deliveryStatus = true;
                }
            };

            pubnub.publish(channel, message);
            //wait till the response is received from the server
            while (!deliveryStatus) ;
            Assert.AreEqual("Sent", strSent);
            Assert.AreEqual("1", strOne);
        }
        [Test]
        public void ThenItShouldReturnSuccessCodeAndInfo()
        {
            Pubnub pubnub = new Pubnub(
                "demo",
                "demo",
                "",
                "",
                false
            );
            string channel = "hello_world";
            string message = "Pubnub API Usage Example";

            bool deliveryStatus = false;

            string strSent = "";
            string strOne = "";
            //Add a PropertyChanged event handler 
            pubnub.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Publish")
                {
                    strSent = ((Pubnub)sender).Publish[1].ToString();
                    strOne = ((Pubnub)sender).Publish[0].ToString();
                    Console.WriteLine(strSent);
                    Console.WriteLine(strOne);
                    deliveryStatus = true;
                }
            };

            pubnub.publish(channel, message);
            //wait till the response is received from the server
            while (!deliveryStatus) ;
            Assert.AreEqual("Sent", strSent);
            Assert.AreEqual("1", strOne);
        }

        static void Pubnub_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Assert.AreEqual("1", ((Pubnub)sender).Publish[0].ToString());
            Assert.AreEqual("Sent", ((Pubnub)sender).Publish[0].ToString());
        }

        [Test]
        public void ThenItShouldGenerateUniqueIdentifier()
        {
            Pubnub pubnub = new Pubnub(
                "demo",
                "demo",
                "",
                "",
                false
            );

            Assert.IsNotNull(pubnub.generateGUID());
        }

        [Test]
        public void ThenPublishKeyShouldBeOverriden()
        {
            Pubnub pubnub = new Pubnub(
                "",
                "demo",
                "",
                "",
                false
            );
            string channel = "my/channel";
            string message = "Pubnub API Usage Example";

            pubnub.PropertyChanged += new PropertyChangedEventHandler(Pubnub_PropertyChanged);

            pubnub.PUBLISH_KEY = "demo";
            Assert.AreEqual(true, pubnub.publish(channel, message));

        }

        [Test]
        public void ThenPublishKeyShouldNotBeEmptyAfterOverriden()
        {
            Pubnub pubnub = new Pubnub(
                "",
                "demo",
                "",
                "",
                false
            );
            string channel = "my/channel";
            string message = "Pubnub API Usage Example";

            pubnub.PropertyChanged += new PropertyChangedEventHandler(Pubnub_PropertyChanged);

            Assert.AreEqual(false, pubnub.publish(channel, message));
        }

        [Test]
        public void ThenSecretKeyShouldBeProvidedOptionally()
        {
            Pubnub pubnub = new Pubnub(
                "demo",
                "demo"
            );
            string channel = "my/channel";
            string message = "Pubnub API Usage Example";

            pubnub.PropertyChanged += new PropertyChangedEventHandler(Pubnub_PropertyChanged);

            Assert.AreEqual(true, pubnub.publish(channel, message));

            pubnub.SECRET_KEY = "key";
            Assert.AreEqual(true, pubnub.publish(channel, message));
        }

        [Test]
        public void IfSSLNotProvidedThenDefaultShouldBeFalse()
        {
            Pubnub pubnub = new Pubnub(
                "demo",
                "demo",
                ""
            );
            string channel = "hello_world";
            string message = "Pubnub API Usage Example";

            pubnub.PropertyChanged += new PropertyChangedEventHandler(Pubnub_PropertyChanged);

            Assert.AreEqual(true, pubnub.publish(channel, message));
        }

        [Test]
        public void NullShouldBeTreatedAsEmpty()
        {
            Pubnub pubnub = new Pubnub(
                null,
                "demo",
                null,
                null,
                false
            );
            string channel = "my/channel";
            string message = "Pubnub API Usage Example";

            pubnub.PropertyChanged += new PropertyChangedEventHandler(Pubnub_PropertyChanged);

            Assert.AreEqual(false, pubnub.publish(channel, message));
        }

    }
}

