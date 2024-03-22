using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gfycat.ScreenCapture
{
	/// <summary>
    /// This class provides screen capture functionality.
    /// Your application must keed this object alive until video capture session is finished or cancelled.
	/// </summary>
    public abstract class ViewRecorder 
    {
        /// <summary>
        /// Creates a platform-specific instance of the recorder
        /// </summary>
		public static ViewRecorder Create()
		{
#if UNITY_EDITOR
			return new Stubs.ViewRecorderStub();
#elif UNITY_IOS
			return new iOS.ViewRecorderBridge();
#elif UNITY_ANDROID
            return new Android.ViewRecorderBridgeAndroid();
#else
            #error Platform not supported.
#endif
		}

        /// <summary>
        /// Path to mp4 file to be created
        /// </summary>
        public abstract string OutputPath { get; }

        /// <summary>
        /// Checks if recording is supported
        /// </summary>
        public abstract void IsRecordingSupported(Action<bool> completionHandler);
       
        /// <summary>
        /// Checks pissibility
        /// </summary>
        public abstract bool IsSourceViewSupported();

        /// <summary>
        /// Source view reference, initialized to main view by default
        /// </summary>
        public abstract IntPtr SourceView { get; set; }

        /// <summary>
        /// Checks pissibility to capture video
        /// </summary>
        public abstract bool IsSourceRectSupported();

        /// <summary>
        /// Source rect in view coordinate system, defaults to full screen
        /// </summary>
    	public abstract Rect SourceRect { get; set; }

        /// <summary>
        /// Capture scale, defaults to screen scale
        /// </summary>
    	public abstract float Scale { get; set; }

        /// <summary>
        /// Capture frame rate
        /// </summary>
    	public abstract Int32 FrameRate { get; set; }

        /// <summary>
        /// Maximum capture duration in seconds
        /// </summary>
    	public abstract float BufferDuration { get; set; }

        /// <summary>
        /// Starts video capture session, no properties should be modified after this call
        /// </summary>
    	public abstract void Start();

        /// <summary>
        /// Pauses video capture session
        /// </summary>
    	public abstract void Pause();

        /// <summary>
        /// Rresumes paused video capture session
        /// </summary>
    	public abstract void Resume();

        /// <summary>
        /// Cancels started video capture session
        /// </summary>
    	public abstract void Cancel();

        /// <summary>
        /// Cinishes action video capture session and calls `completionHandler` when done
        /// </summary>
    	public abstract void Finish(Action completionHandler);

        /// <summary>
        /// Fired in case of error, receives error message as a parameter
        /// </summary>
        public abstract event Action<string> OnFailure;
    }
}
