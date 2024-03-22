using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gfycat.ScreenCapture
{
	public class GfycatCredentials: MonoBehaviour
	{
		public string APIKey;

		public string APISecret;
		
		void Awake()
		{
			if (!String.IsNullOrEmpty(APIKey) && !String.IsNullOrEmpty(APISecret))
			{
				AccountManager.Instance.SetClientCredentials(APIKey, APISecret);
			}
		}

		void Start()
		{
		}
	}
}
