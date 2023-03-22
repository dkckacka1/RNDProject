using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;
using RPG.Battle.AI;
using RPG.Battle.Fight;
using RPG.Battle.Move;
using RPG.Character.Status;

namespace RPG.Battle.Control
{
    public class PlayerController : Controller
    {
        public override void Initialize()
        {
            base.Initialize();
            BattleManager.GetInstance().LivePlayers.Add(this);
        }

        public override void DeadEvent()
        {
            base.DeadEvent();
            BattleManager.GetInstance().DeadController(this);
        }

        public override bool SetTarget(out Controller controller)
        {
            controller = BattleManager.GetInstance().ReturnNearDistanceController<EnemyController>(transform);

            return (controller != null);
        }
    }
}