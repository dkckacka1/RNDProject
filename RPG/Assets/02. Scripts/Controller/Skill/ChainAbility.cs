using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RPG.Battle.Core;
using RPG.Battle.Control;
using RPG.Character.Status;

namespace RPG.Battle.Ability
{
    public class ChainAbility : Ability
    {
        [SerializeField] float chainDelay = 0.1f; // √º¿Œ µÙ∑π¿Ã
        [SerializeField] float chainRange = 1f; // √º¿Œ π›∞Ê
        [SerializeField] int ChainCount = 3; // ≈∏∞Ÿ¿« ∞πºˆ

        List<EnemyController> targetList = new List<EnemyController>();

        public override void InitAbility(Transform startPos, UnityAction<BattleStatus> action)
        {
            base.InitAbility(startPos, action);
        }

        public override void ReleaseAbility()
        {
            base.ReleaseAbility();
        }

        public void SetTarget(EnemyController target)
        {
            var currentTarget = target;
            targetList.Add(currentTarget);
            for (int i = 0; i < ChainCount; i++)
            {
                EnemyController nextTarget;
                if (TryCheckNearlyTarget(currentTarget, out nextTarget))
                {
                    targetList.Add(nextTarget);
                    currentTarget = nextTarget;
                }
                else
                {
                    return;
                }

            }
        }

        public bool TryCheckNearlyTarget(EnemyController target, out EnemyController nextTarget)
        {
            BattleManager.Instance.liveEnemies.Sort((enemy1, enemy2) =>
            {
                float distance1 = Vector3.Distance(enemy1.transform.position, target.transform.position);
                float distance2 = Vector3.Distance(enemy2.transform.position, target.transform.position);

                if (distance1 > distance2)
                    return -1;
                else
                    return 1;
            });

            foreach (var enemycontroller in BattleManager.Instance.liveEnemies)
            {
                if (targetList.Find(enemy => enemycontroller == enemy) == null)
                {
                    nextTarget = enemycontroller;
                    return true;
                }
            }

            nextTarget = null;
            return false;
        }

        public IEnumerator delayCoroutine()
        {
            foreach (var enemy in targetList)
            {
                Debug.Log(enemy.name);
            }

            foreach (var target in targetList)
            {
                var newEffect = BattleManager.ObjectPool.GetAbility(this.abilityID, target.transform, action);
                newEffect.transform.position = target.transform.position;
                action.Invoke(target.battleStatus);
                yield return new WaitForSeconds(chainDelay);
            }
        }
    }
}
