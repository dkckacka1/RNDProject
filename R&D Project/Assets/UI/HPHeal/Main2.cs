using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.UI.HPHeal
{

    class Main2 : MonoBehaviour
    {
        private float currentHp;
        public float maxhp;
        public HealthBar2 healthBar;
        [SerializeField] int dameged;
        [SerializeField] int healed;

        public float CurrentHp 
        { 
            get => currentHp;
            set
            {
                currentHp = Mathf.Clamp(value, 0, maxhp);
                healthBar.UpdateValue(currentHp, maxhp);
            }
        }

        private void Start()
        {
            healthBar.UpdateMaxValue(maxhp);
            CurrentHp = 50;
        }

        public void TakeDamaged()
        {
            CurrentHp -= dameged;
        }

        public void Heal()
        {
            CurrentHp += healed;
        }
    }
}
