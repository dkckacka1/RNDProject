using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;
using RPG.Battle.AI;
using RPG.Battle.Fight;
using RPG.Battle.Move;
using RPG.Character.Status;
using RPG.Character.Equipment;
using UnityEngine.Events;

namespace RPG.Battle.Control
{
    public class PlayerController : Controller
    {
        public override void SetUp()
        {
            base.SetUp();
            AddAttackEvent();
            BattleManager.Instance.livePlayer = this;
        }

        public void AddAttackEvent()
        {
            PlayerStatus status = (battleStatus.status as PlayerStatus);

            if (status.currentWeapon.prefix != null)
            {
                attack.AddAction((status.currentWeapon.prefix as WeaponIncant).Skill);
            }

            if (status.currentWeapon.suffix != null)
            {
                attack.AddAction((status.currentWeapon.suffix as WeaponIncant).Skill);
            }
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