#if UNITY_ANDROID

using UnityEngine;
using System.Collections;
using Gfycat.ScreenCapture;
using System;

namespace Gfycat.ScreenCapture.Android
{
    public class VideoUploaderBridgeAndroid : VideoUploader
    {
        private readonly AndroidJavaClass _javaClass;
        private readonly UploaderEventsProxy _eventsProxy;

        private string videoPath = "";
        private string gfyId = "";
        private string gfycatUrl = "";

        public VideoUploaderBridgeAndroid()
        {
            _javaClass = new AndroidJavaClass("com.gfycat.screencapruteunity.UploaderBridge");
            _eventsProxy = new UploaderEventsProxy(this);
            _javaClass.CallStatic("init", new object[] { _eventsProxy });
        }

        public override string VideoPath
        {
            get { return videoPath; }
            set
            {
                videoPath = value;
            }
        }
        public override string Title
        {
            get 
            { 
                return _javaClass.CallStatic<string>("getTitle"); 
            }
            set
            {
                _javaClass.CallStatic("setTitle", new object[] { value });
            }
        }

        public override string GfyId
        {
            get
            {
                return gfyId;
            }
        }

        public override string GfycatUrl
        {
            get
            {
                return gfycatUrl;
            }
        }

        public override event Action<string> OnGfycatRegistered;
        public override event Action<string> OnGfycatUploaded;
        public override event Action<string> OnFailure;

        public override void Upload(Action<string> completionHandler = null)
        {
            _javaClass.CallStatic("upload", new object[] { getContext(), videoPath });
        }

        private AndroidJavaObject getContext() {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            return activity;
        }

        private class UploaderEventsProxy : AndroidJavaProxy
        {
            private VideoUploaderBridgeAndroid videoUploader;

            public UploaderEventsProxy(VideoUploaderBridgeAndroid videoUploader) : base("com.gfycat.screencapruteunity.UploaderBridgeControllerListener")
            {
                this.videoUploader = videoUploader;
            }

            public void onGfycatRegistered(String gfycatName)
            {
                if (videoUploader.OnGfycatRegistered != null) {
                    videoUploader.OnGfycatRegistered.Invoke(gfycatName);
                }
            }

            public void onGfycatUploaded(String url)
            {
                videoUploader.gfycatUrl = url;
                if (videoUploader.OnGfycatUploaded != null)
                {
                    videoUploader.OnGfycatUploaded.Invoke(url);
                }
            }

            public void onFailed(String errorMessage)
            {
                if (videoUploader.OnFailure != null)
                {
                    videoUploader.OnFailure.Invoke(errorMessage);
                }
            }
        }
    }
}

#endif