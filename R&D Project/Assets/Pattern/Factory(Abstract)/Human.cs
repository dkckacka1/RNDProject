using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Pattern.Factory_Abstract_
{
    public class Human : MonoBehaviour
    {
        [SerializeField] Transform clothTansform;
        [SerializeField] Transform pantsTansform;
        [SerializeField] Transform shoeTansform;

        Cloth cloth;
        Pants pants;
        Shoe shoe;

        public virtual void SetEquipment(HumanFactory factory, int equipmentID = 0)
        {
            this.cloth = factory.CreateEquipment<Cloth>(equipmentID, clothTansform);
            this.pants = factory.CreateEquipment<Pants>(equipmentID, pantsTansform);
            this.shoe = factory.CreateEquipment<Shoe>(equipmentID, shoeTansform);
        }
    }
}

