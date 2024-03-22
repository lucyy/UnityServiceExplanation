#if UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace Gfycat.ScreenCapture.Stubs
{
    public class AccountManagerStub : AccountManager
    {
        public override void SetClientCredentials(string appClientID, string appClientSecret)
        {
        }

        public override bool IsLoggedIn { get { return false; } }

		public override string Username { get { return null; } }

        public override void Login(Action<bool> completionHandler)
        {
            completionHandler(false);
        }

        public override void Logout()
        {
        }
    }
}

#endif
