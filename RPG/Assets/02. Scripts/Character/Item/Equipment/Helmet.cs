using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Helmet : Equipment
    {
        private int defencePoint;
        private int hpPoint;
        private float decreseCriticalDamage;
        private float evasionCritical;

        // Encapsulation
        public int DefencePoint { get => defencePoint; set => defencePoint = value; }
        public int HpPoint { get => hpPoint; set => hpPoint = value; }
        public float DecreseCriticalDamage { get => decreseCriticalDamage; set => decreseCriticalDamage = value; }
        public float EvasionCritical { get => evasionCritical; set => evasionCritical = value; }

        public Helmet(Helmet helmet) : base(helmet)
        {
            DefencePoint = helmet.DefencePoint;
            HpPoint = helmet.HpPoint;
            DecreseCriticalDamage = helmet.DecreseCriticalDamage;
            EvasionCritical = helmet.EvasionCritical;

            this.UpdateItem();
        }

        public Helmet(HelmetData data) : base(data)
        {
            DefencePoint = data.defencePoint;
            HpPoint = data.hpPoint;
            DecreseCriticalDamage = data.decreseCriticalDamage;
            EvasionCritical = data.evasionCritical;
        }

        public override void ChangeData(EquipmentData data)
        {
            if (!(data is HelmetData))
            {
                Debug.LogError("잘못된 데이타 형식입니다.");
            }

            base.ChangeData(data);
            DefencePoint = (data as HelmetData).defencePoint;
            HpPoint = (data as HelmetData).hpPoint;
            DecreseCriticalDamage = (data as HelmetData).decreseCriticalDamage;
            EvasionCritical = (data as HelmetData).evasionCritical;
        }

        public override void UpdateReinfoce()
        {

            DefencePoint = (data as HelmetData).defencePoint + (int)((data as HelmetData).defencePoint * 0.1 * reinforceCount);
            HpPoint = (data as HelmetData).hpPoint + (int)((data as HelmetData).hpPoint * 0.1 * reinforceCount);
            DecreseCriticalDamage = (data as HelmetData).decreseCriticalDamage;
            EvasionCritical = (data as HelmetData).evasionCritical;
        }
    }
}