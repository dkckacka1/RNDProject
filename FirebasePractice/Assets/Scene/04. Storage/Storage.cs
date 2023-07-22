using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Storage;
using Firebase.Extensions;
using UnityEngine.UI;
using UnityEngine.Video;
using System;
using UnityEngine.Networking;
using System.IO;

public class Storage : MonoBehaviour
{
    [SerializeField] InputField bundleNameField;
    [SerializeField] InputField videoNameField;
    [SerializeField] Text resultText;
    [SerializeField] VideoPlayer videoPlayer;

    // 파이어베이스 스토리지 업로드 및 다운로드 참조
    public StorageReference storageReference => FirebaseManager.Instance.firebaseStorage.RootReference;

    public void VideoPlay()
    {
        AssetBundle localAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, bundleNameField.text));

        if (localAssetBundle == null)
        {
            resultText.text = "번들이 존재 하지 않습니다. 번들을 다운로드 해주세요.";
            return;
        }

        VideoClip clip = localAssetBundle.LoadAsset<VideoClip>(videoNameField.text);

        if (clip == null)
        {
            resultText.text = "번들은 존재하되 번들안에 에셋이 존재하지 않습니다.";
            localAssetBundle.Unload(false);
            return;
        }

        resultText.text = "비디오 로드 완료!";
        videoPlayer.clip = clip;
        videoPlayer.Play();

        localAssetBundle.Unload(false);
    }

    public void UploadFile()
    {

    }

    public void DownloadFile()
    {
        StorageReference videosBundleURLpath = FirebaseManager.Instance.firebaseStorage.GetReferenceFromUrl("gs://testproject-c9f8b.appspot.com/VideoBundle/" + bundleNameField.text);

        videosBundleURLpath.GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
        {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                FirebaseManager.Instance.messageQueue.Enqueue(() =>
                {
                    resultText.text = "Download URL: " + task.Result;
                    StartCoroutine(BundleDownLoad(task.Result));
                });
            }
            else
            {
                FirebaseManager.Instance.messageQueue.Enqueue(() =>
                {
                    resultText.text = "Download URL Error";
                });
            }
        });
    }

    private IEnumerator BundleDownLoad(Uri result)
    {
        FirebaseManager.Instance.messageQueue.Enqueue(() =>
        {
            resultText.text = "웹 연결 시도";
        });
        WWW.LoadFromCacheOrDownload(result.AbsoluteUri, 0);
        UnityWebRequest webRequest = UnityWebRequestAssetBundle.GetAssetBundle(result);
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            FirebaseManager.Instance.messageQueue.Enqueue(() =>
            {
                resultText.text = "웹 연결 실패";
            });
        }
        else
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(webRequest);
            FirebaseManager.Instance.messageQueue.Enqueue(() =>
            {
                resultText.text = "웹 연결 성공 및 에셋번들 다운로드 완료";
            });
            var videoList = bundle.LoadAllAssets<VideoClip>();
            foreach (var clip in videoList)
            {
                Debug.Log(clip.name);
            }
        }
    }
}
