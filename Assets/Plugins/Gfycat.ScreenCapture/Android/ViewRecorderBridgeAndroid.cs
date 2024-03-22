#if UNITY_ANDROID

using UnityEngine;
using System.Collections;
using Gfycat.ScreenCapture;
using System;

namespace Gfycat.ScreenCapture.Android
{
    public class ViewRecorderBridgeAndroid : ViewRecorder
    {
        private readonly AndroidJavaClass _javaClass;
        private readonly RecorderEventsProxy _eventsProxy;

        private Action finishRecordingCompletionHandler;
        private Action<bool> isRecordingSupportedCompletionHandler;

        public override event Action<string> OnFailure;

        public ViewRecorderBridgeAndroid()
        {
            _javaClass = new AndroidJavaClass("com.gfycat.screencapruteunity.RecorderBridge");
            _eventsProxy = new RecorderEventsProxy(this);

            _javaClass.CallStatic("init", new object[] {getContext(), _eventsProxy});
        }

        public override string OutputPath
        {
            get
            {
                return _javaClass.CallStatic<string>("getOutputPath");
            }
        }

        public override bool IsSourceViewSupported()
        {
            return false;
        }

        public override bool IsSourceRectSupported()
        {
            return false;
        }

        public override IntPtr SourceView
        {
            get 
            { 
                throw new NotImplementedException(); 
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public override Rect SourceRect
        {
            get 
            { 
                throw new NotImplementedException();

            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public override float Scale
        {
            get
            {
                return _javaClass.CallStatic<float>("getScale");
            }
            set
            {
                _javaClass.CallStatic("setScale", new object[] { value });
            }
        }
        public override int FrameRate
        {
            get 
            { 
                return _javaClass.CallStatic<int>("getFrameRate"); 
            }
            set
            {
                _javaClass.CallStatic("setFrameRate", new object[] { value });
            }
        }
        public override float BufferDuration
        {
            get
            {
                return _javaClass.CallStatic<float>("setBufferDuration");
            }
            set
            {
                _javaClass.CallStatic("setBufferDuration", new object[] { value });
            }
        }

        public override void Cancel()
        {
            _javaClass.CallStatic("cancel", new object[] { getContext() });
        }

        public override void Finish(Action completionHandler)
        {
            finishRecordingCompletionHandler = completionHandler;
            _javaClass.CallStatic("finish", new object[] { getContext() });
        }

        public override void Pause()
        {
            _javaClass.CallStatic("pause", new object[] { getContext() });
        }

        public override void Resume()
        {
            _javaClass.CallStatic("resume", new object[] { getContext() });
        }

        public override void Start()
        {
            _javaClass.CallStatic("start", new object[] { getContext() });
        }

        public override void IsRecordingSupported(Action<bool> completionHandler)
        {
            isRecordingSupportedCompletionHandler = completionHandler;
            _javaClass.CallStatic("checkRecordingSupported", new object[] { getContext() });
        }

        private AndroidJavaObject getContext() {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            return activity;
        }

        private class RecorderEventsProxy : AndroidJavaProxy
        {
            private ViewRecorderBridgeAndroid viewRecorder;

            public RecorderEventsProxy(ViewRecorderBridgeAndroid viewRecorder) : base("com.gfycat.screencapruteunity.RecorderBridgeListener")
            {
                this.viewRecorder = viewRecorder;
            }

            public void onPermissionsError(String errorMessage)
            {
                this.viewRecorder.OnFailure.Invoke(errorMessage);
            }

            public void onDeviceError(String errorMessage)
            {
                this.viewRecorder.OnFailure.Invoke(errorMessage);
            }

            public void onError(String errorMessage)
            {
                this.viewRecorder.OnFailure.Invoke(errorMessage);
            }

            public void onCapturingStarted()
            {
            }

            public void onSavingStarted()
            {
            }

            public void onSaved(String filePath)
            {
                if (viewRecorder.finishRecordingCompletionHandler != null) {
                    viewRecorder.finishRecordingCompletionHandler.Invoke();
                }
            }

            public void onCanceled()
            {
            }

            public void onRecordingSupportChecked(bool isSupported) {
                if (viewRecorder.isRecordingSupportedCompletionHandler != null)
                {
                    viewRecorder.isRecordingSupportedCompletionHandler.Invoke(isSupported);
                }
            }
        }
    }
}

#endif