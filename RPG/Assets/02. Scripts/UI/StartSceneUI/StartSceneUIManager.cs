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
            // 게임매니저 데이터 로드
            // 유저 데이터 로드
            yield return null;
            SceneLoader.LoadMainScene();
        }
    }
}