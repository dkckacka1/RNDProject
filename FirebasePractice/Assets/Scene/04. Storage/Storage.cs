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

    // ���̾�̽� ���丮�� ���ε� �� �ٿ�ε� ����
    public StorageReference storageReference => FirebaseManager.Instance.firebaseStorage.RootReference;

    private AssetBundle bundle;
    private Uri downloadURL;

    public void VideoPlay()
    {
        string path = Path.Combine(Application.streamingAssetsPath, bundleNameField.text);

        if (File.Exists(path))
        {
            resultText.text = "���� ������ ����մϴ�.";
            bundle = AssetBundle.LoadFromFile(path);
        }
        else
        {
            resultText.text = "ĳ�õ� ������ ����մϴ�.";
        }


        if (bundle == null)
        {
            resultText.text = "������ ���� ���� �ʽ��ϴ�. ������ �ٿ�ε� ���ּ���.";
            return;
        }

        VideoClip clip = bundle.LoadAsset<VideoClip>(videoNameField.text);

        if (clip == null)
        {
            resultText.text = "������ �����ϵ� ����ȿ� ������ �������� �ʽ��ϴ�.";
            bundle.Unload(false);
            return;
        }


        resultText.text = "���� �ε� �Ϸ�!";
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
            resultText.text = "�� ���� �õ� Ȥ�� ĳ�õ� ������ �ִ��� Ȯ��";
        });

        using (UnityWebRequest webRequest = UnityWebRequestAssetBundle.GetAssetBundle(result,0,0))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                FirebaseManager.Instance.messageQueue.Enqueue(() =>
                {
                    resultText.text = "�� ���� ����";
                });
            }
            else
            {
                bundle = DownloadHandlerAssetBundle.GetContent(webRequest);
                FirebaseManager.Instance.messageQueue.Enqueue(() =>
                {
                    resultText.text = "�� ���� ���� �� ���¹��� �ٿ�ε� �� ĳ�� �Ϸ�";
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
