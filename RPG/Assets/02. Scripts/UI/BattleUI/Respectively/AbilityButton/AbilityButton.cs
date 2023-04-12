using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Battle.Core;

namespace RPG.Battle.UI
{
    public class AbilityButton : MonoBehaviour
    {
        public float abilityCoolTime = 10f;
        public float currentCoolTime;

        public Button AbilityBtn;
        [SerializeField] Image AbilityIconImage;
        [SerializeField] TextMeshProUGUI AbilityCoolTimeText;
        [SerializeField] Image AbilityCoolTimeImage;

        [SerializeField] bool canUse;

        public void Init(Sprite sprite, float coolTime)
        {
            AbilityIconImage.sprite = sprite;
            abilityCoolTime = coolTime;
        }

        public void UseAbility()
        {
            if (canUse)
            {
                StartCoroutine(CheckCoolTime());
            }
        }

        public IEnumerator CheckCoolTime()
        {
            SetCool();
            while (true)
            {
                if (BattleManager.Instance.currentBattleState == BattleSceneState.Battle)
                {
                    AbilityCoolTimeImage.fillAmount -= Time.deltaTime / abilityCoolTime;
                    currentCoolTime -= Time.deltaTime;
                    AbilityCoolTimeText.text = currentCoolTime.ToString("N1");
                    if (AbilityCoolTimeImage.fillAmount <= 0)
                    {
                        break;
                    }
                }
                yield return null;
            }
            CanSkill();
        }

        public void CanSkill()
        {
            canUse = true;
            AbilityCoolTimeImage.gameObject.SetActive(false);
            AbilityCoolTimeText.gameObject.SetActive(false);
            AbilityBtn.interactable = true;
        }

        private void SetCool()
        {
            canUse = false;
            AbilityCoolTimeImage.fillAmount = 1;
            currentCoolTime = abilityCoolTime;
            AbilityCoolTimeImage.gameObject.SetActive(true);
            AbilityCoolTimeText.gameObject.SetActive(true);
            AbilityBtn.interactable = false;
        }
    }
}