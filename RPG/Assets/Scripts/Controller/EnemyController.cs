using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;
using RPG.Battle.Fight;
using RPG.Battle.Character;

namespace RPG.Battle.Control
{

    public class EnemyController : Controller
    {
        public override void DeadAction()
        {
            base.DeadAction();
            BattleManager.GetInstance().DeadController(this);
        }

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
