using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gfycat.ScreenCapture
{
	/// <summary>
    /// This class provides Gfycat account management functionality.
	/// </summary>
	public abstract class AccountManager
	{
        /// <summary>
        /// Creates a platform-specific instance of the manager
        /// </summary>
		public static AccountManager Instance { get; private set; }

        static AccountManager()
        {
#if UNITY_EDITOR
			Instance = new Stubs.AccountManagerStub();
#elif UNITY_IOS
			Instance = new iOS.AccountManagerBridge();
#elif UNITY_ANDROID
            Instance = new Android.AccountManagerBridgeAndroid();
#else
            #error Platform not supported.
#endif
        }

        /// <summary>
        /// Call this function once per application lifetime to specify Gfycat API credentials
        /// </summary>
        public abstract void SetClientCredentials(string appClientID, string appClientSecret);

        /// <summary>
        /// Returns user login status
        /// </summary>
		public abstract bool IsLoggedIn { get; }

        /// <summary>
        /// Returns currently logged in username
        /// </summary>
		public abstract string Username { get; }

        /// <summary>
        /// Authenticate user with Gfycat
        /// </summary>
		public abstract void Login(Action<bool> completionHandler);

        /// <summary>
        /// Clear currently authenticated user
        /// </summary>
		public abstract void Logout();
	}
}
