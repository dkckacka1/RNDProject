using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;
using RPG.Battle.Skill;
using RPG.Core;

namespace RPG.Character.Equipment
{
    public class Stone_Weapon : WeaponIncant
    {
        public Stone_Weapon()
        {
            incantType = IncantType.suffix;
            itemType = EquipmentItemType.Weapon;
            name = "µπµ¢¿Ã";
            addDesc = "∞¯∞› Ω√ µπµ¢¿Ã ≈ı√¥";
            minusDesc = "";
            isIncantSkill = true;
            skillID = 1;
        }

        public override void IncantEquipment(Equipment equipment)
        {
        }

        public override void RemoveIncant(Equipment equipment)
        {
        }

        public override void Skill(BattleStatus player, BattleStatus enemy)
        {
            Skill skill = GameManager.Instance.SkillPrefabDic[skillID];
            Object.Instantiate(skill,player.transform.position,player.transform.rotation);
        }
    }
}