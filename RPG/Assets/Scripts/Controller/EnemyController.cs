using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;
using RPG.Battle.Fight;

namespace RPG.Battle.Control
{

    public class EnemyController : Controller
    {
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
