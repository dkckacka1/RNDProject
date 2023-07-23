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

    private AssetBundle bundle;
    private Uri downloadURL;

    public void VideoPlay()
    {
        string path = Path.Combine(Application.streamingAssetsPath, bundleNameField.text);

        if (File.Exists(path))
        {
            resultText.text = "로컬 번들을 사용합니다.";
            bundle = AssetBundle.LoadFromFile(path);
        }
        else
        {
            resultText.text = "캐시된 번들을 사용합니다.";
        }


        if (bundle == null)
        {
            resultText.text = "번들이 존재 하지 않습니다. 번들을 다운로드 해주세요.";
            return;
        }

        VideoClip clip = bundle.LoadAsset<VideoClip>(videoNameField.text);

        if (clip == null)
        {
            resultText.text = "번들은 존재하되 번들안에 에셋이 존재하지 않습니다.";
            bundle.Unload(false);
            return;
        }


        resultText.text = "비디오 로드 완료!";
        videoPlayer.Stop();
        videoPlayer.clip = clip;
        StartCoroutine(PlayVideo());

        bundle.Unload(false);
    }

    private IEnumerator PlayVideo()
    {
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        videoPlayer.Play();
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
                    Debug.Log("Download URL: " + task.Result);
                    downloadURL = task.Result;
                    StartCoroutine(BundleDownLoad(downloadURL));
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
            resultText.text = "웹 연결 시도 혹은 캐시된 파일이 있는지 확인";
        });

        using (UnityWebRequest webRequest = UnityWebRequestAssetBundle.GetAssetBundle(result,0,0))
        {
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
                bundle = DownloadHandlerAssetBundle.GetContent(webRequest);
                FirebaseManager.Instance.messageQueue.Enqueue(() =>
                {
                    resultText.text = "웹 연결 성공 및 에셋번들 다운로드 및 캐싱 완료";
                });
                var videoList = bundle.LoadAllAssets<VideoClip>();
                foreach (var clip in videoList)
                {
                    Debug.Log(clip.name);
                }
            }
        }
    }
}
