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
        public override void SetUp()
        {
            base.SetUp();
            BattleManager.GetInstance().player = this;
        }

        public override void DeadEvent()
        {
            base.DeadEvent();
            BattleManager.GetInstance().DeadController(this);
        }

        public override bool SetTarget(out Controller controller)
        {
            controller = BattleManager.GetInstance().ReturnNearDistanceController<EnemyController>(transform);
            if (controller != null)
            {
                this.target = controller;
                attack.SetTarget(controller.status);
            }

            return (controller != null);
        }
    }
}