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
            BattleManager.GetInstance().livePlayers.Add(this);
        }

        public override void FindNextTarget()
        {
            base.FindNextTarget();
            print("플레이어컨트롤러");
            target = BattleManager.GetInstance().ReturnNearDistanceController<EnemyController>(transform);
        }
    }
}