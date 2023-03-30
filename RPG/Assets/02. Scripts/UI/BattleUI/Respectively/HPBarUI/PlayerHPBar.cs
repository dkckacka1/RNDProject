using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG.Battle.UI
{
    public class PlayerHPBar : HPBar
    {
        public TextMeshProUGUI hpText;

        private int maxHp;

        public override void ChangeCurrentHP(int currentHp)
        {
            base.ChangeCurrentHP(currentHp);
            hpText.text = $"{currentHp}  /  {maxHp}";
        }

        public override void SetHpSlider(int maxHp)
        {
            base.SetHpSlider(maxHp);
            this.maxHp = maxHp;
            hpText.text = $"{maxHp}  /  {maxHp}";
        }
    } 
}