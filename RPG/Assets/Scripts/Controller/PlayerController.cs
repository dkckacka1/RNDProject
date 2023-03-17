using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;
using RPG.Battle.Fight;
using RPG.Battle.Move;
using RPG.Character.Status;

namespace RPG.Battle.Control
{
    public class PlayerController : Controller
    {
        protected override void Start()
        {
            base.Start();
            BattleManager.GetInstance().LivePlayers.Add(this);
        }

        protected override void SetChaseState()
        {
            Target = BattleManager.GetInstance().ReturnNearDistanceController<EnemyController>(transform);

            base.SetChaseState();
        }

        public override void DeadAction()
        {
            base.DeadAction();
            BattleManager.GetInstance().DeadController(this);
        }
    }
}