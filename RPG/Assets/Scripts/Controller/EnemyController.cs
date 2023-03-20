using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;
using RPG.Battle.Fight;

namespace RPG.Battle.Control
{

    public class EnemyController : Controller
    {
        protected override void SetChaseState()
        {
            Target = BattleManager.GetInstance().ReturnNearDistanceController<PlayerController>(transform);

            base.SetChaseState();
        }

        protected override void Start()
        {
            base.Start();
            BattleManager.GetInstance().LiveEnemys.Add(this);
        }
    }
}
