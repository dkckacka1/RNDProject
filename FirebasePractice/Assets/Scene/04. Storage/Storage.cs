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

    public void VideoPlay()
    {
        AssetBundle localAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, bundleNameField.text));

        if (localAssetBundle == null)
        {
            resultText.text = "������ ���� ���� �ʽ��ϴ�. ������ �ٿ�ε� ���ּ���.";
            return;
        }

        VideoClip clip = localAssetBundle.LoadAsset<VideoClip>(videoNameField.text);

        if (clip == null)
        {
            resultText.text = "������ �����ϵ� ����ȿ� ������ �������� �ʽ��ϴ�.";
            localAssetBundle.Unload(false);
            return;
        }

        resultText.text = "���� �ε� �Ϸ�!";
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
            resultText.text = "�� ���� �õ�";
        });
        WWW.LoadFromCacheOrDownload(result.AbsoluteUri, 0);
        UnityWebRequest webRequest = UnityWebRequestAssetBundle.GetAssetBundle(result);
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
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(webRequest);
            FirebaseManager.Instance.messageQueue.Enqueue(() =>
            {
                resultText.text = "�� ���� ���� �� ���¹��� �ٿ�ε� �Ϸ�";
            });
            var videoList = bundle.LoadAllAssets<VideoClip>();
            foreach (var clip in videoList)
            {
                Debug.Log(clip.name);
            }
        }
    }
}
