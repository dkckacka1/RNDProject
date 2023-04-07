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
        public BattleStatus status;

        [Header("HPUI")]
        public HPBar hpBar;

        [Header("BattleText")]
        public Vector3 battleTextOffset;

        private void Awake()
        {
            SetUp();
        }


        public virtual void SetUp()
        {
            this.status = GetComponent<BattleStatus>();
            battleCanvas = BattleManager.BattleUI.battleCanvas;
        }

        public virtual void Init()
        {
            if (hpBar != null)
            {
                hpBar.gameObject.SetActive(true);
                hpBar.InitHpSlider(status.status.maxHp);
            }
        }

        public virtual void ReleaseUI()
        {
            if (hpBar != null)
            {
                hpBar.gameObject.SetActive(false);
            }
        }

        public void UpdateHPUI(int currentHP)
        {
            if(hpBar != null)
            {
                hpBar.ChangeCurrentHP(currentHP);
            }
        }

        public void TakeDamageText(string damage, DamagedType type = DamagedType.Normal)
        {
            BattleManager.ObjectPool.GetText(damage.ToString(), this.transform.position + battleTextOffset, type);
        }
    }
}