using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
//added ra
using System.Security.Cryptography;
using System.IO;

namespace PubNubMessaging
{
    public class Pubnub_Example
    {
        static public bool deliveryStatus = false;
        static public Pubnub pubnub = new Pubnub(
                    "demo",
                    "demo",
                    "",
                    "",
                    false);
        static public string channel = "hello_world";
        //static public string message = "Pubnub API Usage Example - Publish";
        static public string message = "Pubnub API Usage Example - Publish";

        /*static public void Main()
        {

            //Console.WriteLine("\nRunning publish()");
            //Publish_Example();

            //Console.WriteLine("\nRunning detailedHistory()");
            //DetailedHistory_Example();

            //Console.WriteLine("\nRunning detailedHistory()");
            //DetailedHistory_Decrypted_Example();

            //Console.WriteLine("\nRunning timestamp()");
            //Timestamp_Example();

            //Console.WriteLine("\nRunning here_now()");
            //HereNow_Example();

            //Console.WriteLine("\nRunning presence()");
            //Presence_Example();

            //Console.WriteLine("\nRunning timestamp()");
            //Subscribe_Example();

            //Console.WriteLine("\nRunning history()");
            //History_Example();

            //Console.WriteLine("\nRunning TestUnencryptedHistory()");
            //TestUnencryptedHistory();

            //Console.WriteLine("\nRunning TestEncryptedHistory()");
            //TestEncryptedHistory();

            //Console.WriteLine("\nRunning TestUnencryptedDetailedHistory()");
            //TestUnencryptedDetailedHistory();
            
            //Console.WriteLine("\nRunning TestEncryptedDetailedHistory()");
            //TestEncryptedDetailedHistory();

            //Console.WriteLine("\nRunning TestUnencryptedDetailedHistoryParams()");
            //TestUnencryptedDetailedHistoryParams();

            Console.WriteLine("\nRunning TestEncryptedDetailedHistoryParams()");
            TestEncryptedDetailedHistoryParams();

            Console.WriteLine("\nPress any key to exit when done with demo.\n\n");
            Console.ReadLine();

        }*/
        public static void TestUnencryptedHistory()
        {
            pubnub.CIPHER_KEY = "";
            pubnub.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Publish")
                {
                    Console.WriteLine("\n*********** Publish Messages *********** ");
                    Console.WriteLine(
                        "Publish Success: " + ((Pubnub)sender).Publish[0].ToString() +
                        "\nPublish Info: " + ((Pubnub)sender).Publish[1].ToString()
                        );
                }

                if (e.PropertyName == "History")
                {
                    Console.WriteLine("\n*********** History Messages *********** ");
                    MessageFeeder(((Pubnub)sender).History);
                }
            };
            pubnub.publish(channel, message);
            pubnub.history(channel, 1);
        }

