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
    }

}