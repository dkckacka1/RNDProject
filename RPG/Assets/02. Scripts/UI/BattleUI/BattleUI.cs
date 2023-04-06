using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Battle.Core;

namespace RPG.Battle.UI
{
    public class BattleUI : MonoBehaviour
    {
        public Canvas battleCanvas;

        [Header("ReadyUI")]
        [SerializeField] TextMeshProUGUI readyText;
        [SerializeField] float removeReadyUITime = 1f;

        [Header("looting")]
        public Image backpack;

        [Header("PlayerUI")]
        // PlayerUI
        public PlayerHPBar playerHPBar;

        [Header("BattleResultUI")]
        public BattleResultWindow resultUI;

        public void ShowReady()
        {
            readyText.text = "준비";
            readyText.gameObject.SetActive(true);
        }

        public void ShowStart()
        {
            readyText.text = "시작";
            StartCoroutine(RemoveReadyUI(removeReadyUITime));
        }

        public void ShowWinText()
        {
            readyText.text = "승리!";
            readyText.gameObject.SetActive(true);

        }

        public void ShowDefeatText()
        {
            readyText.text = "패배~";
            readyText.gameObject.SetActive(true);

        }

        public void ShowResultUI()
        {
            resultUI.gameObject.SetActive(true);
        }

        private IEnumerator RemoveReadyUI(float duration)
        {
            yield return new WaitForSeconds(duration);
            readyText.gameObject.SetActive(false);
        }


    }
}
