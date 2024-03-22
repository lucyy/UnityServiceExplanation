#if UNITY_IOS && !UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace Gfycat.ScreenCapture.iOS
{
    public class AccountManagerBridge : AccountManager
    {
        public override void SetClientCredentials(string appClientID, string appClientSecret)
        {
            _GFYAccountManager_setClientCredentials(appClientID, appClientSecret);
        }

        public override bool IsLoggedIn { get { return _GFYAccountManager_isLoggedIn(); } }

		public override string Username { get { return _GFYAccountManager_getUsername(); } }

        public override void Login(Action<bool> completionHandler)
        {
            var handle = GCHandle.Alloc(completionHandler);
            var cookie = GCHandle.ToIntPtr(handle);
            _GFYAccountManager_login(cookie, LoginCompletionHandlerProxy);
        }

        public override void Logout()
        {
            _GFYAccountManager_logout();
        }

        [MonoPInvokeCallback(typeof(Action<IntPtr, IntPtr, string>))]
        private static void LoginCompletionHandlerProxy(IntPtr cookie, bool success)
        {
            var handle = GCHandle.FromIntPtr(cookie);
            var action = handle.Target as Action<bool>;
            action(success);
            handle.Free();
        }

        [DllImport ("__Internal")]
        private static extern void _GFYAccountManager_setClientCredentials(string appClientID, string appClientSecret);

        [DllImport ("__Internal")]
        private static extern bool _GFYAccountManager_isLoggedIn();

        [DllImport ("__Internal")]
        private static extern string _GFYAccountManager_getUsername();

        [DllImport ("__Internal")]
        private static extern void _GFYAccountManager_login(IntPtr cookie, Action<IntPtr, bool> completionHandler);

        [DllImport ("__Internal")]
        private static extern void _GFYAccountManager_logout();
    }
}

#endif
