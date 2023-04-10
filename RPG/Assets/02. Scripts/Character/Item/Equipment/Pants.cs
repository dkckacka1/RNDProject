using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Pants : Equipment
    {
        private int defencePoint;
        private int hpPoint;
        private float movementSpeed;

        // Encapsulation
        public int DefencePoint { get => defencePoint; set => defencePoint = value; }
        public int HpPoint { get => hpPoint; set => hpPoint = value; }
        public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }

        public Pants(Pants pants) : base(pants)
        {
            DefencePoint = pants.DefencePoint;
            HpPoint = pants.HpPoint;
            MovementSpeed = pants.MovementSpeed;

            this.UpdateItem();
        }

        public Pants(PantsData data) : base(data)
        {
            DefencePoint = data.defencePoint;
            HpPoint = data.hpPoint;
            MovementSpeed = data.movementSpeed;
        }

        public override void ChangeData(EquipmentData data)
        {
            if (!(data is PantsData))
            {
                Debug.LogError("잘못된 데이타 형식입니다.");
            }

            base.ChangeData(data);
            DefencePoint = (data as PantsData).defencePoint;
            HpPoint = (data as PantsData).hpPoint;
            MovementSpeed = (data as PantsData).movementSpeed;
        }

        public override void UpdateReinfoce()
        {
            DefencePoint = (data as PantsData).defencePoint + (int)((data as PantsData).defencePoint * 0.1 * reinforceCount);
            HpPoint = (data as PantsData).hpPoint + (int)((data as PantsData).hpPoint * 0.1 * reinforceCount);
            MovementSpeed = (data as PantsData).movementSpeed;
        }


    }
}