using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class CharacterAppearance : MonoBehaviour
    {
        public Transform weaponHandle;

        public void EquipWeapon(GameObject item)
        {
            Instantiate(item, weaponHandle);
        }

        public void EquipWeapon(int weaponApparenceID)
        {
            var childCount =  weaponHandle.childCount;
            weaponHandle.GetChild(weaponApparenceID).gameObject.SetActive(true);
        }
    }

}