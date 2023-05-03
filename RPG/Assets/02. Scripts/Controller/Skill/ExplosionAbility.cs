using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Control;
using RPG.Battle.Core;

namespace RPG.Battle.Ability
{
    public class ExplosionAbility : Ability
    {
        public float explosionRange = 1f; // ���� �ݰ�
        List<Controller> inExplosionController = new List<Controller>();


        protected override void OnEnable()
        {
            base.OnEnable();
            var list = CheckInsideExplosionController();

            if (hitAction != null)
            {
                foreach (var controller in list)
                {
                    hitAction.Invoke(controller.battleStatus);
                }
            }

            if (chainAction != null)
            {
                foreach (var controller in list)
                {
                    chainAction.Invoke(controller.battleStatus);
                }
            }

            foreach (var controller in inExplosionController)
            {
                Debug.Log(controller.name);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            inExplosionController.Add(other.GetComponent<EnemyController>());
        }

        // �ݰ� ���� ��Ʈ�ѷ� ����Ʈ ����
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
    }
}
