using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Control;

namespace RPG.Battle.Ability
{
    public class ExplosionAbility : Ability
    {

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
