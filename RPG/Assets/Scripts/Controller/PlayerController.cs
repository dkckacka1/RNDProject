using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Fight;
using RPG.Move;
using RPG.Character;

namespace RPG.Control
{
    public class PlayerController : Controller
    {
        protected override void Start()
        {
            base.Start();
            BattleManager.GetInstance().LivePlayers.Add(this);
        }

        public override void FindNextTarget()
        {
            Target = BattleManager.GetInstance().ReturnNearDistanceController<EnemyController>(transform);

            base.FindNextTarget();
        }

        public override void DeadAction()
        {
            base.DeadAction();
            BattleManager.GetInstance().DeadController(this);
        }
    }
}