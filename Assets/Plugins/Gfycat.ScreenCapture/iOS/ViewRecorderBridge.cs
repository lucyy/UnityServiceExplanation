#if UNITY_IOS && !UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using AOT;
using UnityEngine;

namespace Gfycat.ScreenCapture.iOS
{
    public class ViewRecorderBridge: ViewRecorder
    {
        private IntPtr m_instance;

        public override event Action<string> OnFailure;

        public ViewRecorderBridge()
    	{
    		m_instance = _GFYViewRecorder_new();
    	}

        ~ViewRecorderBridge()
    	{
            _GFYViewRecorder_release(m_instance);
        }

        public override void IsRecordingSupported(Action<bool> completionHandler)
        {
            completionHandler(true);
        }

        public override bool IsSourceViewSupported()
        {
            return true;
        }

        public override bool IsSourceRectSupported()
        {
            return true;
        }

        public override string OutputPath
    	{
    		get { return _GFYViewRecorder_getOutputPath(m_instance); }
    	}

        public override IntPtr SourceView
        {
            get { return _GFYViewRecorder_getSourceView(m_instance); }
            set { _GFYViewRecorder_setSourceView(m_instance, value); }
        }

    	public override Rect SourceRect
    	{
    		get
    		{
    			float left, top, width, height;
    			_GFYViewRecorder_getSourceRect(m_instance, out left, out top, out width, out height);
    			return new Rect(left, top, width, height);
    		}
    		set
    		{
    			_GFYViewRecorder_setSourceRect(m_instance, value.xMax, value.yMin, value.width, value.height);
    		}
    	}

    	public override float Scale
    	{
    		get { return _GFYViewRecorder_getScale(m_instance); }
    		set { _GFYViewRecorder_setScale(m_instance, value); }
    	}

    	public override Int32 FrameRate
    	{
    		get { return _GFYViewRecorder_getFrameRate(m_instance); }
    		set { _GFYViewRecorder_setFrameRate(m_instance, value); }
    	}

    	public override float BufferDuration
    	{
    		get { return _GFYViewRecorder_getBufferDuration(m_instance); }
    		set { _GFYViewRecorder_setBufferDuration(m_instance, value); }
    	}

    	public override void Start()
    	{
    		_GFYViewRecorder_start(m_instance);
    	}

    	public override void Pause()
    	{
    		_GFYViewRecorder_pause(m_instance);
    	}

    	public override void Resume()
    	{
    		_GFYViewRecorder_resume(m_instance);
    	}

    	public override void Cancel()
    	{
    		_GFYViewRecorder_cancel(m_instance);
    	}

    	public override void Finish(Action completionHandler)
    	{
            _GFYViewRecorder_finish(m_instance, CallbackProxy, GCHandle.ToIntPtr(GCHandle.Alloc(completionHandler)));
    	}

        [MonoPInvokeCallback(typeof(Action<IntPtr>))]
        private static void CallbackProxy(IntPtr cookie)
        {
            var handle = GCHandle.FromIntPtr(cookie);
            var callback = handle.Target as Action;
            callback();
            handle.Free();
        }

    	[DllImport ("__Internal")]
    	private static extern IntPtr _GFYViewRecorder_new();

    	[DllImport ("__Internal")]
        private static extern void _GFYViewRecorder_release(IntPtr box);

    	[DllImport ("__Internal")]
        private static extern string _GFYViewRecorder_getOutputPath(IntPtr box);

        [DllImport ("__Internal")]
        private static extern void _GFYViewRecorder_setOutputPath(IntPtr box, string outputPath);

        [DllImport ("__Internal")]
        private static extern IntPtr _GFYViewRecorder_getSourceView(IntPtr box);

        [DllImport ("__Internal")]
        private static extern void _GFYViewRecorder_setSourceView(IntPtr box, IntPtr sourceView);

    	[DllImport ("__Internal")]
        private static extern void _GFYViewRecorder_getSourceRect(IntPtr box, out float left, out float top, out float width, out float height);

        [DllImport ("__Internal")]
        private static extern void _GFYViewRecorder_setSourceRect(IntPtr box, float left, float top, float width, float height);

    	[DllImport ("__Internal")]
        private static extern float _GFYViewRecorder_getScale(IntPtr box);

        [DllImport ("__Internal")]
        private static extern void _GFYViewRecorder_setScale(IntPtr box, float scale);

    	[DllImport ("__Internal")]
        private static extern Int32 _GFYViewRecorder_getFrameRate(IntPtr box);

        [DllImport ("__Internal")]
        private static extern void _GFYViewRecorder_setFrameRate(IntPtr box, Int32 frameRate);

    	[DllImport ("__Internal")]
        private static extern Int32 _GFYViewRecorder_getKeyframeRate(IntPtr box);

        [DllImport ("__Internal")]
        private static extern void _GFYViewRecorder_setKeyframeRate(IntPtr box, Int32 keyframeRate);

    	[DllImport ("__Internal")]
        private static extern float _GFYViewRecorder_getBufferDuration(IntPtr box);

        [DllImport ("__Internal")]
        private static extern void _GFYViewRecorder_setBufferDuration(IntPtr box, float bufferDuration);

    	[DllImport ("__Internal")]
        private static extern void _GFYViewRecorder_start(IntPtr box);

    	[DllImport ("__Internal")]
        private static extern void _GFYViewRecorder_pause(IntPtr box);

    	[DllImport ("__Internal")]
        private static extern void _GFYViewRecorder_resume(IntPtr box);

    	[DllImport ("__Internal")]
        private static extern void _GFYViewRecorder_cancel(IntPtr box);

    	[DllImport ("__Internal")]
        private static extern void _GFYViewRecorder_finish(IntPtr box, Action<IntPtr> completionHandler, IntPtr cookie);
    }
}

#endif
