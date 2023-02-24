using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Fight;
using RPG.Character;

namespace RPG.Control
{

    public class EnemyController : Controller
    {
        public override void DeadAction()
        {
            base.DeadAction();
            BattleManager.GetInstance().DeadController(this);
        }

        public override void FindNextTarget()
        {
            Target = BattleManager.GetInstance().ReturnNearDistanceController<PlayerController>(transform);

            base.FindNextTarget();
        }

        protected override void Start()
        {
            base.Start();
            BattleManager.GetInstance().LiveEnemys.Add(this);
        }
    }
}
