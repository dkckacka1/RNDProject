using RPG.Battle.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Battle.Skill
{
    public abstract class Ability : MonoBehaviour
    {
        public int abilityID;
        public string abilityName;
        [Space()]
        [TextArea()]
        public string abilityDesc;
        public float abilityTime;

        // 처음에 스킬이 어디서 나타날 것인가?
        public Vector3 abilityPositionOffset;
        public virtual void InitAbility(Transform startPos)
        {
            this.transform.position = startPos.position + abilityPositionOffset;
        }

        public void ReleaseAbility()
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