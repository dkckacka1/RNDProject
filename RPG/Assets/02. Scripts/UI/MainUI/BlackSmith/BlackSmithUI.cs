using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using RPG.Core;

namespace RPG.Main.UI
{
    public class BlackSmithUI : MonoBehaviour
    {
        [SerializeField] ItemPopupUI popupUI;
        [SerializeField] ItemChoiceUI choiceUI;

        [SerializeField] float scaleAnimDuration = 1f;

        [SerializeField] TextMeshProUGUI remainIncantText;
        [SerializeField] TextMeshProUGUI remainReinfoceText;
        [SerializeField] TextMeshProUGUI remainGachaText;

        private void OnEnable()
        {
            choiceUI.InitButtonImage();
            choiceUI.ChoiceWeapon(popupUI);
            InitRemainText();

            transform.DOScale(Vector3.one, scaleAnimDuration).SetEase(Ease.InOutBounce); 
        }

        private void OnDisable()
        {
            transform.localScale = Vector3.zero;
        }

        public void InitRemainText()
        {
            remainIncantText.text = GameManager.Instance.UserInfo.itemIncantTicket.ToString();
            remainReinfoceText.text = GameManager.Instance.UserInfo.itemReinforceTicket.ToString();
            remainGachaText.text = GameManager.Instance.UserInfo.itemGachaTicket.ToString();
        }
    } 
}
