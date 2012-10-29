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

            Common.deliveryStatus = false;

            pubnub.publish(channel, message, Common.DisplayReturnMessage);
            //wait till the response is received from the server
            while (!Common.deliveryStatus) ;
            IList<object> fields = Common.objResponse as IList<object>;
            string strSent = fields[1].ToString();
            string strOne = fields[0].ToString();
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

            Common.deliveryStatus = false;

            pubnub.publish(channel, message, Common.DisplayReturnMessage);
            //wait till the response is received from the server
            while (!Common.deliveryStatus) ;
            IList<object> fields = Common.objResponse as IList<object>;
            string strSent = fields[1].ToString();
            string strOne = fields[0].ToString();
            Assert.AreEqual("Sent", strSent);
            Assert.AreEqual("1", strOne);
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

            //pubnub.PropertyChanged += new PropertyChangedEventHandler(Pubnub_PropertyChanged);

            pubnub.PUBLISH_KEY = "demo";
            Assert.AreEqual(true, pubnub.publish(channel, message, Common.DisplayReturnMessage));

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

            //pubnub.PropertyChanged += new PropertyChangedEventHandler(Pubnub_PropertyChanged);

            Assert.AreEqual(false, pubnub.publish(channel, message, Common.DisplayReturnMessage));
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

            //pubnub.PropertyChanged += new PropertyChangedEventHandler(Pubnub_PropertyChanged);

            Assert.AreEqual(true, pubnub.publish(channel, message, Common.DisplayReturnMessage));

            pubnub.SECRET_KEY = "key";
            Assert.AreEqual(true, pubnub.publish(channel, message, Common.DisplayReturnMessage));
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

            //pubnub.PropertyChanged += new PropertyChangedEventHandler(Pubnub_PropertyChanged);

            Assert.AreEqual(true, pubnub.publish(channel, message, Common.DisplayReturnMessage));
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

            //pubnub.PropertyChanged += new PropertyChangedEventHandler(Pubnub_PropertyChanged);

            Assert.AreEqual(false, pubnub.publish(channel, message, Common.DisplayReturnMessage));
        }
    }
}

