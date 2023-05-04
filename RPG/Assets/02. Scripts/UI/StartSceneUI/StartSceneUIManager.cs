using RPG.Core;
using RPG.Main.Audio;
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
            if (GameSLManager.isSaveFileExist())
            {
                GameManager.Instance.UserInfo = GameSLManager.LoadFromJson();
            }
            else
            {
                GameManager.Instance.UserInfo = GameManager.Instance.CreateUserInfo();
            }

            GameManager.Instance.Player.SetPlayerStatusFromUserinfo(GameManager.Instance.UserInfo);

            // ���� ������ �ε�
            GameManager.Instance.configureData = GameSLManager.LoadConfigureData();

            // ���� ������ ����
            AudioManager.Instance.ChangeMusicVolume(GameManager.Instance.configureData.musicVolume);
            AudioManager.Instance.ChangeSoundVolume(GameManager.Instance.configureData.soundVolume);

            yield return null;
            SceneLoader.LoadMainScene();
        }
    }
}