using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Assets.UI.SkillCoolTime
{
    class SkillCoolTimeSystem : MonoBehaviour
    {
        [SerializeField] float coolTime;
        [SerializeField] float textDelay;
        [SerializeField] Text skillCoolTimeText;
        [SerializeField] Image skillCooltimeImage;
        [SerializeField] Button skillButton;
        [SerializeField] UnityEvent cooltimeEndEvents;

        public void UseSkill()
        {
            skillButton.interactable = false;
            skillCooltimeImage.fillAmount = 1;
            skillCoolTimeText.gameObject.SetActive(true);
            StartCoroutine(SkillCoolTimeCoroutine());
            StartCoroutine(SkillCoolTimeTextCoroutine(coolTime));
        }


        IEnumerator SkillCoolTimeCoroutine()
        {
            while (skillCooltimeImage.fillAmount > 0)
            {
                skillCooltimeImage.fillAmount -= 1 * Time.smoothDeltaTime / coolTime;
                yield return null;
            }

            skillButton.interactable = true;
            cooltimeEndEvents.Invoke();
        }

        IEnumerator SkillCoolTimeTextCoroutine(float number)
        {
            if (number > textDelay)
            {
                number -= 0.1f;
                skillCoolTimeText.text = number.ToString("N1");

                yield return new WaitForSeconds(0.1f);
                StartCoroutine(SkillCoolTimeTextCoroutine(number));
            }
            else
            {
                skillCoolTimeText.gameObject.SetActive(false);
            }
        }
    }
}
