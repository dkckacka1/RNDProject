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