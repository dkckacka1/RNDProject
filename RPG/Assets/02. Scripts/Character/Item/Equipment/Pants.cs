using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Pants : Equipment
    {
        public int defencePoint;
        public int hpPoint;
        public float movementSpeed;

        public Pants(PantsData data) : base(data)
        {
            defencePoint = data.defencePoint;
            hpPoint = data.hpPoint;
            movementSpeed = data.movementSpeed;
        }

        public override void ChangeData(EquipmentData data)
        {
            if (!(data is PantsData))
            {
                Debug.LogError("잘못된 데이타 형식입니다.");
            }

            base.ChangeData(data);
            defencePoint = (data as PantsData).defencePoint;
            hpPoint = (data as PantsData).hpPoint;
            movementSpeed = (data as PantsData).movementSpeed;
        }

        public override void UpdateReinfoce()
        {
            defencePoint = (data as PantsData).defencePoint + (int)((data as PantsData).defencePoint * 0.1 * reinforceCount);
            hpPoint = (data as PantsData).hpPoint + (int)((data as PantsData).hpPoint * 0.1 * reinforceCount);
            movementSpeed = (data as PantsData).movementSpeed;
        }


    }
}