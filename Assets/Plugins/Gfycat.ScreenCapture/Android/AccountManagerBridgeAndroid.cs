#if UNITY_ANDROID

using UnityEngine;
using System.Collections;
using Gfycat.ScreenCapture;
using System;

namespace Gfycat.ScreenCapture.Android
{
    public class AccountManagerBridgeAndroid : AccountManager
    {
        private readonly AndroidJavaClass _javaClass;
        private readonly AccountManagerEventsProxy _eventsProxy;

        private Action<bool> loginCompletionHandler;

        public AccountManagerBridgeAndroid()
        {
            _javaClass = new AndroidJavaClass("com.gfycat.screencapruteunity.AccountManagerBridge");
            _eventsProxy = new AccountManagerEventsProxy(this);
        }

        public override void SetClientCredentials(string appClientID, string appClientSecret)
        {
            _javaClass.CallStatic("init", new object[] { getContext(), appClientID, appClientSecret, _eventsProxy });
        }

        public override bool IsLoggedIn
        {
            get
            {
                return _javaClass.CallStatic<bool>("isSignedIn");
            }
        }

        public override string Username
        {
            get
            {
                return _javaClass.CallStatic<string>("getUsername");
            }
        }

        public override void Login(Action<bool> completionHandler)
        {
            this.loginCompletionHandler = completionHandler;
            _javaClass.CallStatic("startSignInFlow", new object[] { getContext() });
        }

        public override void Logout()
        {
            _javaClass.CallStatic("signOut");
        }

        private AndroidJavaObject getContext() {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            return activity;
        }

        private class AccountManagerEventsProxy : AndroidJavaProxy
        {
            private AccountManagerBridgeAndroid accountManager;

            public AccountManagerEventsProxy(AccountManagerBridgeAndroid accountManager) : base("com.gfycat.screencapruteunity.AccountManagerBridgeListener")
            {
                this.accountManager = accountManager;
            }

            public void onError(String errorMessage)
            {
            }

            public void onSignInSuccess()
            {
                if (accountManager.loginCompletionHandler != null) {
                    accountManager.loginCompletionHandler.Invoke(true);
                }
            }

            public void onSignOutSuccess()
            {
            }
        }
    }
}

#endif