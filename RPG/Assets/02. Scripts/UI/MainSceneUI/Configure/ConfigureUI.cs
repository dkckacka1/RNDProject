using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using TMPro;

namespace RPG.Main.UI
{
    public class ConfigureUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI musicVolumeValueTxt;
        [SerializeField] TextMeshProUGUI soundVolumeValueTxt;

        public void ChangeMusicVolume(float value)
        {

        }

        public void ChangeSoundVoume(float value)
        {

        }

        public void GameExit()
        {
            GameSLManager.SaveToJSON(GameManager.Instance.UserInfo, Application.dataPath + @"\Userinfo.json");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}