        public static void TestEncryptedHistory()
        {
            pubnub.CIPHER_KEY = "enigma";
            pubnub.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Publish")
                {
                    Console.WriteLine("\n*********** Publish Messages *********** ");
                    Console.WriteLine(
                        "Publish Success: " + ((Pubnub)sender).Publish[0].ToString() +
                        "\nPublish Info: " + ((Pubnub)sender).Publish[1].ToString()
                        );
                }

                if (e.PropertyName == "History")
                {
                    Console.WriteLine("\n*********** History Messages *********** ");
                    MessageFeeder(((Pubnub)sender).History);
                }
            };
            pubnub.publish(channel, message);
            pubnub.history(channel, 1);
        }

        public static void TestUnencryptedDetailedHistory()
        {
            // Context setup for Detailed History
            pubnub.CIPHER_KEY = "";
            int total_msg = 10;
            long starttime = Timestamp();
            Dictionary<long, string> inputs = new Dictionary<long,string>();
            for (int i = 0; i < total_msg / 2; i++)
            {
                string msg = i.ToString();
                pubnub.publish(channel, msg);
                long t = Timestamp();
                inputs.Add(t, msg);
                Console.WriteLine("Message # " + i.ToString() + " published");
            }

            long midtime = Timestamp();
            for (int i = total_msg / 2; i < total_msg; i++)
            {
                string msg = i.ToString();
                pubnub.publish(channel, msg);
                long t = Timestamp();
                inputs.Add(t, msg);
                Console.WriteLine("Message # " + i.ToString() + " published");
            }

            long endtime = Timestamp();

            deliveryStatus = false;
            pubnub.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "DetailedHistory")
                {
                    Console.WriteLine("\n*********** DetailedHistory Messages *********** ");
					//modified object[] to List<object>
                    foreach (object msg_org in (List<object>)((Pubnub)sender).DetailedHistory)
                    {
                        Console.WriteLine(msg_org.ToString());
                    }
                    deliveryStatus = true;
                }
            };
            pubnub.detailedHistory(channel, total_msg);
            while (!deliveryStatus) ;
            Console.WriteLine("\n******* DetailedHistory Messages Received ******* ");
        }

        public static void TestEncryptedDetailedHistory()
        {
            // Context setup for Detailed History
            pubnub.CIPHER_KEY = "enigma";
            int total_msg = 10;
            long starttime = Timestamp();
            Dictionary<long, string> inputs = new Dictionary<long, string>();
            for (int i = 0; i < total_msg / 2; i++)
            {
                string msg = i.ToString();
                pubnub.publish(channel, msg);
                long t = Timestamp();
                inputs.Add(t, msg);
                Console.WriteLine("Message # " + i.ToString() + " published");
            }

            long midtime = Timestamp();
            for (int i = total_msg / 2; i < total_msg; i++)
            {
                string msg = i.ToString();
                pubnub.publish(channel, msg);
                long t = Timestamp();
                inputs.Add(t, msg);
                Console.WriteLine("Message # " + i.ToString() + " published");
            }

            long endtime = Timestamp();

            deliveryStatus = false;
            pubnub.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "DetailedHistory")
                {
                    Console.WriteLine("\n*********** DetailedHistory Messages *********** ");
                    foreach (object msg_org in (List<object>)((Pubnub)sender).DetailedHistory)
                    {
                        Console.WriteLine(msg_org.ToString());
                    }
                    deliveryStatus = true;
                }
            };
            pubnub.detailedHistory(channel, total_msg);
            while (!deliveryStatus) ;
            Console.WriteLine("\n*********** DetailedHistory Messages Received*********** ");
        }

        public static void TestUnencryptedDetailedHistoryParams()
        {
            // Context setup for Detailed History
            pubnub.CIPHER_KEY = "";
            int total_msg = 10;
            long starttime = Timestamp();
            Dictionary<long, string> inputs = new Dictionary<long, string>();
            for (int i = 0; i < total_msg / 2; i++)
            {
                string msg = i.ToString();
                pubnub.publish(channel, msg);
                long t = Timestamp();
                inputs.Add(t, msg);
                Console.WriteLine("Message # " + i.ToString() + " published");
            }

            long midtime = Timestamp();
            for (int i = total_msg / 2; i < total_msg; i++)
            {
                string msg = i.ToString();
                pubnub.publish(channel, msg);
                long t = Timestamp();
                inputs.Add(t, msg);
                Console.WriteLine("Message # " + i.ToString() + " published");
            }

            long endtime = Timestamp();

            deliveryStatus = false;
            pubnub.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "DetailedHistory")
                {
                    Console.WriteLine("\n*********** DetailedHistory Messages *********** ");
					//modified object[] to List<object>
                    foreach (object msg_org in (List<object>)((Pubnub)sender).DetailedHistory)
                    {
                        Console.WriteLine(msg_org.ToString());
                    }
                    deliveryStatus = true;
                }
            };
            Console.WriteLine("DetailedHistory with start & end");
            pubnub.detailedHistory(channel, starttime, midtime, total_msg / 2, true);
            while (!deliveryStatus) ;
            Console.WriteLine("DetailedHistory with start & reverse = true");
            deliveryStatus = false;
            pubnub.detailedHistory(channel, midtime, -1, total_msg / 2, true);
            while (!deliveryStatus) ;
            Console.WriteLine("DetailedHistory with start & reverse = false");
            deliveryStatus = false;
            pubnub.detailedHistory(channel, midtime, -1, total_msg / 2, false);
            while (!deliveryStatus) ;
            Console.WriteLine("\n******* DetailedHistory Messages Received ******* ");
        }

        public static void BasicEncryptionDecryptionTests ()
        {
            PubnubCrypto pc = new PubnubCrypto("enigma");

            string enc = pc.EncryptOrDecrypt(true, "Pubnub Messaging API 1");
            Console.WriteLine ("Pubnub Messaging API 1 = " + enc);
            Console.WriteLine ("dec = " + pc.EncryptOrDecrypt(false, enc));

            enc = pc.EncryptOrDecrypt(true, "yay!");
            Console.WriteLine ("yay = " + enc);
            Console.WriteLine ("dec = " + pc.EncryptOrDecrypt(false, enc));

            Console.WriteLine ("Wi24KS4pcTzvyuGOHubiXg==: = " + pc.EncryptOrDecrypt(false, "Wi24KS4pcTzvyuGOHubiXg=="));
            Console.WriteLine ("f42pIQcWZ9zbTbH8cyLwB/tdvRxjFLOYcBNMVKeHS54=: = " + pc.EncryptOrDecrypt(false, "f42pIQcWZ9zbTbH8cyLwB/tdvRxjFLOYcBNMVKeHS54="));
            Console.WriteLine ("f42pIQcWZ9zbTbH8cyLwByD/GsviOE0vcREIEVPARR0=: = " + pc.EncryptOrDecrypt(false, "f42pIQcWZ9zbTbH8cyLwByD/GsviOE0vcREIEVPARR0="));
            Console.WriteLine ("zMqH/RTPlC8yrAZ2UhpEgLKUVzkMI2cikiaVg30AyUu7B6J0FLqCazRzDOmrsFsF = " + pc.EncryptOrDecrypt(false, "zMqH/RTPlC8yrAZ2UhpEgLKUVzkMI2cikiaVg30AyUu7B6J0FLqCazRzDOmrsFsF"));
            Console.WriteLine ("GsvkCYZoYylL5a7/DKhysDjNbwn+BtBtHj2CvzC4Y4g= = " + pc.EncryptOrDecrypt(false, "GsvkCYZoYylL5a7/DKhysDjNbwn+BtBtHj2CvzC4Y4g="));

            Console.WriteLine ("IDjZE9BHSjcX67RddfCYYg== = " + pc.EncryptOrDecrypt(false, "IDjZE9BHSjcX67RddfCYYg=="));
            Console.WriteLine ("Ns4TB41JjT2NCXaGLWSPAQ== = " + pc.EncryptOrDecrypt(false, "Ns4TB41JjT2NCXaGLWSPAQ=="));

            Console.WriteLine ("+BY5/miAA8aeuhVl4d13Kg== = " + pc.EncryptOrDecrypt(false, "+BY5/miAA8aeuhVl4d13Kg=="));

            Console.WriteLine ("Zbr7pEF/GFGKj1rOstp0tWzA4nwJXEfj+ezLtAr8qqE= = " + pc.EncryptOrDecrypt(false, "Zbr7pEF/GFGKj1rOstp0tWzA4nwJXEfj+ezLtAr8qqE="));
            Console.WriteLine ("q/xJqqN6qbiZMXYmiQC1Fw==: = " + pc.EncryptOrDecrypt(false, "q/xJqqN6qbiZMXYmiQC1Fw=="));
        }

        public static void TestEncryptedDetailedHistoryParams()
        {
            // Context setup for Detailed Histor
            pubnub.CIPHER_KEY = "enigma";
            int total_msg = 10;
            long starttime = Timestamp();
            
            Dictionary<long, string> inputs = new Dictionary<long, string>();
            for (int i = 0; i < total_msg / 2; i++)
            {
                string msg = i.ToString();
                pubnub.publish(channel, msg);
                long t = Timestamp();
                inputs.Add(t, msg);
                Console.WriteLine("Message # " + i.ToString() + " published");
            }

            long midtime = Timestamp();
            for (int i = total_msg / 2; i < total_msg; i++)
            {
                string msg = i.ToString();
                pubnub.publish(channel, msg);
                long t = Timestamp();
                inputs.Add(t, msg);
                Console.WriteLine("Message # " + i.ToString() + " published");
            }


            long endtime = Timestamp();

            deliveryStatus = false;
            pubnub.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "DetailedHistory")
                {
                    Console.WriteLine("\n*********** DetailedHistory Messages *********** ");
                    foreach (object msg_org in (List<object>)((Pubnub)sender).DetailedHistory)
                    {
                        Console.WriteLine(msg_org.ToString());
                    }
                    deliveryStatus = true;
                }
            };
            Console.WriteLine("DetailedHistory with start & end");
            pubnub.detailedHistory(channel, starttime, midtime, total_msg / 2, true);
            while (!deliveryStatus) ;
            Console.WriteLine("DetailedHistory with start & reverse = true");
            deliveryStatus = false;
            pubnub.detailedHistory(channel, midtime, -1, total_msg / 2, true);
            while (!deliveryStatus) ;
            Console.WriteLine("DetailedHistory with start & reverse = false");
            deliveryStatus = false;
            pubnub.detailedHistory(channel, midtime, -1, total_msg / 2, false);
            while (!deliveryStatus) ;
            Console.WriteLine("\n******* DetailedHistory Messages Received ******* ");
        }

        public static long Timestamp()
        {
            deliveryStatus = false;
            pubnub.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Time")
                {
                    deliveryStatus = true;
                    //test
                    //Console.WriteLine("Timestamp delivered");
                }
            };
            pubnub.time();
            while (!deliveryStatus) ;
            return Convert.ToInt64(pubnub.Time[0].ToString());
        }
        public static void Publish_Example()
        {
            pubnub.CIPHER_KEY = "";
            pubnub.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Publish")
                {
                    Console.WriteLine("\n*********** Publish Messages *********** ");
                    Console.WriteLine(
                        "Publish Success: " + ((Pubnub)sender).Publish[0].ToString() +
                        "\nPublish Info: " + ((Pubnub)sender).Publish[1].ToString()
                        );
                }
            };
            pubnub.publish(channel, message);
        }
        
        public static void DetailedHistory_Example()
        {
            pubnub.CIPHER_KEY = "";
            //int start = 
            pubnub.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "DetailedHistory")
                {
                    Console.WriteLine("\n*********** DetailedHistory Messages *********** ");
                    MessageFeeder(((Pubnub)sender).DetailedHistory);
                    deliveryStatus = true;
                }
            };
            pubnub.detailedHistory(channel, 10);
            while (!deliveryStatus) ;
            Console.WriteLine("\n*********** DetailedHistory Messages Received*********** ");
        }

        public static void DetailedHistory_Decrypted_Example()
        {
            pubnub.CIPHER_KEY = "enigma";
            //int start = 
            pubnub.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "DetailedHistory")
                {
                    Console.WriteLine("\n*********** DetailedHistory Messages *********** ");
                    MessageFeeder((List<object>)(((Pubnub)sender).DetailedHistory));
                    deliveryStatus = true;
                }
            };
            pubnub.detailedHistory(channel, 1);
            while (!deliveryStatus) ;
            Console.WriteLine("\n*********** DetailedHistory Messages Received*********** ");
        }

        static void Timestamp_Example()
        {
            pubnub.CIPHER_KEY = "";
            pubnub.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Time")
                {
                    Console.WriteLine("\n********** Timestamp Messages ********** ");
                    MessageFeeder(((Pubnub)sender).Time[0]);
                }
            };
            pubnub.time();
        }
        public static void Subscribe_Example()
        {
            pubnub.CIPHER_KEY = "";
            pubnub.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "ReturnMessage")
                {
                    Console.WriteLine("\n********** Subscribe Messages ********** ");
                    MessageFeeder(((Pubnub)sender).ReturnMessage);
       			}
                //added Roger
                else if (e.PropertyName == "Subscribe")
                {
                    Console.WriteLine("\n********** Subscribe Messages ********** ");
                    MessageFeeder(((Pubnub)sender).Subscribe);
                }
            };
            pubnub.subscribe(channel);
        }

        public static void Presence_Example()
        {
            pubnub.CIPHER_KEY = "";
            pubnub.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "ReturnMessage")
                {
                    Console.WriteLine("\n********** Presence Messages ********** ");
                    MessageFeeder(((Pubnub)sender).ReturnMessage);
                }
                //added Roger
                else if (e.PropertyName == "Presence")
                {
                    Console.WriteLine("\n********** Presence Messages ********** ");
                    MessageFeeder(((Pubnub)sender).Presence);
                }
            };
            pubnub.presence(channel);
        }

        public static void HereNow_Example()
        {
            pubnub.CIPHER_KEY = "";
            pubnub.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Here_Now")
                {
                    Console.WriteLine("\n********** Here Now Messages *********** ");
                    Dictionary<string, object> _message = (Dictionary<string, object>)(((Pubnub)sender).Here_Now[0]);
                    foreach (object uuid in (object[])_message["uuids"])
                    {
                        Console.WriteLine("UUID: " + uuid.ToString());
                    }
                    Console.WriteLine("Occupancy: " + _message["occupancy"].ToString());
                }
            };
            pubnub.here_now(channel);
        }

        static void MessageFeeder(List<object> feed)
        {
            foreach (object message in feed)
            {
                try
                {
                    Dictionary<string, object> _messageHistory = (Dictionary<string, object>)(message);
                    Console.WriteLine("Key: " + _messageHistory.ElementAt(0).Key + " - Value: " + _messageHistory.ElementAt(0).Value);
                }
                catch
                {
                    Console.WriteLine(message.ToString());
                }
            }
        }
        static void MessageFeeder(object feed)
        {
            try
            {
                Dictionary<string, object> _message = (Dictionary<string, object>)(feed);
                for (int i = 0; i < _message.Count; i ++)
                    Console.WriteLine("Key: " + _message.ElementAt(i).Key + " - Value: " + _message.ElementAt(i).Value);
            }
            catch
            {
                try
                {
                    List<object> _message = (List<object>)feed;
                    for (int i = 0; i < _message.Count; i++)
                        Console.WriteLine(_message[i].ToString());
                }
                catch
                {
                    Console.WriteLine("Time: " + feed.ToString());
                }
                
            }
        }
    }
}
