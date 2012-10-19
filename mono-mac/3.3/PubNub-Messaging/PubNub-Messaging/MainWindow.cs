
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
            //Subscribe Example
            //Pubnub_Example.Subscribe_Example(); //Success

            //Publish Example
            //Pubnub_Example.Publish_Example(); //Success

            Pubnub_Example.BasicEncryptionDecryptionTests();//success

            //TestEncryptedDetailedHistoryParams
            //Pubnub_Example.TestEncryptedDetailedHistoryParams (); //success

            //TestUnencryptedDetailedHistory
            //Pubnub_Example.TestUnencryptedDetailedHistory(); //success

            //TestUnencryptedDetailedHistoryParams
            //Pubnub_Example.TestUnencryptedDetailedHistoryParams(); //success

            //DetailedHistory_Example
            //Pubnub_Example.DetailedHistory_Example(); //success
            

            //Timestamp
            //Pubnub_Example.Timestamp(); //success

            //HereNow_Example
            //Pubnub_Example.HereNow_Example(); //success

            //Presence_Example
            //Pubnub_Example.Presence_Example(); //success

            //TestUnencryptedHistory
            //Pubnub_Example.TestUnencryptedHistory(); //success


            //TestUnencryptedDetailedHistory
            //Pubnub_Example.TestUnencryptedDetailedHistory(); //success
            
            //TestEncryptedDetailedHistory
            //Pubnub_Example.TestEncryptedDetailedHistory(); //success

            //TestUnencryptedDetailedHistoryParams
            //Pubnub_Example.TestUnencryptedDetailedHistoryParams(); //success

            //TestEncryptedDetailedHistoryParams
            //Pubnub_Example.TestEncryptedDetailedHistoryParams(); //success
            
            //TestEncryptedHistory
            //Pubnub_Example.TestEncryptedHistory(); //Success

            //DetailedHistory_Decrypted_Example
            //Pubnub_Example.DetailedHistory_Decrypted_Example(); //Success

		}
		
		#endregion
	}
}

