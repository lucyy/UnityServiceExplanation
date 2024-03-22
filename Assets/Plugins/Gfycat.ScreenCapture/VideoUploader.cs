using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gfycat.ScreenCapture
{
	/// <summary>
    /// This class provides Gfycat video upload functionality.
    /// Your application must keed this object alive until upload completes or fails.
	/// </summary>
    public abstract class VideoUploader
    {
        /// <summary>
        /// Creates a platform-specific instance of the uploader
        /// </summary>
        public static VideoUploader Create()
        {
#if UNITY_EDITOR
			return new Stubs.VideoUploaderStub();
#elif UNITY_IOS
			return new iOS.VideoUploaderBridge();
#elif UNITY_ANDROID
            return new Android.VideoUploaderBridgeAndroid();
#else
            #error Platform not supported.
#endif
        }

        /// <summary>
        /// Fired after allocating new Gfycat ID, receives gfyId as a parameter
        /// </summary>
        public abstract event Action<string> OnGfycatRegistered;

        /// <summary>
        /// Fired after uploading media content, receives gfyId as a parameter
        /// </summary>
        public abstract event Action<string> OnGfycatUploaded;

        /// <summary>
        /// Fired in case of error, receives error message as a parameter
        /// </summary>
        public abstract event Action<string> OnFailure;

        /// <summary>
        /// Path to mp4 file to be uploaded
        /// </summary>
        public abstract string VideoPath { get; set; }

        /// <summary>
        /// Gfycat title
        /// </summary>
        public abstract string Title { get; set; }

        /// <summary>
        /// Gfycat ID of the newly created Gfycat
        /// </summary>
        public abstract string GfyId { get; }

        /// <summary>
        /// Gfycat URL of the newly created Gfycat
        /// </summary>
        public abstract string GfycatUrl { get; }

        /// <summary>
        /// Initiates upload, passes Gfycat ID to the `completionHandler` on success or `null` on failure.
        /// </summary>
        public abstract void Upload(Action<string> completionHandler = null);
    }
}
