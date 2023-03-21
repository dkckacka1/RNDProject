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
        protected override void Awake()
        {
            base.Awake();
            idelState = new IdelState();
            chaseState = new ChaseState();
            attackState = new AttackState();
        }

        protected override void Start()
        {
            base.Start();
            BattleManager.GetInstance().LivePlayers.Add(this);
        }

        protected override void SetChaseState()
        {
            target = BattleManager.GetInstance().ReturnNearDistanceController<EnemyController>(transform);

            base.SetChaseState();
        }

        public override void DeadAction()
        {
            base.DeadAction();
            BattleManager.GetInstance().DeadController(this);
        }

        protected override void SetAttackState()
        {
            base.SetAttackState();
        }

        public override void AttactAction()
        {
            base.AttactAction();
        }
    }
}