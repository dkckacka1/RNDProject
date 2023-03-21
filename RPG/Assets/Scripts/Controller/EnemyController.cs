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
        protected override void Awake()
        {
            base.Awake();
            idelState = new IdelState();
            chaseState = new ChaseState();
            attackState = new AttackState();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public void SetAnimator(Animator animator)
        {
            this.animator = animator;
        }

        protected override void SetChaseState()
        {
            target = BattleManager.GetInstance().ReturnNearDistanceController<PlayerController>(transform);

            base.SetChaseState();
        }

        protected override void Start()
        {
            base.Start();
            BattleManager.GetInstance().LiveEnemys.Add(this);
        }

        public override void DeadAction()
        {
            base.DeadAction();
        }

        public override void AttactAction()
        {
            base.AttactAction();
        }
    }
}
