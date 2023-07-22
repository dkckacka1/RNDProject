using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateAssetBundle : MonoBehaviour
{
    // �� �÷��� �� ��Ʈ���� ������ ��ȯ�ϴ� ��ġ�� �ٸ��⿡ ���Ǻ� �����Ϸ� ����
#if UNITY_EDITOR || PLATFORM_STANDALONE_WIN
    public static readonly string bundlePath = Application.dataPath + "/StreamingAssets";
#elif UNITY_IOS
    public static readonly string bundlePath = Application.dataPath + "/Resources/Data/StreamingAssets";
#elif UNITY_ANDROID
    public static readonly string bundlePath = "jar:file://" + Application.dataPath + "!/assets";
#endif

    [UnityEditor.MenuItem("Assets/Build All Asset Bundle")]
    public static void BuildAllAssetBundles()
    {
        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(bundlePath);
        }

        BuildPipeline.BuildAssetBundles(bundlePath, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
    }
}
