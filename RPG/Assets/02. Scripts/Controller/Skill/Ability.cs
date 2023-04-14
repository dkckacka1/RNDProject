using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RPG.Battle.Core;
using RPG.Character.Status;

namespace RPG.Battle.Ability
{
    public abstract class Ability : MonoBehaviour
    {
        public int abilityID;
        public string abilityName;
        [Space()]
        [TextArea()]
        public string abilityDesc;
        public float abilityTime;

        protected UnityAction<BattleStatus> action;
        private void OnEnable()
        {
            StartCoroutine(ReleaseTimer());
        }

        // ó���� ��ų�� ��� ��Ÿ�� ���ΰ�?
        public Vector3 abilityPositionOffset;
        public virtual void InitAbility(Transform startPos, UnityAction<BattleStatus> action)
        {
            this.transform.localPosition = startPos.localPosition;
            this.transform.Translate(abilityPositionOffset);
            this.action = action;
        }

        public virtual void ReleaseAbility()
        {
            BattleManager.ObjectPool.ReturnAbility(this);
        }

        public IEnumerator ReleaseTimer()
        {
            yield return new WaitForSeconds(abilityTime);
            ReleaseAbility();
        }
    }
}