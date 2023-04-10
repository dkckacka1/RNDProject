using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RPG.Battle.Ability
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
                AbilityCoolTimeImage.fillAmount -= Time.deltaTime / abilityCoolTime;
                currentCoolTime -= Time.deltaTime;
                AbilityCoolTimeText.text = currentCoolTime.ToString("N1");
                yield return null;
                if (AbilityCoolTimeImage.fillAmount <= 0)
                {
                    break;
                }
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