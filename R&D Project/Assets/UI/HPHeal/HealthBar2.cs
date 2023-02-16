using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI.HPHeal
{
    class HealthBar2 : MonoBehaviour
    {
        public Slider hpSlider, hpEffectSlider;
        public Text text;
        public float effectDelay = 0.5f;
        public float effectLerp = 5f;

        bool isChanging = false;
        Coroutine ChangeCoroutin;

        public void UpdateMaxValue(float maxHp)
        {
            hpSlider.maxValue = maxHp;
            hpEffectSlider.maxValue = maxHp;
            hpEffectSlider.value = hpSlider.value;
        }

        public void UpdateValue(float currentHp, float maxhp)
        {
            text.text = string.Format("{0} / {1}", currentHp, maxhp);
            if (currentHp > hpSlider.value)
            {
                hpSlider.value = currentHp;
                if(isChanging)
                {
                    StopCoroutine(ChangeCoroutin);
                }
                hpEffectSlider.value = currentHp;
            }
            else
            {
                // 감소중
                hpSlider.value = currentHp;
                if (!isChanging)
                {
                    isChanging = true;
                    ChangeCoroutin = StartCoroutine(ChangeValue(hpSlider.value));
                }
                else
                {
                    StopCoroutine(ChangeCoroutin);
                    ChangeCoroutin = StartCoroutine(ChangeValue(hpSlider.value));
                }
            }
        }

        IEnumerator ChangeValue(float nextValue)
        {
            yield return new WaitForSecondsRealtime(effectDelay);

            while (Mathf.Abs(hpEffectSlider.value - nextValue) > 0.1f)
            {
                hpEffectSlider.value = Mathf.Lerp(hpEffectSlider.value, nextValue, Time.deltaTime * effectLerp);
                yield return null;
            }
            isChanging = false;
        }
    }
}
