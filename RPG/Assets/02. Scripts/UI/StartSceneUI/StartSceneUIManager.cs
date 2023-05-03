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
            GameManager.Instance.DataLoad();

            // ���� ������ �ε�
            GameManager.Instance.UserInfo = GameSLManager.LoadFromJson(Application.dataPath + @"\Userinfo.json");
            GameManager.Instance.Player.SetPlayerStatusFromUserinfo(GameManager.Instance.UserInfo);

            yield return null;
            SceneLoader.LoadMainScene();
        }
    }
}