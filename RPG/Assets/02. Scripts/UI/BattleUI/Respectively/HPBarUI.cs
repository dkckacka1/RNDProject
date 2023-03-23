using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Battle.UI
{
    public class HPBarUI : MonoBehaviour
    {
        public Slider hpSlider;

        public void SetHpSlider(int maxHp)
        {
            hpSlider.maxValue = maxHp;
            hpSlider.value= maxHp;
        }

        public void ChangeCurrentHP(int currentHp)
        {
            hpSlider.value = currentHp;
        }
    }
}