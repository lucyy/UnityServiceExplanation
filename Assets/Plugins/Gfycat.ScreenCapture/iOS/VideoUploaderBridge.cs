#if UNITY_IOS && !UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using AOT;
using UnityEngine;

namespace Gfycat.ScreenCapture.iOS
{
    public class VideoUploaderBridge: VideoUploader
    {
        private GCHandle m_selfHandle;
        private IntPtr m_selfCookie;
        private IntPtr m_instance;
        private IntPtr m_listener;

        public VideoUploaderBridge()
    	{
            m_selfHandle = GCHandle.Alloc(this, GCHandleType.Weak);
            m_selfCookie = GCHandle.ToIntPtr(m_selfHandle);
            m_instance = _GFYVideoUploader_new();
            m_listener = _GFYVideoUploaderListener_new(m_selfCookie, DidRegisterGfycatProxy, DidUploadGfycatProxy, DidFailProxy);
            _GFYVideoUploader_setListener(m_instance, m_listener);
    	}

        ~VideoUploaderBridge()
    	{
            _GFYVideoUploader_release(m_instance);
            _GFYVideoUploaderListener_release(m_listener);
            m_selfHandle.Free();
    	}

        public override event Action<string> OnGfycatRegistered;

        public override event Action<string> OnGfycatUploaded;

        public override event Action<string> OnFailure;

        public override string VideoPath
        {
            get { return _GFYVideoUploader_getVideoPath(m_instance); }
            set { _GFYVideoUploader_setVideoPath(m_instance, value); }
        }

        public override string Title
        {
            get { return _GFYVideoUploader_getTitle(m_instance); }
            set { _GFYVideoUploader_setTitle(m_instance, value); }
        }

        public override string GfyId
        {
            get { return _GFYVideoUploader_getGfyId(m_instance); }
        }

        public override string GfycatUrl
        {
            get { return _GFYVideoUploader_getGfycatURL(m_instance); }
        }

        public override void Upload(Action<string> completionHandler)
        {
            if (completionHandler != null)
            {
                OnGfycatUploaded += completionHandler;
                OnFailure += (error) => completionHandler(null);
            }

            _GFYVideoUploader_uploadVideo(m_instance);
        }

        [MonoPInvokeCallback(typeof(Action<IntPtr, IntPtr, string>))]
        private static void DidRegisterGfycatProxy(IntPtr cookie, IntPtr uploader, string gfyId)
        {
            var handle = GCHandle.FromIntPtr(cookie);
            var self = handle.Target as VideoUploaderBridge;
            if (self.OnGfycatRegistered != null)
            {
                self.OnGfycatRegistered(gfyId);
            }
        }

        [MonoPInvokeCallback(typeof(Action<IntPtr, IntPtr, string>))]
        private static void DidUploadGfycatProxy(IntPtr cookie, IntPtr uploader, string gfyId)
        {
            var handle = GCHandle.FromIntPtr(cookie);
            var self = handle.Target as VideoUploaderBridge;
            if (self.OnGfycatUploaded != null)
            {
                self.OnGfycatUploaded(gfyId);
            }
        }

        [MonoPInvokeCallback(typeof(Action<IntPtr, IntPtr, string>))]
        private static void DidFailProxy(IntPtr cookie, IntPtr uploader, string errorMessage)
        {
            var handle = GCHandle.FromIntPtr(cookie);
            var self = handle.Target as VideoUploaderBridge;
            if (self.OnFailure != null)
            {
                self.OnFailure(errorMessage);
            }
        }

    	[DllImport ("__Internal")]
        private static extern IntPtr _GFYVideoUploader_new();

    	[DllImport ("__Internal")]
        private static extern void _GFYVideoUploader_release(IntPtr box);

        [DllImport ("__Internal")]
        private static extern string _GFYVideoUploader_getVideoPath(IntPtr box);

        [DllImport ("__Internal")]
        private static extern void _GFYVideoUploader_setVideoPath(IntPtr box, string videoPath);

        [DllImport ("__Internal")]
        private static extern string _GFYVideoUploader_getTitle(IntPtr box);

        [DllImport ("__Internal")]
        private static extern void _GFYVideoUploader_setTitle(IntPtr box, string title);

        [DllImport ("__Internal")]
        private static extern IntPtr _GFYVideoUploader_getListener(IntPtr box);

        [DllImport ("__Internal")]
        private static extern void _GFYVideoUploader_setListener(IntPtr box, IntPtr listener);

        [DllImport ("__Internal")]
        private static extern string _GFYVideoUploader_getGfyId(IntPtr box);

        [DllImport ("__Internal")]
        private static extern string _GFYVideoUploader_getGfycatURL(IntPtr box);

        [DllImport ("__Internal")]
        private static extern void _GFYVideoUploader_uploadVideo(IntPtr box);

        [DllImport ("__Internal")]
        private static extern IntPtr _GFYVideoUploaderListener_new(IntPtr cookie,
            Action<IntPtr, IntPtr, string> didRegisterGfycatWithId,
            Action<IntPtr, IntPtr, string> didUploadGfycatWithId,
            Action<IntPtr, IntPtr, string> didFailWithError);

        [DllImport ("__Internal")]
        private static extern void _GFYVideoUploaderListener_release(IntPtr box);
    }
}

#endif
