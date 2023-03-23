using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Battle.UI
{
    public class HPBar : MonoBehaviour
    {
        public Slider hpSlider;

        public virtual void SetHpSlider(int maxHp)
        {
            hpSlider.maxValue = maxHp;
            hpSlider.value= maxHp;
        }

        public virtual void ChangeCurrentHP(int currentHp)
        {
            hpSlider.value = currentHp;
        }
    }
}