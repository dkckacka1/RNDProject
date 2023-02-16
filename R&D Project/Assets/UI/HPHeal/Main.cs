using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI.HPHeal
{
    class Main : MonoBehaviour
    {
        [SerializeField] HealthBar healthBar;
        [SerializeField] int maxHp;
        [SerializeField] int dameged;
        [SerializeField] int healed;
        private int currentHp;

        public int CurrentHp 
        { 
            get => currentHp; 
            set
            {
                Debug.Log(value);
                value = Mathf.Clamp(value ,0, maxHp);
                currentHp = value;
                healthBar.ChangedValue(maxHp,CurrentHp); ;
            }
        }

        private void Start()
        {
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
