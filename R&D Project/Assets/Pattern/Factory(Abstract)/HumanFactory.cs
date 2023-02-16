using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Pattern.Factory_Abstract_
{
    public class HumanFactory : EquipmentFactory
    {
        [SerializeField] List<GameObject> Cloths;
        [SerializeField] List<GameObject> pants;
        [SerializeField] List<GameObject> shoes;


        public override T CreateEquipment<T>(int equipmentID, Transform transform)
        {
            List<GameObject> eList = null;

            switch (typeof(T).Name)
            {
                case "Cloth": eList = Cloths; break;
                case "Pants": eList = pants;  break;
                case "Shoe":  eList = shoes;  break;
            }

            var equipmentObject = Instantiate(eList[equipmentID],transform);
            return equipmentObject.GetComponent<T>();
        }
    }
}
