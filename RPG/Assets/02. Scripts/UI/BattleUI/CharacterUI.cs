using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;
using RPG.Character.Status;

namespace RPG.Battle.UI
{
    public abstract class CharacterUI : MonoBehaviour
    {
        public Canvas battleCanvas;

        public HPBarUI hpBar;

        public virtual void Initialize(Status status)
        {
            battleCanvas = BattleManager.GetInstance().battleCanvas;
        }

        public virtual void RemoveUI()
        {
            if (hpBar != null)
            {
                hpBar.gameObject.SetActive(false);
            }
        }

        public virtual void RemoveUI(float time)
        {
            StartCoroutine(Remove(time));
        }

        public IEnumerator Remove(float time)
        {
            yield return new WaitForSeconds(time);

            RemoveUI();
        }

        public void InitHPUI(int maxHP)
        {
            if(hpBar != null)
            {
                hpBar.SetHpSlider(maxHP);
            }
        }

        public void ChangeHPUI(int currentHP)
        {
            if(hpBar != null)
            {
                hpBar.ChangeCurrentHP(currentHP);
            }
        }

    }
}