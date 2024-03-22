#if UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using AOT;
using UnityEngine;

namespace Gfycat.ScreenCapture.Stubs
{
    public class ViewRecorderStub: ViewRecorder
    {
        public override string OutputPath { get { return ""; } }

        public override void IsRecordingSupported(Action<bool> completionHandler)
        {
            completionHandler(false);
        }

        public override bool IsSourceViewSupported()
        {
            return false;
        }

        public override bool IsSourceRectSupported()
        {
            return false;
        }

        public override IntPtr SourceView { get; set; }
        
    	public override Rect SourceRect { get; set; }

    	public override float Scale { get; set; }

    	public override Int32 FrameRate { get; set; }

    	public override float BufferDuration { get; set; }

        public override event Action<string> OnFailure;

        public override void Start()
    	{
    	}

    	public override void Pause()
    	{
    	}

    	public override void Resume()
    	{
    	}

    	public override void Cancel()
    	{
    	}

    	public override void Finish(Action completionHandler)
    	{
			completionHandler();
    	}
	}
}

#endif
