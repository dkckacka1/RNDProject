using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Start.UI
{
    public class StartSceneUIManager : MonoBehaviour
    {
        public void GameStart()
        {
            StartCoroutine(MainSceneLoad());
        }

        IEnumerator MainSceneLoad()
        {
            // ���ӸŴ��� ������ �ε�
            // ���� ������ �ε�
            yield return null;
            SceneLoader.LoadMainScene();
        }
    }
}