using RPG.Battle.Core;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Battle.UI
{
    public class PauseUI : MonoBehaviour
    {
        public void Init()
        {
            // 게임매니저의 오디오 매니저에 접속
            this.transform.parent.gameObject.SetActive(true);
        }

        public void ChangeMusicVolum()
        {

        }

        public void ChangeSoundVolum()
        {

        }

        public void Release()
        {
            this.transform.parent.gameObject.SetActive(false);
        }

        public void ReturnBattle()
        {
            BattleManager.Instance.ReturnBattle();
        }

        public void StopBattle()
        {
            SceneLoader.LoadMainScene();
        }
    }
}