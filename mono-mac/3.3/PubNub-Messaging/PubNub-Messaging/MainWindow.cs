
using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace PubNubMessaging
{
	public partial class MainWindow : MonoMac.AppKit.NSWindow
	{
		#region Constructors
		
		// Called when created from unmanaged code
		public MainWindow (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public MainWindow (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
            //Success
            Pubnub_Example.TestEncryptedDetailedHistoryParams ();

            //Success
            //Pubnub_Example.TestUnencryptedDetailedHistory();

            //Success
            //Pubnub_Example.TestUnencryptedDetailedHistoryParams();

            //Success
            //Console.WriteLine("\nRunning publish()");
            //Pubnub_Example.Publish_Example();

            //Success
            //Console.WriteLine("\nRunning detailedHistory()");
            //Pubnub_Example.DetailedHistory_Example();

            //Random Failure, looks like when detailed history is empty
            //Console.WriteLine("\nRunning detailedHistory()");
            //Pubnub_Example.DetailedHistory_Decrypted_Example();

            //Success
            //Console.WriteLine("\nRunning timestamp()");
            //Pubnub_Example.Timestamp();

            //Success
            //Console.WriteLine("\nRunning here_now()");
            //Pubnub_Example.HereNow_Example();

            //Success
            //Console.WriteLine("\nRunning presence()");
            //Pubnub_Example.Presence_Example();

            //Success
            //Console.WriteLine("\nRunning Subscribe()");
            //Pubnub_Example.Subscribe_Example();

            //Success
            //Console.WriteLine("\nRunning TestUnencryptedHistory()");
            //Pubnub_Example.TestUnencryptedHistory();

            //Success
            //Console.WriteLine("\nRunning TestEncryptedHistory()");
            //Pubnub_Example.TestEncryptedHistory();

            //Success
            //Console.WriteLine("\nRunning TestUnencryptedDetailedHistory()");
            //Pubnub_Example.TestUnencryptedDetailedHistory();
            
            //Success
            //Console.WriteLine("\nRunning TestEncryptedDetailedHistory()");
            //Pubnub_Example.TestEncryptedDetailedHistory();

            //Success
            //Console.WriteLine("\nRunning TestUnencryptedDetailedHistoryParams()");
            //Pubnub_Example.TestUnencryptedDetailedHistoryParams();

            //Success
            //Console.WriteLine("\nRunning TestEncryptedDetailedHistoryParams()");
            //Pubnub_Example.TestEncryptedDetailedHistoryParams();

		}
		
		#endregion
	}
}

