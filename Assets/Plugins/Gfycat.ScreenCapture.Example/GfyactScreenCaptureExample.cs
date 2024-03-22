using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gfycat.ScreenCapture;

public class GfyactScreenCaptureExample: MonoBehaviour
{
    private ViewRecorder _recorder;
    private VideoUploader _uploader;

    public Text LoginStatus;

    void Start()
    {
        // AccountManager.Instance.SetClientCredentials(GfycatCredentials.APIKey, GfycatCredentials.APISecret);
        UpdateLoginStatus();
    }
    
    void Update()
    {
    }

    private void UpdateLoginStatus()
    {
        LoginStatus.text = AccountManager.Instance.Username ?? "Not logged in.";
    }

    public void OnLoginButton()
    {
        AccountManager.Instance.Login(success => {
            UpdateLoginStatus();
        });
    }

    public void OnLogoutButton()
    {
        AccountManager.Instance.Logout();
        UpdateLoginStatus();
    }

    public void OnStartRecordingButton()
    {
        if (_recorder != null)
        {
            _recorder.Cancel();
            _recorder = null;
        }

        _recorder = ViewRecorder.Create();
        _recorder.FrameRate = 30;
        _recorder.BufferDuration = 30;

        _recorder.OnFailure += (error) => {
            Debug.Log("Failed: " + error);
            _ShowToastMessage("Recording failed: " + error);
        };

        _recorder.Start();
    }

    public void OnStopRecordingButton()
    {
        if (_recorder == null)
        {
            return;
        }

        Debug.Log("Encoding video");
        _recorder.Finish(() => {
            Debug.Log("Video created: " + _recorder.OutputPath);

            _uploader = VideoUploader.Create();
            _uploader.VideoPath = _recorder.OutputPath;
            _uploader.Title = "A quick brown fox jumps over the lazy dog.";

            _uploader.OnGfycatRegistered += (gfyId) => {
                Debug.Log("Registered Gfycat: " + gfyId + ", " + _uploader.GfycatUrl);
                _ShowToastMessage("Registered Gfycat: " + gfyId);
            };

            _uploader.OnGfycatUploaded += (gfyId) => {
                Debug.Log("Uploaded Gfycat: " + gfyId + ", " + _uploader.GfycatUrl);
                _ShowToastMessage("Uploaded Gfycat: " + _uploader.GfycatUrl);
                Application.OpenURL(_uploader.GfycatUrl);
            };

            _uploader.OnFailure += (error) => {
                Debug.Log("Failed: " + error);
                _ShowToastMessage("Failed to upload Gfycat: " + error);
            };

            _uploader.Upload();
        });
    }

    private void _ShowToastMessage(string message) 
    {
#if UNITY_IOS
        //not implemented
#elif UNITY_ANDROID
        _ShowAndroidToastMessage(message);
#endif
    }

    private void _ShowAndroidToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
 
        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                toastObject.Call("show");
            }));
        }
    }
}
