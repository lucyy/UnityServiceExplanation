#if UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using AOT;
using UnityEngine;

namespace Gfycat.ScreenCapture.Stubs
{
    public class VideoUploaderStub: VideoUploader
    {
        public override event Action<string> OnGfycatRegistered;

        public override event Action<string> OnGfycatUploaded;

        public override event Action<string> OnFailure;

        public override string VideoPath { get; set; }

        public override string Title { get; set; }

        public override string GfyId { get { return null; } }

        public override string GfycatUrl { get { return null; } }

        public override void Upload(Action<string> completionHandler)
        {
            if (completionHandler != null)
            {
                OnGfycatRegistered += completionHandler;
                OnFailure += (error) => completionHandler(null);
            }

            if (OnFailure != null)
            {
                OnFailure("Not Implemented.");
            }
        }
    }
}

#endif
