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
        private void Start()
        {
            BattleManager.GetInstance().livePlayers.Add(this);
        }

        protected override void FindNextTarget()
        {
            base.FindNextTarget();
            print("�÷��̾���Ʈ�ѷ�");
            BattleManager.GetInstance().MoveToNextPhase<PlayerController>(this);
            this.combatStats = CombatStats.CHASESTART;
        }
    }
}