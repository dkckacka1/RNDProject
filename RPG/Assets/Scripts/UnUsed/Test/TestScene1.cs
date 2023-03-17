using RPG.Battle.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene1 : MonoBehaviour
{
    public PlayerStatus status;

    private void Start()
    {
        {
            var armor = ResoucesLoader.LoadEquipment<Armor>("Armor", EquipmentType.Armor);
            print(armor.description);
            status.currentArmor = armor;
            var weapon = ResoucesLoader.LoadEquipment<Weapon>("Weapon", EquipmentType.Weapon);
            status.currentWeapon = weapon;
            var helmet = ResoucesLoader.LoadEquipment<Helmet>("Helmet", EquipmentType.Helmet);
            status.currentHelmet = helmet;
            var pants = ResoucesLoader.LoadEquipment<Pants>("Pants", EquipmentType.Pants);
            status.currentPants = pants;

            status.Initialize();
        }

        //status.EquipmentWeapon(WeaponPrefab.GetComponent<Weapon>());

    }
}
