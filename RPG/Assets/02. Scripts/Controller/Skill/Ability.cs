using RPG.Battle.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        // ó���� ��ų�� ��� ��Ÿ�� ���ΰ�?
        public Vector3 abilityPositionOffset;
        public virtual void InitAbility(Transform startPos)
        {
            this.transform.localPosition = startPos.localPosition;
            this.transform.Translate(abilityPositionOffset);
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