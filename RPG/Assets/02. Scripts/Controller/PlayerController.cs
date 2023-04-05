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
            BattleManager.Instance.livePlayer = this;
        }

        public override void DeadEvent()
        {
            base.DeadEvent();
        }

        public override bool SetTarget(out Controller controller)
        {
            controller = BattleManager.Instance.ReturnNearDistanceController<EnemyController>(transform);
            if (controller != null)
            {
                this.target = controller;
                attack.SetTarget(controller.battleStatus);
            }

            return (controller != null);
        }
    }
}