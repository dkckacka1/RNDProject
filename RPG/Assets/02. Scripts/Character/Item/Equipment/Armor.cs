using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Armor : Equipment
    {
        private int hpPoint;
        private int defencePoint;
        private float movementSpeed;
        private float evasionPoint;

        // Encapsulation
        public int HpPoint { get => hpPoint; set => hpPoint = value; }
        public int DefencePoint { get => defencePoint; set => defencePoint = value; }
        public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
        public float EvasionPoint { get => evasionPoint; set => evasionPoint = value; }

        public Armor(Armor armor) : base(armor)
        {
            DefencePoint = armor.DefencePoint;
            HpPoint = armor.HpPoint;
            MovementSpeed = armor.MovementSpeed;
            EvasionPoint = armor.EvasionPoint;

            this.UpdateItem();
        }

        public Armor(ArmorData data) : base(data)
        {
            DefencePoint = data.defencePoint;
            HpPoint = data.hpPoint;
            MovementSpeed = data.movementSpeed;
            EvasionPoint = data.evasionPoint;
        }

        public override void ChangeData(EquipmentData data)
        {
            if (!(data is ArmorData))
            {
                Debug.LogError("잘못된 데이타 형식입니다.");
            }

            base.ChangeData(data);
            DefencePoint = (data as ArmorData).defencePoint;
            HpPoint = (data as ArmorData).hpPoint;
            MovementSpeed = (data as ArmorData).movementSpeed;
            EvasionPoint = (data as ArmorData).evasionPoint;
        }

        public override void UpdateReinfoce()
        {
            DefencePoint = (data as ArmorData).defencePoint + (int)((data as ArmorData).defencePoint * 0.1 * reinforceCount);
            HpPoint = (data as ArmorData).hpPoint + (int)((data as ArmorData).hpPoint * 0.1 * reinforceCount);
            MovementSpeed = (data as ArmorData).movementSpeed;
            EvasionPoint = (data as ArmorData).evasionPoint;
        }
    } 
}
