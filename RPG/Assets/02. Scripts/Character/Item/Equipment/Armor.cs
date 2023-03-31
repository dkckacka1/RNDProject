using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Armor : Equipment
    {
        public int hpPoint;
        public int defencePoint;
        public float movementSpeed;
        public float evasionPoint;

        public Armor(Armor armor) : base(armor)
        {
            defencePoint = armor.defencePoint;
            hpPoint = armor.hpPoint;
            movementSpeed = armor.movementSpeed;
            evasionPoint = armor.evasionPoint;

            this.UpdateItem();
        }

        public Armor(ArmorData data) : base(data)
        {
            defencePoint = data.defencePoint;
            hpPoint = data.hpPoint;
            movementSpeed = data.movementSpeed;
            evasionPoint = data.evasionPoint;
        }

        public override void ChangeData(EquipmentData data)
        {
            if (!(data is ArmorData))
            {
                Debug.LogError("잘못된 데이타 형식입니다.");
            }

            base.ChangeData(data);
            defencePoint = (data as ArmorData).defencePoint;
            hpPoint = (data as ArmorData).hpPoint;
            movementSpeed = (data as ArmorData).movementSpeed;
            evasionPoint = (data as ArmorData).evasionPoint;
        }

        public override void UpdateReinfoce()
        {
            defencePoint = (data as ArmorData).defencePoint + (int)((data as ArmorData).defencePoint * 0.1 * reinforceCount);
            hpPoint = (data as ArmorData).hpPoint + (int)((data as ArmorData).hpPoint * 0.1 * reinforceCount);
            movementSpeed = (data as ArmorData).movementSpeed;
            evasionPoint = (data as ArmorData).evasionPoint;
        }
    } 
}
