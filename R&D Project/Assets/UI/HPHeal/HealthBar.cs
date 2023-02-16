using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI.HPHeal
{
    class HealthBar : MonoBehaviour
    {
        [SerializeField] Slider slider;
        [SerializeField] Text text;
        [SerializeField] int changedDelay = 60;

        bool isChanging = false;
        Coroutine changedCorotine;

        public void ChangedValue(float maxHp, float currentHp)
        {
            float preValue = slider.value;
            float nextValue = currentHp / maxHp;
            if(isChanging)
            {
                StopCoroutine(changedCorotine);
                changedCorotine = StartCoroutine(ChangeValue(preValue,nextValue));
            }
            else
            {
                changedCorotine = StartCoroutine(ChangeValue(preValue,nextValue));
            }

            text.text = string.Format("{0} / {1}", currentHp, maxHp);
        }

        public IEnumerator ChangeValue(float preValue, float nextValue)
        {
            float changeValue = (nextValue - preValue) / (float)changedDelay;
            isChanging = true;
            for (int i = 0; i < changedDelay; i++)
            {
                slider.value += changeValue;
                yield return new WaitForSeconds(1 / (float)changedDelay);
            }
            isChanging = false;
        }
    }
}
