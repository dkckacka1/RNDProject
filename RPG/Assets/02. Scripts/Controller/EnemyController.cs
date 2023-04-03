
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;
using RPG.Battle.Fight;
using RPG.Battle.AI;

namespace RPG.Battle.Control
{
    public class EnemyController : Controller
    {
        public GameObject enemyLooks;

        public override void SetUp()
        {
            base.SetUp();
        }

        public override void Init()
        {
            this.animator = GetComponentInChildren<Animator>();
            base.Init();
        }

        public override bool SetTarget(out Controller controller)
        {
            controller = BattleManager.GetInstance().ReturnNearDistanceController<PlayerController>(transform);
            if(controller != null)
            {
                this.target = controller;
                attack.SetTarget(controller.status);
            }

            return (controller != null);
        }

        public override void DeadEvent()
        {
            base.DeadEvent();
            BattleManager.GetInstance().DeadController(this);
        }
    }
}
