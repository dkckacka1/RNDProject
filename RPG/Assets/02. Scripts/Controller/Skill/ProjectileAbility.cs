using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RPG.Battle.Control;
using RPG.Character.Status;

namespace RPG.Battle.Ability
{
    public class ProjectileAbility : Ability
    {
        [SerializeField] float speed;

        public override void InitAbility(Transform startPos, UnityAction<BattleStatus> action, Space space = Space.Self)
        {
            this.transform.rotation = startPos.rotation;
            base.InitAbility(startPos, action, space);
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            var enemyController = other.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                var enemyStatus = enemyController.battleStatus;
                if (enemyStatus != null)
                {
                    action.Invoke(enemyStatus);
                }
            }

        }
    }

}