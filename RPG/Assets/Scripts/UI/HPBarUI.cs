using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Battle.UI
{
    public class HPBarUI : MonoBehaviour
    {
        public Slider hpSlider;

        public void SetHpSlider(float maxHp, float currentHp)
        {
            hpSlider.maxValue = maxHp;
            hpSlider.value= currentHp;
        }

        public void SetHpSlider(float currentHp)
        {
            hpSlider.value = currentHp;
        }
    }
}