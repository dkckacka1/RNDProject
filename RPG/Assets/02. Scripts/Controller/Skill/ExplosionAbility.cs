using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Control;
using RPG.Battle.Core;

namespace RPG.Battle.Ability
{
    public class ExplosionAbility : Ability
    {
        public float explosionRange = 1f; // 폭발 반경


        protected override void OnEnable()
        {
            base.OnEnable();
            var list = CheckInsideExplosionController();
            foreach (var controller in list)
            {
                action.Invoke(controller.battleStatus);
            }
        }

        // 반경 내의 컨트롤러 리스트 리턴
        public List<Controller> CheckInsideExplosionController()
        {
            List<Controller> controllerList = new List<Controller>();

            if (BattleManager.Instance.livePlayer != null 
                && Vector3.Distance(this.transform.position, BattleManager.Instance.livePlayer.transform.position) < explosionRange)
            {
                controllerList.Add(BattleManager.Instance.livePlayer);
            }

            foreach (var enemy in BattleManager.Instance.liveEnemies)
            {
                if (BattleManager.Instance.liveEnemies != null
                    && Vector3.Distance(this.transform.position, enemy.transform.position) < explosionRange)
                {
                    controllerList.Add(enemy);
                }
            }

            return controllerList;
        }

        void OnDrawGizmosSelected()
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, explosionRange);
        }
    }
}
